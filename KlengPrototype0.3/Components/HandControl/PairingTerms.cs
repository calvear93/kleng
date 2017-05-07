using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Kleng.Components.Exceptions;

namespace Kleng.Components.HandControl
{
    /// <summary>
    ///     Pairing terms manager. Reads and processes the
    ///     terms for pairing activity.
    ///     <author>Cristopher Alvear Candia</author>
    ///     <version>1.0.13</version>
    /// </summary>
    public class PairingTerms
    {
        /// <summary>
        ///     Creates the controller.
        /// </summary>
        /// <param name="uri">Uniform Resource Identifier of the file containing the pairing terms.</param>
        public PairingTerms(string uri)
        {
            Pairs = new List<Pair>();
            // Reads the pairing terms text in an array of lines.   
            var lines = File.ReadAllLines(@uri, Encoding.Default);
            try
            {
                // Gets the title.
                // Gets the first line, then gets next ones.
                Title = lines[0];
                // Forces to verify file format.
                if (lines.Length < 2)
                    throw new Exception();
                for (var i = 1; i < lines.Length; i++)
                {
                    var terms = lines[i].Split(',').Select(term => term.Trim()).ToArray();
                    Pairs.Add(new Pair(terms[0], terms[1]));
                }
            }
            catch
            {
                // If pairing terms file format is wrong, throws an exception.
                throw new ActivityFormatException("Invalid Pairing Terms activity file format.");
            }
        }

        #region PROPERTIES

        /// <summary>
        ///     Title of the reading.
        /// </summary>
        public string Title { get; }

        /// <summary>
        ///     List of pairs.
        /// </summary>
        public List<Pair> Pairs;

        /// <summary>
        ///     Pairs quantity.
        /// </summary>
        public int Count => Pairs.Count;

        #endregion

        #region LISTS CONTROL

        /// <summary>
        ///     Verifies if a pair is correct and belongs to the pairs list.
        /// </summary>
        /// <param name="term1">First term.</param>
        /// <param name="term2">Second term.</param>
        /// <returns>True if are equals, otherwise false.</returns>
        public bool IsCorrectPair(string term1, string term2)
        {
            var pair = new Pair(term1, term2);
            return Pairs.Any(p => pair.Equals(p));
        }

        /// <summary>
        ///     Randomizes the terms on each side of the pairs.
        /// </summary>
        /// <returns>Matrix with two columns with shuffled terms. (left and right pairs)</returns>
        public string[][] GetShuffledTerms()
        {
            // Creates and populates the list with the terms.
            var left = new List<string>();
            var right = new List<string>();

            foreach (var p in Pairs)
            {
                left.Add(p.Term1);
                right.Add(p.Term2);
            }
            // Matrix will store the shuffled terms.
            var shuffledTerms = new string[2][];
            // Shuffle by random events.
            var rand = new Random();
            shuffledTerms[0] = left.OrderBy(c => rand.Next()).ToArray();
            shuffledTerms[1] = right.OrderBy(c => rand.Next()).ToArray();

            return shuffledTerms;
        }

        #endregion
    }
}