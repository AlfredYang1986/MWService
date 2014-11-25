using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using java.io;
using opennlp.tools.parser;
using opennlp.tools.cmdline.parser;

namespace SemanticSearchService
{
    /* The address strings are hard coded. Later refactoring should make
     * sure that such strings are read from a server-side config file */
    class SemanticQueryParser
    {
        private Parser parser = null;
        Theap th = null;
        private IDictionary<string, List<string>> searchArgs = new Dictionary<string, List<string>>();

        public SemanticQueryParser(Theap th, string modelPath = @".\OpenNLP\en-parser-chunking.bin\")
        {
            FileInputStream parserModelInputStream = new FileInputStream(modelPath);
            ParserModel parserModel = new ParserModel(parserModelInputStream);
            parser = ParserFactory.create(parserModel);
            this.th = th;
        }

        public SemanticQueryParser(string modelPath = @".\OpenNLP\en-parser-chunking.bin\")
        {
            FileInputStream parserModelInputStream = new FileInputStream(modelPath);
            ParserModel parserModel = new ParserModel(parserModelInputStream);
            this.th = new Theap();
            parser = ParserFactory.create(parserModel);
        }

        //Assuming a query is one single sentence
        public IList<Condition> parseQuery(string sentence)
        {
            IList<Condition> parsed = new List<Condition>();
            Parse parse = ParserTool.parseLine(sentence, parser, 1)[0];

            List<Parse> nounPhrases = new List<Parse>();
            getNounPhrases(parse, nounPhrases);
            
            foreach (var p in nounPhrases)
            {
                foreach (Condition cond in parseCondition(p.toString()))
                {
                    parsed.Add(cond);
                }
            }

            return parsed;
        }

        /* Most conditions are ultimately represented by a noun phrase.*/
        public void getNounPhrases(Parse p, List<Parse> nounPhrases)
        {

            if (p.getType() == "NP")
            {
                if (p.getChildCount() == p.getCoveredText().Split().Length)
                    nounPhrases.Add(p);
            }
            foreach (Parse child in p.getChildren())
            {
                getNounPhrases(child, nounPhrases);
            }
        }

        /* In this implementation I'm assuming no negation word be used, and
         each query represents only one item */
        public IList<Condition> parseCondition(string phrase)
        {
            IList<Condition> discovered = new List<Condition>();
            string[] words = phrase.Split();
            foreach (string _word in words)
            {
                Word word = th.getWord(_word.Trim().ToLower());
                if (word != null)
                {
                    discovered.Add(new Condition(word.getConditionType(), _word));
                }
            }
            return discovered;
        }

    }
}
