using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SemanticSearchService
{
    /* This class is a stub for the Tree heap class. It stores the most
     * frequently used condition words and their types accordingly. Each 
     * word is assosicated with a weighted vector of conditions, the weights
     * of which are dynamically updated on the server, depending on user's
     * usage of the words. This class need to be replaced -- Amos */
    class Theap
    {
        IList<Word> words = new List<Word>();

        public Theap(string voc = @"H:\Projects\megawardrobe\backend\ConsoleApplication3\vocabulary.txt")
        {
            try
            {
                using (StreamReader sr = new StreamReader(voc))
                {
                    string line = null;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] segs = line.Split(',');
                        if (segs.Length == 3)
                        {
                            Word word = new Word(segs[0]);
                            word.conditions.Add(segs[1],float.Parse(segs[2]));
                            words.Add(word);

                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }

        public Word getWord(string word)
        {
            foreach(Word _word in words){
                if (_word.Text == word)
                {
                    return _word;
                }
            }
            return null;
        }
    }
}
