using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemanticSearchService
{
    /* A Word is a string with possible conditions, each associated with a
     * probability. All probabilities should sum up to 1. All conditions get
     * updated based on global usage of the words by the users. The updating
     * logic is not yet implemented. -Amos */

    class Word
    {
        public string Text { get; set; }
        public IDictionary<string, float> conditions = new Dictionary<string, float>();

        public Word(string text)
        {
            Text = text;
        }

        public string getConditionType()
        {
            string most_likely_type = "UNKNOWN";
            float highest_probablity=-1; //To ensure it will be overriden by a real prob.
            foreach (string condition in conditions.Keys)
            {
                float current_probablity = conditions[condition];
                if (current_probablity > highest_probablity) {
                    most_likely_type = condition;
                    highest_probablity = current_probablity;
                } 
            }
            return most_likely_type;
        }
    }
}
