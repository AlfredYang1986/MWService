using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SemanticSearchService
{
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class SemanticSearchService : ISemanticSearchService
    {
        public string echo(string query)
        {
            return string.Format("Query entered: [{0}]", query);
        }

        public IList<Condition> parse(string query)
        {
            SemanticQueryParser parser = new SemanticQueryParser();
            return parser.parseQuery(query);
        }
    }
}
