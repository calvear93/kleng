using System;
using System.Windows;
using Kleng.Components;
using Kleng.Components.VoiceRecognizer;
using Kleng.Views.Utils;

namespace Kleng.Views
{
    /// <summary>
    ///     Interaction logical segment for ReadingActivityView.xaml
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.0</version>
    public partial class ReadingActivityView : Window
    {
        public ReadingActivityView(Reading reading)
        {
            InitializeComponent();
            Topmost = true;
            reading_name.Text = reading.Title;
            reading_text.Text = reading.RawText;
            Help_Button.Cursor = CursorsUtils.HelpSelect();
            reading_text.Cursor = CursorsUtils.Link();
        }

        private void Help(object sender, EventArgs e)
        {
            FileUtils.PlayAudioFile("Sounds/help/reading_view.wav");
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Topmost = true;
            Activate();
        }
    }
}