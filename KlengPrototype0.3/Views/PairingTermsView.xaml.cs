using System;
using System.Diagnostics;
using System.Windows;
using Kleng.Components;
using Kleng.Components.HandControl;
using Kleng.Views.Utils;
using Microsoft.Kinect;

namespace Kleng.Views
{
    /// <summary>
    ///     Interaction logical segment for PairingTermsView.xaml
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.4.2</version>
    public partial class PairingTermsView : Window
    {
        private readonly KinectController _kinect;
        private readonly MainWindow _parent;
        private PairingTerms _pairs;
        private HandControl _nui;
        private bool _playVisible;
        private SkeletonTracker _skeletonTracker;
        private Stopwatch _timer;
        private TimedThreadHandler _timerHandler;
        private bool _helpingActive;
        private PairingTermsActivityView _ptView;

        public PairingTermsView(MainWindow parent, KinectController kinect)
        {
            InitializeComponent();
            _parent = parent;
            _parent.KinectDisconnected += Kinect_Disconnected;
            _kinect = kinect;
            _helpingActive = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _kinect.Start();
            _skeletonTracker = new SkeletonTracker(_kinect);

            _timer = new Stopwatch();
            _timerHandler = new TimedThreadHandler(1000)
            {
                IntervalElapsed = OnEachSecond
            };
            SetCursors();
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            if (Run_Help("load_button")) return;

            var pairsPath = FileUtils.LoadFile("Cargar Lectura", ".txt", "Lectura (.txt)|*.txt");

            if (pairsPath == string.Empty)
                return;
            try
            {
                _pairs = new PairingTerms(pairsPath);
            }
            catch
            {
                MessageUtils.ShowError("Error en formato de lectura.",
                    "¡Error al intentar cargar lectura! Formato no soportado.");
                return;
            }

            Elements.Enable(play);
            Elements.Disable(load, Help_Button, SaveReport);
            Elements.SetLabelStatusFinished(pairing_status, "Cargada.");
            pairing_name.Text = _pairs.Title;
            correct_pairs.Text = "0/" + _pairs.Count;
            wrong_pairs.Text = "0/" + _pairs.Count;
            time.Text = "00:00:00";
            FileUtils.PlayAudioFile("Sounds/ui/affirm.wav");
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            if (Run_Help("play_button")) return;
            FileUtils.PlayAudioFile("Sounds/ui/load.wav");
            Topmost = true;
            _playVisible = true;
            _ptView = new PairingTermsActivityView(this, _pairs);
            _ptView.Show();

            _kinect.StartSkeletonTrackingEngine();

            _nui = new HandControl(_kinect, _skeletonTracker,
                left_handed_mode.IsChecked == true ? JointType.HandLeft : JointType.HandRight);

            Animations.Highlight(play);
            Elements.Enable(stop);
            Elements.Disable(load, play, Back_Button, left_handed_mode);
            Animations.SoftRotate(hourglass, 1, 0, 180);
            Elements.SetLabelStatusInProgress(pairing_status, "EN PROGRESO...");

            Animations.StartBlinking(play, 0.8f);

            _timer.Start();
            _timerHandler.Start();
        }

        private void stop_Click(object sender, RoutedEventArgs e)
        {
            if (Run_Help("stop_button")) return;
            FileUtils.PlayAudioFile("Sounds/ui/deny.wav");
            Finish("CANCELADA...");
        }

        private void SaveReport_Click(object sender, RoutedEventArgs e)
        {
            if (Run_Help("save_button")) return;

            FileUtils.PlayAudioFile("Sounds/ui/affirm.wav");
            Hide();
            var result = new Result(pairing_name.Text, DateTime.Now.ToString("HH:mm:ss"),
                DateTime.Now.ToString("yyyy-MM-dd"))
            {
                TimeConsuming = time.Text,
                Corrects = correct_pairs.Text,
                Wrongs = wrong_pairs.Text
            };
            new SaveReportView(this, result, "reports/pairing").Show();
            Elements.Enable(Help_Button);
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _kinect.StopSkeletonTrackingEngine();
                _parent.Show();
                Disable_Help();
                Close();
                _ptView?.Close();
            }
            catch
            {
            }
        }

        private void left_handed_Checked(object sender, RoutedEventArgs e)
        {
            Run_Help("left_handed_button");
        }

        public void OnEachSecond(object sender, EventArgs e)
        {
            time.Text = _timer.Elapsed.ToString(@"hh\:mm\:ss");
        }

        public void UpdateScores(int correct, int wrongs)
        {
            correct_pairs.Text = correct + "/" + _pairs.Count;
            wrong_pairs.Text = wrongs + "/" + _pairs.Count;
        }

        public void Finish(string text)
        {
            FileUtils.PlayAudioFile("Sounds/ui/end.wav");
            _timerHandler.Stop();
            _timer.Stop();
            _timer.Reset();
            Topmost = false;
            _playVisible = false;

            _kinect.StopSkeletonTrackingEngine();
            _ptView?.Close();
            Animations.StopBlinking(play);

            Elements.Enable(load, SaveReport, Back_Button, left_handed_mode);
            Elements.Disable(play, stop);
            Animations.SoftRotate(hourglass, 1, 180, 360);
            Elements.SetLabelStatusFinished(pairing_status, text);
            Animations.Zoom(time, 0.5, 1, 2, true, false, true);
        }

        private void Kinect_Disconnected(object sender, EventArgs e)
        {
            if (!IsVisible)
                return;
            MessageUtils.ShowError("Kinect desconectada.",
                "¡Kinect desconectada! La aplicación volverá al menú principal.");
            try
            {
                _parent.Show();
                _parent.KinectDisconnected -= Kinect_Disconnected;
                Close();
                _ptView?.Close();
            }
            catch
            {
            }
        }

        private void SetCursors()
        {
            Cursor = CursorsUtils.Arrow();
            Back_Button.Cursor = CursorsUtils.Link();
            SaveReport.Cursor = CursorsUtils.Link();
            load.Cursor = CursorsUtils.Link();
            play.Cursor = CursorsUtils.Link();
            stop.Cursor = CursorsUtils.Link();
            Help_Button.Cursor = CursorsUtils.HelpSelect();
        }

        private void Help(object sender, EventArgs e)
        {
            SaveReport.Cursor = CursorsUtils.HelpSelect();
            load.Cursor = CursorsUtils.HelpSelect();
            play.Cursor = CursorsUtils.HelpSelect();
            stop.Cursor = CursorsUtils.HelpSelect();
            Elements.Enable(play, stop, SaveReport);
            _helpingActive = true;
        }

        private void Disable_Help()
        {
            SetCursors();
            _helpingActive = false;
        }

        private bool Run_Help(string audioHelp)
        {
            if (!_helpingActive)
                return false;
            FileUtils.PlayAudioFile("Sounds/help/" + audioHelp + ".wav");
            Elements.Disable(play, stop, SaveReport);
            Disable_Help();
            return true;
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (!_playVisible) return;
            Topmost = true;
            Activate();
        }
    }
}