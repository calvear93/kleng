using System.IO;
using System.Windows.Input;

namespace Kleng.Views.Utils
{
    /// <summary>
    ///     Gets some nice cursors.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.0</version>
    internal static class CursorsUtils
    {
        /// <summary>
        ///     Path of the cursors folder.
        /// </summary>
        public static readonly string CusorPath = "Cursors/";

        /// <summary>
        ///     Gets a new Cursor.
        /// </summary>
        /// <param name="name">Name of the cursor with its extension.</param>
        /// <returns></returns>
        private static Cursor GetCursor(string name)
        {
            return new Cursor(Path.Combine(Directory.GetCurrentDirectory(), CusorPath + name));
        }

        public static Cursor Arrow()
        {
            return GetCursor("Arrow.cur");
        }

        public static Cursor Busy()
        {
            return GetCursor("Busy.ani");
        }

        public static Cursor Handwriting()
        {
            return GetCursor("Handwriting.cur");
        }

        public static Cursor HelpSelect()
        {
            return GetCursor("HelpSelect.cur");
        }

        public static Cursor Link()
        {
            return GetCursor("Link.cur");
        }

        public static Cursor Move()
        {
            return GetCursor("Move.cur");
        }

        public static Cursor Aim()
        {
            return GetCursor("Aim.cur");
        }

        public static Cursor Loading()
        {
            return GetCursor("Loading.ani");
        }

        public static Cursor Text()
        {
            return GetCursor("Text.cur");
        }

        public static Cursor Unavailable()
        {
            return GetCursor("Unavailable.ani");
        }
    }
}