namespace Kleng.Components.HandControl
{
    /// <summary>
    ///     Pair of terms.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.0.1</version>
    public class Pair
    {
        /// <summary>
        ///     Creates the pair.
        /// </summary>
        /// <param name="term1">First term.</param>
        /// <param name="term2">Second term.</param>
        public Pair(string term1, string term2)
        {
            Term1 = term1;
            Term2 = term2;
        }

        /// <summary>
        ///     First term.
        /// </summary>
        public string Term1 { get; }

        /// <summary>
        ///     Second term.
        /// </summary>
        public string Term2 { get; }

        /// <summary>
        ///     Verifies if two terms are equivalents.
        /// </summary>
        /// <param name="other">Other term to compare with itself.</param>
        /// <returns></returns>
        public bool Equals(Pair other)
        {
            return (Term1 == other.Term1 || Term1 == other.Term2) && (Term2 == other.Term2 || Term2 == other.Term1);
        }
    }
}