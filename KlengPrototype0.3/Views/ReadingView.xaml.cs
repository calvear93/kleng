using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Kleng.Components;
using Kleng.Components.Events;
using Kleng.Components.VoiceRecognizer;
using Kleng.Views.Utils;
using Microsoft.Speech.Recognition;

namespace Kleng.Views
{
    /// <summary>
    ///     Interaction logical segment for ReadingView.xaml
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>3.4.11</version>
    public partial class ReadingView : Window
    {
        private const string TempDir = "temp";
        private readonly KinectController _kinect;
        private readonly MainWindow _parent;
        private string _lastAudio;
        private bool _playVisible;
        private Reading _reading;
        private VoiceRecognizer _recognizer;
        private Stopwatch _timer;
        private TimedThreadHandler _timerHandler;
        private int _wrongs, _wrongsGet;
        private bool _helpingActive;
        private ReadingActivityView _rView;

        public ReadingView(MainWindow parent, KinectController kinect)
        {
            InitializeComponent();
            _parent = parent;
            _kinect = kinect;
            _parent.KinectDisconnected += Kinect_Disconnected;
            _helpingActive = false;
            FileUtils.CreateDirectory(TempDir);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _playVisible = false;
            try
            {
                _recognizer = new VoiceRecognizer
                {
                    SpeechRecognized = OnKinectSpeechRecognized,
                    SpeechRejected = OnKinectSpeechRejected
                };
            }
            catch
            {
                MessageUtils.ShowError("Problema con Speech Engine.",
                    "¡Error al intentar cargar lenguaje!. Descargue e instale los paquetes de lenguaje: https://www.microsoft.com/en-us/download/details.aspx?id=27226 - https://www.microsoft.com/en-us/download/details.aspx?id=34809");
                Environment.Exit(0);
            }

            VerifyLanguagePack();
            _timer = new Stopwatch();
            _timerHandler = new TimedThreadHandler(1000)
            {
                IntervalElapsed = OnEachSecond
            };
            _kinect.Start();

            SetCursors();
        }

        private void load_Click(object sender, RoutedEventArgs e)
        {
            if (Run_Help("load_button")) return;
            _rView?.Close();
            var readingPath = FileUtils.LoadFile("Cargar Lectura", ".txt", "Lectura (.txt)|*.txt");

            if (_kinect.AudioStream != null)
                _kinect.StopAudioEngine();
            _kinect.StartAudioEngine();

            if (readingPath == string.Empty)
                return;
            try
            {
                _reading = new Reading(readingPath, KinectCapabilities.ConfidenceRequired);
            }
            catch
            {
                MessageUtils.ShowError("Error en formato de lectura.",
                    "¡Error al intentar cargar lectura! Formato no soportado.");
                return;
            }

            _reading.MatchedStringEvent += OnMatchedString;
            _recognizer.InitializeRecognitionFromStream(_kinect.AudioStream, _reading.GetGrammarScheme());
            reading_name.Text = _reading.Title;
            Elements.Enable(play);
            Elements.Disable(load, save_audio, Help_Button, SaveReport);
            Elements.SetLabelStatusFinished(reading_status, "Cargada.");
            correct_words.Text = "0/" + _reading.Words.Length;
            time.Text = "00:00:00";
            wrong_words.Text = "0";
            last_word.Text = "";
            modulation.Text = "0.0";

            _rView = new ReadingActivityView(_reading);
            _rView.Show();
            FileUtils.PlayAudioFile("Sounds/ui/load.wav");
        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            if (Run_Help("play_button")) return;
            Topmost = true;
            _playVisible = true;
            FileUtils.PlayAudioFile("Sounds/ui/affirm.wav");
            if (save_audio.IsChecked == true)
            {
                _lastAudio = "recording" + DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss");
                _kinect.InitializeVoiceRecorder(TempDir + "/", _lastAudio);
                _kinect.StartRecording();
                Elements.Disable(save_audio);
            }
            _recognizer.Start();

            Animations.Highlight(play);
            Elements.Enable(stop);
            Elements.Disable(load, play, Back_Button);
            Animations.SoftRotate(hourglass, 1, 0, 180);
            Elements.SetLabelStatusInProgress(reading_status, "EN PROGRESO...");

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

        private void VerifyLanguagePack()
        {
            switch (_recognizer.Language)
            {
                case "es-MX":
                    Elements.SetImageSource(language_status, @"\Views\Resources\Languages\es-MX.png");
                    break;
                case "es-ES":
                    Elements.SetImageSource(language_status, @"\Views\Resources\Languages\es-ES.png");
                    break;
                default:
                    MessageUtils.ShowError("Problema con Speech Engine.",
                        "¡Error al intentar cargar lenguaje!. Descargue e instale los paquetes de lenguaje: https://www.microsoft.com/en-us/download/details.aspx?id=27226 - https://www.microsoft.com/en-us/download/details.aspx?id=34809");
                    break;
            }
        }

        public void OnKinectSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            Console.WriteLine("KL (Debug) : " + e.Result.Text + " - Confidence :" + e.Result.Confidence);
            _reading.Check(e.Result.Text, e.Result.Confidence);
        }

