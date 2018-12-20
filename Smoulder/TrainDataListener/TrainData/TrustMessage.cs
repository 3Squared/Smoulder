using Smoulder.Interfaces;

namespace TrainDataListener.TrainData
{
    public class TrustMessage : IProcessDataObject, IDistributeDataObject
    {
        public TrustMessageData MessageData;
        public TrustMessageType MessageType;
    }
}
