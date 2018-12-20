using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Apache.NMS;
using Smoulder;
using Smoulder.Interfaces;
using TrainDataListener.TrainData;

namespace TrainDataListener.Smoulder
{
    public class Loader : LoaderBase
    {
        private IConnection _connection;
        private ISession _session;
        private IDestination _dest;
        private IMessageConsumer _consumer;

        public override void Action(CancellationToken cancellationToken)
        {
            //Strip a message off of the queue
            var msg = _consumer.Receive();

            string body;
            if (msg is ITextMessage)
            {
                body = (msg as ITextMessage).Text;
            }
            else
            {
                return;
            }

            //Parse the xml
            var trustMessage = ParseXml(body);

            if (trustMessage != null)
            {
                ProcessorQueue.Enqueue(trustMessage);
            }
        }

        private void WriteToConsole(string output)
        {
            Console.WriteLine("Loader - " + output);
        }

        private TrustMessage ParseXml(string messageBody)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(messageBody); // This is most expensive bit, refactor this first
                var messageType = doc.DocumentElement.Name;

                TrustMessage trustMessage = null;
                XmlSerializer serialiser;
                var reader = new StringReader(messageBody);

                switch (messageType)
                {
                    case "TrainMovementMsgV1":
                        serialiser = new XmlSerializer(typeof(TrainMovementMsgV1));
                        trustMessage = new TrustMessage
                        {
                            MessageData = (TrainMovementMsgV1) serialiser.Deserialize(reader),
                            MessageType = TrustMessageType.Movement
                        };
                        break;
                    case "TrainActivationMsgV1":
                        serialiser = new XmlSerializer(typeof(TrainActivationMsgV1));
                        trustMessage = new TrustMessage
                        {
                            MessageData = (TrainActivationMsgV1)serialiser.Deserialize(reader),
                            MessageType = TrustMessageType.Activation
                        };
                        break;
                    case "TrainChangeOriginMsgV1":
                        serialiser = new XmlSerializer(typeof(TrainChangeOriginMsgV1));
                        trustMessage = new TrustMessage
                        {
                            MessageData = (TrainChangeOriginMsgV1)serialiser.Deserialize(reader),
                            MessageType = TrustMessageType.ChangeOrigin
                        };
                        break;
                    case "TrainCancellationMsgV1":
                        serialiser = new XmlSerializer(typeof(TrainCancellationMsgV1));
                        trustMessage = new TrustMessage
                        {
                            MessageData = (TrainCancellationMsgV1)serialiser.Deserialize(reader),
                            MessageType = TrustMessageType.Cancellation
                        };
                        break;
                    case "TrainReinstatementMsgV1":
                        serialiser = new XmlSerializer(typeof(TrainReinstatementMsgV1));
                        trustMessage = new TrustMessage
                        {
                            MessageData = (TrainReinstatementMsgV1)serialiser.Deserialize(reader),
                            MessageType = TrustMessageType.Reinstatement
                        };
                        break;
                    case "TrainChangeIdentityMsgV1":
                        serialiser = new XmlSerializer(typeof(TrainChangeIdentityMsgV1));
                        trustMessage = new TrustMessage
                        {
                            MessageData = (TrainChangeIdentityMsgV1)serialiser.Deserialize(reader),
                            MessageType = TrustMessageType.ChangeIdentity
                        };
                        break;
                    default:
                        //TODO Add TrainChangeLocationMsgs
                        WriteToConsole(messageBody);
                        break;
                }

                return trustMessage;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                WriteToConsole(messageBody);
                return null;
            }
        }

        public override async Task Startup(IStartupParameters startupParameters)
        {
            await Connect();
        }

        private async Task Connect()
        {
            var topic = "NR.TRUST";

            var brokerUri = $"activemq:tcp://52.166.187.36:61616"; // Default port
            var factory = new NMSConnectionFactory(brokerUri);

            _connection = factory.CreateConnection();
            _connection.Start();
            _session = _connection.CreateSession(AcknowledgementMode.AutoAcknowledge);
            _dest = _session.GetTopic(topic);
            _consumer = _session.CreateConsumer(_dest);
        }

        public override async Task Finalise()
        {
            _session.Close();
            _connection.Close();
        }
    }
}