        public void OnKinectSpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            _wrongs++;
            wrong_words.Text = "" + (_wrongsGet + _wrongs);
            last_word.Text = "";
        }

        public void OnEachSecond(object sender, EventArgs e)
        {
            time.Text = _timer.Elapsed.ToString(@"hh\:mm\:ss");
        }

        public void OnMatchedString(object sender, MatchedStringEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                if (e.FinalReached)
                {
                    Finish("FINALIZADA...");
                    FormatTextShowing();
                    correct_words.Text = e.Successes + "/" + _reading.Words.Length;
                    wrong_words.Text = (e.Wrongs + _wrongs).ToString();
                    Animations.Zoom(correct_words, 0.25, 1, 2, true, false, true);
                    Animations.Zoom(wrong_words, 0.25, 1, 2, true, false, true);

                    double m = ((int) ((e.Score)*7)) + 1;

                    if (m > 7) m = 7;
                    modulation.Text = m.ToString(CultureInfo.InvariantCulture);

                    _wrongsGet = e.Wrongs;

                    last_word.Text = _reading.Words[e.WordPointer];
                    Animations.Zoom(last_word, 0.25, 1, 2, true, false, true);
                    _recognizer.Stop();
                }
                else
                {
                    correct_words.Text = e.Successes + "/" + _reading.Words.Length;
                    wrong_words.Text = (e.Wrongs + _wrongs).ToString();
                    _wrongsGet = e.Wrongs;

                    last_word.Text = _reading.Words[e.WordPointer];
                    FormatTextShowing();
                }
            });
        }

        private void FormatTextShowing()
        {
            _rView.Dispatcher.Invoke(() =>
            {
                _rView.reading_text.Inlines.Clear();
                for (var i = 0; i < _reading.RawWords.Length; i++)
                {
                    if (i < _reading.WordPointer)
                    {
                        if (_reading.Scores[i] == 0)
                            _rView.reading_text.Inlines.Add(
                                new Italic(new Run(_reading.RawWords[i] + " ") {Foreground = Brushes.Red}));
                        else
                            _rView.reading_text.Inlines.Add(
                                new Italic(new Run(_reading.RawWords[i] + " ") {Foreground = Brushes.Gray}));
                        continue;
                    }
                    if (i == _reading.WordPointer)
                    {
                        _rView.reading_text.Inlines.Add(
                            new Bold(new Run(_reading.RawWords[i] + " ") {Foreground = Brushes.LimeGreen}));
                        continue;
                    }
                    _rView.reading_text.Inlines.Add(new Run(_reading.RawWords[i] + " ") {Foreground = Brushes.Black});
                }
            });
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _recognizer.Stop();
                _kinect.StopAudioEngine();
                _parent.Show();
                Disable_Help();
                Close();
                _rView?.Close();
            }
            catch
            {
            }
        }

        private void SaveReport_Click(object sender, RoutedEventArgs e)
        {
            if (Run_Help("save_button")) return;
            _rView?.Close();
            FileUtils.PlayAudioFile("Sounds/ui/affirm.wav");
            Hide();
            var result = new Result(reading_name.Text, DateTime.Now.ToString("HH:mm:ss"),
                DateTime.Now.ToString("yyyy-MM-dd"))
            {
                TimeConsuming = time.Text,
                Corrects = correct_words.Text,
                Wrongs = wrong_words.Text,
                Score = modulation.Text
            };
            new SaveReportView(this, result, "reports/reading", TempDir, _lastAudio + ".wav").Show();
            Elements.Enable(Help_Button);
        }

        private void save_audio_Checked(object sender, RoutedEventArgs e)
        {
            if (Run_Help("save_audio_button")) return;
            if (save_audio.IsChecked == true)
                return;

            _lastAudio = null;
        }

        private void Finish(string text)
        {
            FileUtils.PlayAudioFile("Sounds/ui/end.wav");
            _timerHandler.Stop();
            _timer.Stop();
            _timer.Reset();

            Topmost = false;
            _playVisible = false;

            if (save_audio.IsChecked == true)
            {
                _kinect.StopRecording();
                Elements.Enable(save_audio);
            }

            _recognizer.Finish();
            _kinect.StopAudioEngine();

            Animations.StopBlinking(play);

            Elements.Enable(load, SaveReport, save_audio, Back_Button);
            Elements.Disable(play, stop);
            Animations.SoftRotate(hourglass, 1, 180, 360);
            Elements.SetLabelStatusFinished(reading_status, text);
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
                _rView?.Close();
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
            save_audio.Cursor = CursorsUtils.Link();
            load.Cursor = CursorsUtils.Link();
            play.Cursor = CursorsUtils.Link();
            stop.Cursor = CursorsUtils.Link();
            Help_Button.Cursor = CursorsUtils.HelpSelect();
        }

        private void Help(object sender, EventArgs e)
        {
            SaveReport.Cursor = CursorsUtils.HelpSelect();
            save_audio.Cursor = CursorsUtils.HelpSelect();
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

        private void Window_Deactivated(object sender, EventArgs e)
        {
            if (!_playVisible) return;
            Topmost = true;
            Activate();
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
    }
}