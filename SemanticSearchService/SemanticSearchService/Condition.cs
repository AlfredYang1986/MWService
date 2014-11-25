using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace SemanticSearchService
{
    [DataContract]
    public class Condition
    {
        [DataMember]
        public string Type { get; set; }
        [DataMember]
        public string Content { get; set; }

        public Condition(string type, string content)
        {
            Type = type;
            Content = content;
        }

        public override string ToString()
        {
            return Type + ":" + Content;
        }
    }
}
