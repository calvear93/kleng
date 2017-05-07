using System.Windows;
using System.Windows.Media;
using BespokeFusion;
using Kleng.Components;

namespace Kleng.Views.Utils
{
    /// <summary>
    ///     Wrapping class of BespokeFusion for shows
    ///     message box in a material design way.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.1.2</version>
    internal static class MessageUtils
    {
        /// <summary>
        ///     Shows a material design message box.
        /// </summary>
        /// <param name="title">Title of the message box.</param>
        /// <param name="colorTitle">Color of the title.</param>
        /// <param name="sizeTitle">Font size of the title.</param>
        /// <param name="msg">Message in the body.</param>
        /// <param name="colorMsg">Color of the message.</param>
        /// <param name="sizeMsg">Font size of the message.</param>
        /// <param name="colorBorder">Border color.</param>
        /// <param name="contentPrimaryButton">Primary (left) button text.</param>
        /// <param name="colorPrimaryButton">Primary button color.</param>
        /// <param name="copyToClipboardHidden">Copy to clipboard button visibility.</param>
        /// <param name="contentSecondaryButton">Secondary (right) button text.</param>
        /// <param name="colorSecondaryButton">Secondary button color.</param>
        /// <param name="secondaryHidden">Secondary button visibility.</param>
        /// <returns>Custom material message box. To shows it call to .Show().</returns>
        public static CustomMaterialMessageBox Message(string title, Brush colorTitle, int sizeTitle,
            string msg, Brush colorMsg, int sizeMsg, Brush colorBorder,
            string contentPrimaryButton, Brush colorPrimaryButton, bool copyToClipboardHidden = true,
            string contentSecondaryButton = "", Brush colorSecondaryButton = null, bool secondaryHidden = true)
        {
            return new CustomMaterialMessageBox
            {
                Cursor = CursorsUtils.Arrow(),
                TxtTitle = {Text = title, Foreground = colorTitle, FontSize = sizeTitle},
                TxtMessage = {Text = msg, Foreground = colorMsg, FontSize = sizeMsg},
                TitleBackgroundPanel = {Background = colorBorder},
                BorderBrush = colorBorder,
                BtnCancel =
                {
                    Height = 48,
                    Width = 128,
                    FontSize = 18,
                    Content = contentPrimaryButton,
                    Background = colorPrimaryButton,
                    Cursor = CursorsUtils.Link()
                },
                BtnOk =
                {
                    Height = 48,
                    Width = 128,
                    FontSize = 18,
                    Content = contentSecondaryButton,
                    Background = colorSecondaryButton,
                    Cursor = CursorsUtils.Link(),
                    Visibility = secondaryHidden ? Visibility.Hidden : Visibility.Visible
                },
                BtnCopyMessage =
                {
                    ToolTip = "Copiar al Portapapeles",
                    Cursor = CursorsUtils.Link(),
                    Visibility = copyToClipboardHidden ? Visibility.Hidden : Visibility.Visible
                }
            };
        }

        /// <summary>
        ///     Information message box.
        /// </summary>
        /// <param name="title">Title of the message box.</param>
        /// <param name="msg">Text of the message.</param>
        public static void ShowInfo(string title, string msg)
        {
            FileUtils.PlayAudioFile("Sounds/ui/affirm.wav");
            Message(title, Brushes.White, 16, msg, Brushes.Black, 16, Brushes.RoyalBlue, "Aceptar", Brushes.RoyalBlue)
                .Show();
        }

        /// <summary>
        ///     Warning message box.
        /// </summary>
        /// <param name="title">Title of the message box.</param>
        /// <param name="msg">Text of the message.</param>
        public static void ShowWarn(string title, string msg)
        {
            FileUtils.PlayAudioFile("Sounds/ui/deny.wav");
            Message(title, Brushes.White, 16, msg, Brushes.Black, 16, Brushes.DarkOrange, "Aceptar", Brushes.DarkOrange)
                .Show();
        }

        /// <summary>
        ///     Error message box.
        /// </summary>
        /// <param name="title">Title of the message box.</param>
        /// <param name="msg">Text of the message.</param>
        public static void ShowError(string title, string msg)
        {
            FileUtils.PlayAudioFile("Sounds/ui/error.wav");
            Message(title, Brushes.White, 16, msg, Brushes.Black, 16, Brushes.Red, "Aceptar", Brushes.Red).Show();
        }

        /// <summary>
        ///     Successful message box.
        /// </summary>
        /// <param name="title">Title of the message box.</param>
        /// <param name="msg">Text of the message.</param>
        public static void ShowSuccess(string title, string msg)
        {
            FileUtils.PlayAudioFile("Sounds/ui/success.wav");
            Message(title, Brushes.White, 16, msg, Brushes.Black, 16, Brushes.DarkGreen, "Aceptar", Brushes.DarkGreen)
                .Show();
        }

        /// <summary>
        ///     Confirmation message box.
        /// </summary>
        /// <param name="title">Title of the message box.</param>
        /// <param name="msg">Text of the message.</param>
        /// <returns>True if Ok button is pressed, otherwise false.</returns>
        public static bool ShowConfirmation(string title, string msg)
        {
            FileUtils.PlayAudioFile("Sounds/ui/affirm.wav");
            var mbox = Message(title, Brushes.White, 16, msg, Brushes.Black, 16, Brushes.RoyalBlue, "No", Brushes.Gray,
                true, "Si", Brushes.RoyalBlue, false);
            mbox.Show();
            return mbox.Result == MessageBoxResult.OK;
        }
    }
}