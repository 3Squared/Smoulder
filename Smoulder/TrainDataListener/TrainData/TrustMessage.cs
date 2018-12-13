using Smoulder.Interfaces;

namespace TrainDataListener.TrainData
{
    public class TrustMessage : IProcessDataObject
    {
        public TrustMessageData MessageData;
        public TrustMessageType MessageType;
    }
}
