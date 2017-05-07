using System.IO;
using System.Media;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Kleng.Components
{
    /// <summary>
    ///     Gives some utilities for handles directories and
    ///     file chooser dialogs.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.1.4</version>
    internal static class FileUtils
    {
        #region DIRECTORY & FILE CHOOSER

        /// <summary>
        ///     Creates a directory.
        /// </summary>
        /// <param name="path">URI of the directory.</param>
        public static void CreateDirectory(string path)
        {
            if (Directory.Exists(path))
                return;

            Directory.CreateDirectory(path);
        }

        /// <summary>
        ///     Shows a file chooser dialog for save a file.
        /// </summary>
        /// <param name="title">Window's title.</param>
        /// <param name="filter">Extension filter.</param>
        /// <returns></returns>
        public static string SaveFile(string title, string filter)
        {
            var dialog = new SaveFileDialog
            {
                Title = title,
                Filter = filter
            };
            dialog.ShowDialog();

            return dialog.FileName;
        }

        /// <summary>
        ///     Shows a file chooser dialog for load a file.
        /// </summary>
        /// <param name="title">Window's title.</param>
        /// <param name="extension">Default extension.</param>
        /// <param name="filter">Extension filter.</param>
        /// <returns></returns>
        public static string LoadFile(string title, string extension, string filter)
        {
            var dialog = new OpenFileDialog
            {
                Title = title,
                DefaultExt = extension,
                Filter = filter
            };
            dialog.ShowDialog();

            return dialog.FileName;
        }

        #endregion

        #region READ-WRITE FILES

        /// <summary>
        ///     Deletes the file.
        /// </summary>
        /// <param name="uri">URI of the file.</param>
        public static void DeleteFile(string uri)
        {
            if (File.Exists(uri))
                File.Delete(uri);
        }

        /// <summary>
        ///     Copies the file to other directory.
        /// </summary>
        /// <param name="uri">URI of the file.</param>
        /// <param name="new_uri">New URI destination.</param>
        public static void CopyFile(string uri, string new_uri)
        {
            if (File.Exists(uri))
                File.Copy(uri, new_uri);
        }

        /// <summary>
        ///     Moves (cuts) the file to other directory.
        /// </summary>
        /// <param name="uri">URI of the file.</param>
        /// <param name="new_uri">New URI destination.</param>
        public static void MoveFile(string uri, string new_uri)
        {
            if (File.Exists(uri))
                File.Move(uri, new_uri);
        }

        /// <summary>
        ///     Gets the media player object after reads the audio file.
        /// </summary>
        /// <param name="uri">URI of the file.</param>
        /// <returns>Media player object with the audio file.</returns>
        public static SoundPlayer ReadAudioFile(string uri)
        {
            if (!File.Exists(uri))
                return null;

            var media = new SoundPlayer(uri);
            media.Load();

            return media;
        }

        /// <summary>
        ///     Plays the audio file.
        /// </summary>
        /// <param name="uri">URI of the file.</param>
        public static void PlayAudioFile(string uri)
        {
            Task.Factory.StartNew(() => { ReadAudioFile(uri)?.PlaySync(); });
        }

        #endregion
    }
}