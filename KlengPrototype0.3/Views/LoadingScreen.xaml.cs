using System;
using System.ComponentModel;
using System.Windows;
using Kleng.Components;
using Kleng.Views.Utils;

namespace Kleng.Views
{
    /// <summary>
    ///     Interaction logical segment for LoadingScreen.xaml
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>2.0.1</version>
    public partial class LoadingScreen : Window
    {
        private readonly Window _parent;
        private readonly TimedThreadHandler _timer;

        public LoadingScreen(Window parent)
        {
            InitializeComponent();
            Animations.Zoom(Logo, 1, 1, 0.6, true, false, true);
            _parent = parent;
            _timer = new TimedThreadHandler(30)
            {
                IntervalElapsed = OnResourcesLoad
            };
            _timer.Start();
            Logo.Cursor = CursorsUtils.Busy();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Progress_Bar.Value = 0;
        }

        public void OnResourcesLoad(object sender, EventArgs e)
        {
            Progress_Bar.Value++;
            if (Progress_Bar.Value >= 60)
                Progress_Bar.Value++;
            if (Progress_Bar.Value >= 100)
            {
                _parent.Show();
                Close();
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _timer.Stop();
            _timer.Dispose();
        }
    }
}