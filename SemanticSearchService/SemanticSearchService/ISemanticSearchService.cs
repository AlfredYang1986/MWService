using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SemanticSearchService
{

    [ServiceContract]
    public interface ISemanticSearchService
    {
        [OperationContract]
        [WebGet(UriTemplate="echo/{query}")]
        string echo(string query);

        [OperationContract]
        [WebGet(UriTemplate = "parse/{query}")]
        IList<Condition> parse(string query);
    }
}
