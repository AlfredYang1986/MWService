using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Speech;
using MWSearchingEngine;
using MWDataSerilizationType;
using Microsoft.Speech.Recognition;
using System.IO;

namespace MWDispatchService.SpeechObject
{
    public class SpRecognition
    {
        private Boolean completed = false;
        private string result = @"";
        private List<string> reCandidates = new List<string>();
        private IEnumerable<Apparel> searchResult = null;

        // Handle the SpeechRecognized event.
        void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result != null && e.Result.Text != null)
            {
                Console.WriteLine("  Recognized text =  {0}", e.Result.Text);
                reCandidates.Clear();
                if (e.Result.Alternates != null && e.Result.Alternates.Count > 0)
                {
                    foreach (var iter in e.Result.Alternates)
                    {
                        reCandidates.Add(iter.Text);
                    }
                }
                result = e.Result.Text;
            }
            else
            {
                Console.WriteLine("  Recognized text not available.");
            }
        }

        // Handle the RecognizeCompleted event.
        void recognizer_RecognizeCompleted(object sender, RecognizeCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Console.WriteLine("  Error encountered, {0}: {1}",
                e.Error.GetType().Name, e.Error.Message);
            }
            if (e.Cancelled)
            {
                Console.WriteLine("  Operation cancelled.");
            }
            if (e.InputStreamEnded)
            {
                Console.WriteLine("  End of stream encountered.");
            }
            completed = true;
        }

        public IEnumerable<String> RecognizeFromWAVStream(Stream stream)
        {
            try
            {
                using (var recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US")))
                {
                    // Create and load a grammar.
                    Choices colors = new Choices();
                    using (var se = MWSearchingEngineFactory.NewInstance())
                    {
                        var candidate = se.GetColorCandidata();
                        colors.Add(candidate.ToArray<string>());
                    }

                    // Create a GrammarBuilder object and append the Choices object.
                    GrammarBuilder gb = new GrammarBuilder();
                    gb.Append(colors);

                    //Grammar dictation = new DictationGrammar();
                    //dictation.Name = "Dictation Grammar";
                    Grammar g = new Grammar(gb);
                    recognizer.LoadGrammar(g);

                    // Configure the input to the recognizer.
                    //recognizer.SetInputToWaveFile(fileName);
                    recognizer.SetInputToWaveStream(stream);

                    // Attach event handlers for the results of recognition.
                    recognizer.SpeechRecognized +=
                      new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
                    recognizer.RecognizeCompleted +=
                      new EventHandler<RecognizeCompletedEventArgs>(recognizer_RecognizeCompleted);

                    // Perform recognition on the entire file.
                    Console.WriteLine("Starting asynchronous recognition...");
                    completed = false;
                    recognizer.RecognizeAsync();

                    // Keep the console window open.
                    while (!completed)
                    {
                        //Console.ReadLine();
                    }
                    //Console.WriteLine("Done.");
                    return reCandidates.Count == 0 ? null : reCandidates as IEnumerable<string>;
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex);
                return null;
            }
        }

        public IEnumerable<string> RecognizeFromWAVFile(string fileName)
        {
            try
            {
                using (var recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US")))
                {
                    // Create and load a grammar.
                    Choices colors = new Choices();
                    using (var se = MWSearchingEngineFactory.NewInstance())
                    {
                        var candidate = se.GetColorCandidata();
                        colors.Add(candidate.ToArray<string>());
                    }

                    // Create a GrammarBuilder object and append the Choices object.
                    GrammarBuilder gb = new GrammarBuilder();
                    gb.Append(colors);

                    //Grammar dictation = new DictationGrammar();
                    //dictation.Name = "Dictation Grammar";
                    Grammar g = new Grammar(gb);
                    recognizer.LoadGrammar(g);

                    // Configure the input to the recognizer.
                    recognizer.SetInputToWaveFile(fileName);

                    // Attach event handlers for the results of recognition.
                    recognizer.SpeechRecognized +=
                      new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
                    recognizer.RecognizeCompleted +=
                      new EventHandler<RecognizeCompletedEventArgs>(recognizer_RecognizeCompleted);

                    // Perform recognition on the entire file.
                    Console.WriteLine("Starting asynchronous recognition...");
                    completed = false;
                    recognizer.RecognizeAsync();

                    // Keep the console window open.
                    while (!completed)
                    {
                        //Console.ReadLine();
                    }
                    //Console.WriteLine("Done.");
                    return reCandidates.Count == 0 ? null : reCandidates as IEnumerable<string>;
                }
            }
            catch (Exception ex) 
            {
                System.Console.WriteLine(ex);
                return null;
            }
        }
    }  
}
