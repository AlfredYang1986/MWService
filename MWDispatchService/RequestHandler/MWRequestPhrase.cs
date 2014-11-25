/************************************************************************/
/* phrase the string to the specific Server                             */
/* the return type shall consist as follows                             */
/*      1. Service Name                                                 */
/*      2. Message Name                                                 */
/*      3. Argument List                                                */
/*      4. Return List                                                  */
/*                                                                      */
/************************************************************************/
/*Ayush Edited*/
/*
 * changing the message format due to: conflict in [ ] symbols with 
 * ShareRequestJSON for StylePallet
 * 19-05-2014
 * 
 * New Format:
 * { MessageName: <name>,
 *   Parameters: [
 *      {
 *          ParameterName: <name>, 
 *          ParameterType: <type>,
 *          ParameterValue: <Value>
 *      },
 *      {
 *          ParameterName: <name>, 
 *          ParameterType: <type>,
 *          ParameterValue: <Value>
 *      }...
 *      ]
 *  }
 */
/* The format of Messages:                                              */
/* MessageName|[type name(signtual)value]|*                             */
/* MessageName|[type name(signtual)[sub type name(signtual)value]]|*    */
/*                                                                      */
/*                                                                      */
/*  Created By Alfred Yang                                              */
/*  11-09-2013                                                          */
/************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MWServiceDispatchHelper;

namespace MWDispatchService
{
    class MWRequestPhrase
    {
        public Boolean phraseMessageName(MWRequestPhraseJSON strRequest, ref MWRequestStruct rsMeta)
        {
            rsMeta.MessageName = strRequest.MessageName;
            return true;
        }

        public Boolean phraseMessageServer(ref MWRequestStruct rsMeta)
        {
            using (var xmlFactory = MWXmlPhraseInterfaceFactory.NewInstance())
            {
                rsMeta.ServerName = xmlFactory.getRequestInterface(rsMeta.MessageName);
                if (rsMeta.ServerName == null)
                    return false;

                return true;
            }
        }

        public Boolean phraseMessageArgs(MWRequestPhraseJSON strRequest, ref MWRequestStruct rsMeta)
        {
            IDictionary<string, Object> args = new Dictionary<string, Object>();
            args.Add("param", strRequest.Parameters);
            rsMeta.Arguments = args;
            return true;
        }

        public int phraseMessageReturns(string strRequest, out MWRequestStruct rsMeta)
        {
            rsMeta = null;
            return 0;
        }
    }
} 