using DBC.Infrastructure.AutoMapperConfigs;
using DBC.Models;
using System;
using System.Runtime.Serialization;

namespace DBC.Contracts.DataContracts
{
    [DataContract]
    public class MunicipalityVw : IMapFrom<Municipality>
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }
    }
}
