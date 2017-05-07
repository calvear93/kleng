using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Kleng.Components.Events;
using Kleng.Components.Exceptions;

namespace Kleng.Components.VoiceRecognizer
{
    /// <summary>
    ///     Reading manager. Reads and processes the reading
    ///     in order to transfer the words and phrases to
    ///     voice recognition engine.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>3.1.6.2</version>
    public class Reading
    {
        /// <summary>
        ///     Create the reading manager.
        /// </summary>
        /// <param name="uri">Uniform Resource Identifier of the file containing the reading</param>
        /// <param name="confidence">Percentage of reliability of recognized atom. From [0,1[</param>
        public Reading(string uri, double confidence)
        {
            // Reads the reading text in an array of lines.   
            var lines = File.ReadAllLines(@uri, Encoding.Default);
            try
            {
                // Gets the title.
                Title = lines[0];
                // Gets the first line, then gets next ones.
                Text += lines[1];
                RawText += lines[1] + "\n";
                for (var i = 2; i < lines.Length; i++)
                {
                    if (lines[i] == String.Empty)
                        continue;
                    Text += " " + lines[i].Replace(Environment.NewLine, " ");
                    RawText += lines[i] + "\n";
                }
            }
            catch
            {
                // If reading file format is wrong, throws an exception.
                throw new ActivityFormatException("Invalid Reading activity file format.");
            }

            WordPointer = -1;
            Confidence = confidence;
            // Separate words without trim and any process.
            RawWords = new Regex(@"(?<=[ \n])").Split(RawText).Where(s => s != string.Empty).ToArray();
            // Generates the separate words (grammatical signs stripped) from text.
            Text = Regex.Replace(Text, @"[.,?¿!¡;:$—-]", string.Empty);
            Words = Text.ToLowerInvariant().Split(' ').Select(word => word.Trim()).ToArray();
            // Creates the word score array.
            Scores = new double[Words.Length];
            // Lock object for multi-threading.
            _locker = new object();
            // Thread for the string recognized processing.
            var checkerTask = new Thread(CheckingService);
            // Initializes the string recognized queue.
            _recognizedStrings = new Queue<Tuple<string, double>>();
            // Starts the thread.
            _isRunning = true;
            // Initializes the thread for string processing.
            checkerTask.Start();
        }

        #region PROPERTIES

        /// <summary>
        ///     Title of the reading.
        /// </summary>
        public string Title { get; }

        /// <summary>
        ///     The whole text from the reading.
        ///     Without grammatical signs.
        /// </summary>
        public string Text { get; }

        /// <summary>
        ///     The raw text from the reading.
        /// </summary>
        public string RawText { get; }

        /// <summary>
        ///     Separate words from the reading text.
        ///     Without grammatical signs.
        /// </summary>
        public string[] Words { get; }

        /// <summary>
        ///     Separate raw words from the reading text.
        /// </summary>
        public string[] RawWords { get; }

        /// <summary>
        ///     Scores for words pronunciation.
        /// </summary>
        public double[] Scores { get; }

        /// <summary>
        ///     Pointer (Index) to the last word recognized.
        /// </summary>
        public int WordPointer { get; private set; }

        /// <summary>
        ///     Percentage of reliability of recognized atom.
        /// </summary>
        public double Confidence { get; }

        /// <summary>
        ///     Indicates if the final of the reading was reached.
        /// </summary>
        public bool FinalReached => WordPointer == Words.Length - 1;

        /// <summary>
        ///     Lock for multi-threading.
        /// </summary>
        private readonly object _locker;

        /// <summary>
        ///     Multi-threading flag.
        /// </summary>
        private bool _isRunning;

        /// <summary>
        ///     Queue that stores the string recognized information.
        /// </summary>
        private readonly Queue<Tuple<string, double>> _recognizedStrings;

        #endregion

        #region EVENTS

        /// <summary>
        ///     Event handler for Matched String event.
        /// </summary>
        public event EventHandler<MatchedStringEventArgs> MatchedStringEvent;

        /// <summary>
        ///     Subscribes a function to Matched String event.
        ///     Is recommended to use OnSkeletonDataReady(object sender, Skeleton[] data)
        ///     header for listener function.
        /// </summary>
        public EventHandler<MatchedStringEventArgs> MatchedString
        {
            set { MatchedStringEvent += value; }
        }

        #endregion

        #region SYNCHRONIZED

        /// <summary>
        ///     Service for processing the data of string recognized from speech engine.
        /// </summary>
        private void CheckingService()
        {
            // Information of string recognized. (String and confidence).
            while (_isRunning)
                lock (_locker)
                    if (_recognizedStrings.Count > 0)
                    {
                        // Gets the next string recognized for processing.
                        var tuple = _recognizedStrings.Dequeue();
                        // Initializes the processing of the string recognized.
                        ProcessStringRecognized(tuple.Item1, tuple.Item2);
                    }
        }

        /// <summary>
        ///     Enqueue the string recognized information for multi-threading processing.
        /// </summary>
        /// <param name="recognized">String recognized by speech engine.</param>
        /// <param name="confidence">Reliability given by speech engine.</param>
        public void Check(string recognized, double confidence)
        {
            // Locks the operation and enqueue the information.
            lock (_locker)
                _recognizedStrings.Enqueue(new Tuple<string, double>(recognized, confidence));
        }

