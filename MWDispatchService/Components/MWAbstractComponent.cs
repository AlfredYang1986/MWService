using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace MWDispatchService
{
    class MWAbstractComponent
    {
        public static string Serialize<T>(T instance)
        {
            var data = new StringBuilder();
            var serializer = new DataContractSerializer(instance.GetType());

            using (var writer = XmlWriter.Create(data))
            {
                serializer.WriteObject(writer, instance);
                writer.Flush();

                return data.ToString();
            }
        }
    }

    class MWRemoteComponent : MWAbstractComponent
    {

    }

    class MWLocalComponent : MWAbstractComponent
    {

    }
}
