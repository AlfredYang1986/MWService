using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace MWDispatchService
{
    [DataContract]
    public class AuthorizationCheck
    {
        [DataMember]
        public string Token { get; set; }
        [DataMember]
        public MWRequestPhraseJSON RequestString { get; set; }
    }
    [DataContract]
    public class MWRequestPhraseJSON
    {
        [DataMember]
        public string MessageName { get; set; }
        [DataMember]
        //public List<MWParameterJSON> Parameters;
        public string Parameters { get; set; }

    }

    [DataContract]
    public class MWParameterJSON
    {
        [DataMember]
        public string ParameterName { get; set; }
        [DataMember]
        public string ParameterType { get; set; }
        [DataMember]
        public string ParameterValue { get; set; }
    }
}