        #endregion

        #region GRAMMAR & RECOGNITION

        /// <summary>
        ///     Generates the grammar scheme for the speech recognition engine.
        ///     It consist of all words combinations, forming atoms and phrases.
        /// </summary>
        /// <returns>Array of atomic and combined strings to will be recognized for speech engine.</returns>
        public string[] GetGrammarScheme()
        {
            // Scheme list.
            var scheme = new List<string>();
            // Adds atoms to scheme.
            scheme.AddRange(Words);

            // Generates the scheme.
            for (var i = 1; i < Words.Length; i++)
            {
                // Atomic element.
                var atom = Words[i - 1];
                // Generates the combinations.
                for (var j = i; j < Words.Length; j++)
                {
                    atom += " " + Words[j];
                    scheme.Add(atom);
                }
            }
            // Removes repeated and empty atoms (strings), so returns the scheme.
            return scheme.Distinct().Where(word => !string.IsNullOrWhiteSpace(word)).ToArray();
        }

        /// <summary>
        ///     Calculates every combinations of each atom (word) in the array.
        /// </summary>
        /// <param name="words">Array of words.</param>
        /// <returns>All combinations of words.</returns>
        /// <warning>UNUSED. OUT OF MEMORY EXCEPTION. HIGH COMPLEXITY ALGORITHM.</warning>
        private List<string> CombineStringArray(params string[] words)
        {
            // Base case.
            if (words.Length == 1)
                return new List<string> {words[0]};

            // Calculate sub-combinations and concatenate it to current.
            var subAtomsList = CombineStringArray(words.Skip(1).ToArray());
            var atomsList = new List<string>().Concat(subAtomsList).ToList();
            atomsList.Add(words[0]);
            // Generates the current level combinations.
            atomsList.AddRange(subAtomsList.Select(subAtoms => words[0] + " " + subAtoms));

            return atomsList;
        }

        /// <summary>
        ///     Removes all the diacritics accents from the string.
        /// </summary>
        /// <param name="text">String with accents.</param>
        /// <returns>String without accents.</returns>
        private static string RemoveDiacriticAccents(string text)
        {
            return Encoding.UTF8.GetString(Encoding.GetEncoding("ISO-8859-8").GetBytes(text));
        }

        /// <summary>
        ///     Checks the recognized input by words and sets up the scores.
        /// </summary>
        /// <param name="recognized">String recognized by speech engine.</param>
        /// <param name="confidence">Reliability given by speech engine.</param>
        private void ProcessStringRecognized(string recognized, double confidence)
        {
            // Verifies the confidence of the recognized atom.
            if (confidence < Confidence)
                return;
            // Gets each word from string recognized.
            var atoms = recognized.Split(' ');
            // Auxiliary pointer for verification. Begins from next word to last matched one.
            var i = WordPointer + 1;
            // Tries to match recognized string on some reading text segment not previously matched.
            for (; i < Words.Length; i++)
            {
                // Tries to match the segment, otherwise continue to next iteration.
                if (!Words[i].Equals(atoms[0]) || !VerifyRecognizedString(atoms, i)) continue;

                // If matches, moves the current word pointer to the next index of the last word recognized.
                WordPointer = i + atoms.Length - 1;
                // Assigns the confidence to the words recognized.
                for (; i < WordPointer + 1; i++)
                    Scores[i] = confidence;
                // Correct and Wrongs words respectively.
                int successes, wrongs;
                // Calculate correct and wrong words.
                CalculateSuccessessAndWrongs(out successes, out wrongs);
                // Triggers the event.
                MatchedStringEvent?.Invoke(this,
                    new MatchedStringEventArgs(WordPointer, Scores, successes, wrongs, FinalReached));
                // If the final of the reading is reached, terminates the thread.
                if (FinalReached)
                    _isRunning = false;
                break;
            }
        }

        /// <summary>
        ///     Verifies if the array of words given matches with the reading text segment from the index given.
        /// </summary>
        /// <param name="atoms">Recognized words to will be matched.</param>
        /// <param name="from">Beginning index for reading words.</param>
        /// <returns>True if segments are matched, false otherwise.</returns>
        private bool VerifyRecognizedString(string[] atoms, int from)
        {
            // If the lengths don't match, returns false.
            if ((Words.Length - from) < atoms.Length)
                return false;
            // Verifies word by word if it matches.
            for (int i = from, j = 0; j < atoms.Length; i++, j++)
            {
                if (!Words[i].Equals(atoms[j]))
                    return false;
            }
            return true;
        }

        /// <summary>
        ///     Calculates words correct and mistakenly spelled/recognized.
        /// </summary>
        /// <param name="successes">(Out). Words correctly spelled/recognized.</param>
        /// <param name="wrongs">(Out). Words mistakenly spelled/recognized.</param>
        private void CalculateSuccessessAndWrongs(out int successes, out int wrongs)
        {
            // Correct words counter.
            successes = Scores.Count(score => score > 0);
            // Calculates wrong words.
            wrongs = WordPointer + 1 - successes;
        }

        #endregion
    }
}