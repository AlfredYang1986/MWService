using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MWTreap
{
    /************************************************************************/
    /* Factory method for the Dll                                           */
    /************************************************************************/
    public class MWTreapFactory
    {
        public enum SplitResult
        {
            no_error, // user input perfect fit the database data
            warning,  // user input not perfect fit the database data, that can be guessed by treap
            error,    // user input totally wrong, that cannot be guessed by treap
        };
        /************************************************************************/
        /* singleton for one Application                                        */
        /************************************************************************/
        static IMWTreapInterface _impl = new TreapFacade();

        public static IMWTreapInterface NewInstance()
        {
            return _impl;
        }
    }

    public interface IMWTreapInterface : IDisposable
    {
        string[] autoComplete(string strInput);
        string nodifyError(string strInput);

        MWTreap.MWTreapFactory.SplitResult splitUserInput(string strInput, ref string strErrors
            , ref Dictionary<string, IList<string>> searchArgs
            , ref Dictionary<string, IList<string>> not_searchArgs
            , ref List<string> orderBy
            , ref List<string> orderByDesending);
        void addOtherArguments(ref Dictionary<string, IList<string>> args
                               , string category
                               , IList<string> values);
        MWTreap.MWTreapFactory.SplitResult AppAVAudioCandiSearch(IEnumerable<string> can, ref string key, ref string value);
    }
}
