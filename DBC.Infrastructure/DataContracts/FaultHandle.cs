using System.Runtime.Serialization;

namespace DBC.Infrastructure.DataContracts
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
