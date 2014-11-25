using MWStylePalette;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWDataSerilizationType;
using System.Runtime.Serialization.Json;
using System.IO;
/*
 * Redundant code.. Now implemented separately with WSDualHttpBinding
 */

/*
 * Created by Ayush
 * 
 * Component for Style Palette
 */
namespace MWDispatchService
{
    class MWStylePaletteComponent : MWRemoteComponent
    {
        public T jsonDes<T>(string data)
        {
            using (var reader = new MemoryStream(Encoding.Unicode.GetBytes(data)))
            {
                var ser = new DataContractJsonSerializer(typeof(T));
                return (T)ser.ReadObject(reader);
            }

        }
        public string ShareRequest(IDictionary<string, Object> args)
        {
            using (var client = MWStylePaletteFactory.NewInstance())
            {
                MWShareRequestJSON deserialised = jsonDes<MWShareRequestJSON>(args["sharerequest"] as string);
                string reVal = client.ShareRequest(deserialised);
                return reVal;
            }
        }

        public string AcceptRequest(IDictionary<string, Object> args)
        {
            using (var client = MWStylePaletteFactory.NewInstance())
            {
                MWAcceptRequestJSON deserialised = jsonDes<MWAcceptRequestJSON>(args["acceptrequest"] as string);
                string reVal = client.AcceptRequest(deserialised);
                return reVal;
            }
        }

        public string StyleChangeNotify(IDictionary<string, Object> args)
        {
            using (var client = MWStylePaletteFactory.NewInstance())
            {
                MWSPEnvironmentJSON deserialised = jsonDes<MWSPEnvironmentJSON>(args["stylechangenotify"] as string);
                string reVal = client.StyleChangeNotify(deserialised);
                return reVal;
            }
        }

        public string GetPreviousEnvironment(IDictionary<string, Object> args)
        {
            using (var client = MWStylePaletteFactory.NewInstance())
            {
                string reVal = client.GetPreviousEnvironment(args["getenv"] as string);
                return reVal;
            }
        }
    }
}
