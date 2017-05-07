using System;

namespace Kleng.Components.Events
{
    /// <summary>
    ///     Event argument class that contains important information about
    ///     the match of a string recognized by speech engine.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.4.1</version>
    public class MatchedStringEventArgs : EventArgs
    {
        /// <summary>
        ///     Assigns all properties.
        /// </summary>
        /// <param name="wordPointer">Pointer (Index) to the last word recognized.</param>
        /// <param name="scores">Scores for words pronunciation.</param>
        /// <param name="successes">Correct words.</param>
        /// <param name="wrongs">Wrong words.</param>
        /// <param name="isTheFinalReached">Indicates if the end of the reading was reached.</param>
        public MatchedStringEventArgs(int wordPointer, double[] scores, int successes, int wrongs,
            bool isTheFinalReached)
        {
            // Pointer to last word recognized.
            WordPointer = wordPointer;
            // Words scores.
            Scores = scores;
            // General score.
            Score = CalculateGeneralScore();
            // Correct words.
            Successes = successes;
            // Mistaken words.
            Wrongs = wrongs;
            // True if the end of the reading was reached.
            FinalReached = isTheFinalReached;
        }

        /// <summary>
        ///     Calculates the general score (pronunciation).
        /// </summary>
        /// <returns>General pronunciation score.</returns>
        private double CalculateGeneralScore()
        {
            // General score.
            double score = 0;
            // Words recognized counter.
            var counter = 0;
            // Calculates the total sum.
            foreach (var partial in Scores)
            {
                if (partial > 0)
                    counter++;
                score += partial;
            }
            // Score average.
            score /= counter;
            return score;
        }

        #region PROPERTIES

        /// <summary>
        ///     Scores for words pronunciation.
        /// </summary>
        public double[] Scores { get; }

        /// <summary>
        ///     General score of pronunciation.
        /// </summary>
        public double Score { get; }

        /// <summary>
        ///     Pointer (Index) to the last word recognized.
        /// </summary>
        public int WordPointer { get; }

        /// <summary>
        ///     Words correctly spelled/recognized.
        /// </summary>
        public int Successes { get; }

        /// <summary>
        ///     Words mistakenly spelled/recognized.
        /// </summary>
        public int Wrongs { get; }

        /// <summary>
        ///     Indicates if the end of the reading was reached.
        /// </summary>
        public bool FinalReached { get; }

        #endregion
    }
}