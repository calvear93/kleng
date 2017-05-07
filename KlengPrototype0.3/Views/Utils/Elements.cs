using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Kleng.Views.Utils
{
    /// <summary>
    ///     Manages some properties of UI elements easily.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.1</version>
    internal static class Elements
    {
        /// <summary>
        ///     Disables UI elements.
        /// </summary>
        /// <param name="elements">UI elements to disable.</param>
        public static void Disable(params UIElement[] elements)
        {
            foreach (var element in elements)
                element.IsEnabled = false;
        }

        /// <summary>
        ///     Enables UI elements.
        /// </summary>
        /// <param name="elements">UI elements to enable.</param>
        public static void Enable(params UIElement[] elements)
        {
            foreach (var element in elements)
                element.IsEnabled = true;
        }

        /// <summary>
        ///     Shows UI elements.
        /// </summary>
        /// <param name="elements">UI elements to show.</param>
        public static void Show(params UIElement[] elements)
        {
            foreach (var element in elements)
                element.Visibility = Visibility.Visible;
        }

        /// <summary>
        ///     Hides UI elements.
        /// </summary>
        /// <param name="elements">UI elements to hide.</param>
        public static void Hide(params UIElement[] elements)
        {
            foreach (var element in elements)
                element.Visibility = Visibility.Hidden;
        }

        /// <summary>
        ///     Collapses UI elements.
        /// </summary>
        /// <param name="elements">UI elements to collapse.</param>
        public static void Collapse(params UIElement[] elements)
        {
            foreach (var element in elements)
                element.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        ///     Sets an image.
        /// </summary>
        /// <param name="image">Image.</param>
        /// <param name="uri">URI of the image.</param>
        public static void SetImageSource(Image image, string uri)
        {
            image.Source = new BitmapImage(new Uri(uri, UriKind.Relative));
        }

        /// <summary>
        ///     Sets a default (black) label for textblock.
        /// </summary>
        /// <param name="label">Texblock.</param>
        /// <param name="text">Text.</param>
        public static void SetLabelDefault(TextBlock label, string text)
        {
            label.Text = text;
            label.Foreground = Brushes.Black;
        }

        /// <summary>
        ///     Sets a green label label for textblock.
        /// </summary>
        /// <param name="label">Texblock.</param>
        /// <param name="text">Text.</param>
        public static void SetLabelAccomplished(TextBlock label, string text)
        {
            label.Text = "[*] " + text + ".";
            label.Foreground = Brushes.DarkGreen;
        }

        /// <summary>
        ///     Sets a blue label for textblock.
        /// </summary>
        /// <param name="label">Texblock.</param>
        /// <param name="text">Text.</param>
        public static void SetLabelInfo(TextBlock label, string text)
        {
            label.Text = "[i] " + text + ".";
            label.Foreground = Brushes.DarkBlue;
        }

        /// <summary>
        ///     Sets a orange label for textblock.
        /// </summary>
        /// <param name="label">Texblock.</param>
        /// <param name="text">Text.</param>
        public static void SetLabelWarning(TextBlock label, string text)
        {
            label.Text = "[i] " + text + ".";
            label.Foreground = Brushes.DarkOrange;
        }

        /// <summary>
        ///     Sets a red label for textblock.
        /// </summary>
        /// <param name="label">Texblock.</param>
        /// <param name="text">Text.</param>
        public static void SetLabelError(TextBlock label, string text)
        {
            label.Text = "[Err] " + text + ".";
            label.Foreground = Brushes.DarkRed;
        }

        /// <summary>
        ///     Sets a olive green label for textblock.
        /// </summary>
        /// <param name="label">Texblock.</param>
        /// <param name="text">Text.</param>
        public static void SetLabelStatusInProgress(TextBlock label, string text)
        {
            label.Text = text;
            label.Foreground = Brushes.DarkOliveGreen;
        }

        /// <summary>
        ///     Sets a green label for textblock.
        /// </summary>
        /// <param name="label">Texblock.</param>
        /// <param name="text">Text.</param>
        public static void SetLabelStatusFinished(TextBlock label, string text)
        {
            label.Text = text;
            label.Foreground = Brushes.Green;
        }

        /// <summary>
        ///     Sets a dark red label for textblock.
        /// </summary>
        /// <param name="label">Texblock.</param>
        /// <param name="text">Text.</param>
        public static void SetLabelStatusStopped(TextBlock label, string text)
        {
            label.Text = text;
            label.Foreground = Brushes.DarkRed;
        }
    }
}