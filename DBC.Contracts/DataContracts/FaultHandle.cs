using System.Runtime.Serialization;

namespace DBC.Contracts.DataContracts
{
    [DataContract]
    public class FaultHandle
    {
        public FaultHandle(string error)
        {
            Error = error;
        }

        [DataMember]
        public string Error;
    }
}
