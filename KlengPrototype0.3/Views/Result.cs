namespace Kleng.Views
{
    /// <summary>
    ///     Stores the results of an activity.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.0</version>
    public class Result
    {
        /// <summary>
        ///     Corrects (words or pairs).
        /// </summary>
        public string Corrects;

        /// <summary>
        ///     Date of the activity.
        /// </summary>
        public string Date;

        /// <summary>
        ///     Score of the activity.
        /// </summary>
        public string Score;

        /// <summary>
        ///     Time of the activity.
        /// </summary>
        public string Time;

        /// <summary>
        ///     Time (in the reading) until the end of the activity.
        /// </summary>
        public string TimeConsuming;

        /// <summary>
        ///     Title of the activity.
        /// </summary>
        public string Title;

        /// <summary>
        ///     Wrongs (words or pairs).
        /// </summary>
        public string Wrongs;

        /// <summary>
        ///     Create the object.
        /// </summary>
        /// <param name="title">Title of the activity.</param>
        /// <param name="time">Time of the activity.</param>
        /// <param name="date">Date of the activity.</param>
        public Result(string title, string time, string date)
        {
            Title = title;
            Time = time;
            Date = date;
        }
    }
}