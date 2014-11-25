using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWServiceDispatchHelper
{
    /************************************************************************/
    /* Client Remote Factory                                                */
    /************************************************************************/
    class MWLocalClassFactory : MWAbstactClassFactory
    {
        public static MWAbstactClient MWCreate(string strClassName)
        {
            Type t = Type.GetType(MWServiceConstConfig.strNameSpace + "." + strClassName);
            return MWAbstactClassFactory
                .MWCreate<MWAbstactClient>(t);
        }
    }

    class MWRemoteClassFactory : MWAbstactClassFactory
    {
        public static MWAbstactClient MWCreate(string strClassName)
        {
            Type t = Type.GetType(MWServiceConstConfig.strNameSpace + "." + strClassName);
            return MWAbstactClassFactory
                .MWCreate<MWAbstactClient>(t);
        }
    }

    /************************************************************************/
    /* Searching Component                                                  */
    /************************************************************************/
    class MWDispatchComponent : MWAbstrctComponent
    {
        public override string ToString()
        {
            String strVel = "MWDispatchComponent:\n\t";

            foreach (MWAbstactClient client in remoteInterfaces)
            {
                strVel += String.Format("Interface: {0}\n\t", client.ToString());
            }

            return strVel;
        }
    }

    /************************************************************************/
    /* Searching Client                                                     */
    /************************************************************************/
    class MWSearchingEngineClient : MWAbstactClient
    {
        public override object request(string strMessagename, string strRequestBosy)
        {
            String str = String.Format(
                "MWSearchingEngineClient:\n\t{0}\n\t\t{1}"
                , strMessagename, strRequestBosy);
            Console.WriteLine(str);
            return null;
        }

        public override string ToString()
        {
            string reVal = "MWSearchingEngineClient\n\t\t";
            foreach (string s in capableRequest)
            {
                reVal += String.Format("{0}\n\t\t", s);
            }
            return reVal;
        }

        public override string getClientName()
        {
            return "MWSearchingEngine";
        }

        public override string getClientFactoryName()
        {
            return "";
        }
    }

    /**********************Ayush added Style Pallet**************************/
    /* StylePalette Component                                                */
    /************************************************************************/
    class MWStylePaletteComponent : MWAbstrctComponent
    {
        public override string ToString()
        {
            String strVel = "MWStylePaletteComponent:\n\t";

            foreach (MWAbstactClient client in remoteInterfaces)
            {
                strVel += String.Format("Interface: {0}\n\t", client.ToString());
            }

            return strVel;
        }
    }

    /************************************************************************/
    /* StylePalette Client                                                   */
    /************************************************************************/
    class MWStylePaletteClient : MWAbstactClient
    {
        public override object request(string strMessagename, string strRequestBosy)
        {
            String str = String.Format(
                "MWStylePaletteClient:\n\t{0}\n\t\t{1}"
                , strMessagename, strRequestBosy);
            Console.WriteLine(str);
            return null;
        }

        public override string ToString()
        {
            string reVal = "MWStylePaletteClient\n\t\t";
            foreach (string s in capableRequest)
            {
                reVal += String.Format("{0}\n\t\t", s);
            }
            return reVal;
        }

        public override string getClientName()
        {
            return "MWStylePalette";
        }

        public override string getClientFactoryName()
        {
            return "";
        }
    }
    /************************************************************************/
    /* Caching Component                                                    */
    /************************************************************************/
    class MWCachingComponent : MWAbstrctComponent
    {
        public override string ToString()
        {
            String strVel = "MWCachingCoponent:\n\t";

            foreach (MWAbstactClient client in remoteInterfaces)
            {
                strVel += String.Format("Interface: {0}\n\t", client.ToString());
            }

            return strVel;
        }
    }

    /************************************************************************/
    /* Caching Client                                                       */
    /************************************************************************/
    class MWCachingClient : MWAbstactClient
    {
        public override object request(string strMessagename, string strRequestBosy)
        {
            String str = String.Format(
                "MWCachingClient:\n\t{0}\n\t\t{1}"
                , strMessagename, strRequestBosy);
            Console.WriteLine(str);
            return null;
        }

        public override string ToString()
        {
            string reVal = "MWCachingClient\n\t\t";
            foreach (string s in capableRequest)
            {
                reVal += String.Format("{0}\n\t\t", s);
            }
            return reVal;
        }

        public override string getClientName()
        {
            return "Caching";
        }

        public override string getClientFactoryName()
        {
            throw new NotImplementedException();
        }
    }

    /************************************************************************/
    /* Tagging Component                                                  */
    /************************************************************************/
    class MWTaggingComponent : MWAbstrctComponent
    {
        public override string ToString()
        {
            String strVel = "MWTaggingComponent:\n\t";

            foreach (MWAbstactClient client in remoteInterfaces)
            {
                strVel += String.Format("Interface: {0}\n\t", client.ToString());
            }

            return strVel;
        }
    }

    /************************************************************************/
    /* Tagging Client                                                     */
    /************************************************************************/
    class MWTaggingClient : MWAbstactClient
    {
        public override object request(string strMessagename, string strRequestBosy)
        {
            String str = String.Format(
                "MWTaggingClient:\n\t{0}\n\t\t{1}"
                , strMessagename, strRequestBosy);
            Console.WriteLine(str);
            return null;
        }

        public override string ToString()
        {
            string reVal = "MWTaggingClient\n\t\t";
            foreach (string s in capableRequest)
            {
                reVal += String.Format("{0}\n\t\t", s);
            }
            return reVal;
        }

        public override string getClientName()
        {
            return "MWTagging";
        }

        public override string getClientFactoryName()
        {
            return "";
        }
    }

    /************************************************************************/
    /* MyWardrobe Component                                                  */
    /************************************************************************/
    class MWMyWardrobeComponent : MWAbstrctComponent
    {
        public override string ToString()
        {
            String strVel = "MWMyWardrobeComponent:\n\t";

            foreach (MWAbstactClient client in remoteInterfaces)
            {
                strVel += String.Format("Interface: {0}\n\t", client.ToString());
            }

            return strVel;
        }
    }

    /************************************************************************/
    /* MyWardrobe Client                                                     */
    /************************************************************************/
    class MWMyWardrobeClient : MWAbstactClient
    {
        public override object request(string strMessagename, string strRequestBosy)
        {
            String str = String.Format(
                "MWMyWardrobeClient:\n\t{0}\n\t\t{1}"
                , strMessagename, strRequestBosy);
            Console.WriteLine(str);
            return null;
        }

        public override string ToString()
        {
            string reVal = "MWMyWardrobeClient\n\t\t";
            foreach (string s in capableRequest)
            {
                reVal += String.Format("{0}\n\t\t", s);
            }
            return reVal;
        }

        public override string getClientName()
        {
            return "MWMyWardrobe";
        }

        public override string getClientFactoryName()
        {
            return "";
        }
    }
}
