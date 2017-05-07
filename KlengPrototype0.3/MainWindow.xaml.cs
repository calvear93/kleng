using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Kleng.Components;
using Kleng.Views;
using Kleng.Views.Utils;
using MaterialDesignThemes.Wpf;
using Microsoft.Kinect;

namespace Kleng
{
    /// <summary>
    ///     Interaction logical segment for MainWindow.xaml
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>3.7.3.1</version>
    public partial class MainWindow : Window
    {
        private readonly SnackbarMessageQueue _msgQueue;
        private KinectController _kinect;
        private string _state;

        public MainWindow()
        {
            InitializeComponent();
            Hide();
            new LoadingScreen(this).Show();
            _msgQueue = status.MessageQueue;
        }

        public event EventHandler KinectDisconnected;

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                _kinect.Stop();
                _kinect.Dispose();
            }
            catch
            {
                // ignored
            }
            Environment.Exit(0);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Cursor = CursorsUtils.Arrow();
            Close_Button.Cursor = CursorsUtils.Link();
            Reading_Button.Cursor = CursorsUtils.Link();
            ParingTerms_Button.Cursor = CursorsUtils.Link();
            About_Button.Cursor = CursorsUtils.HelpSelect();
            Help_Button.Cursor = CursorsUtils.HelpSelect();

            try
            {
                _kinect = new KinectController
                {
                    StatusChanged = OnStatusChanged
                };
                _kinect.Start();
                Task.Factory.StartNew(() => _msgQueue.Enqueue("Kinect conectado."));
                Animations.StopBlinking(kinect_status);
                Elements.SetImageSource(kinect_status, @"\Views\Resources\Icons\kinect_connected.png");
                _state = "Connected";
            }
            catch
            {
                Task.Factory.StartNew(() => _msgQueue.Enqueue("Kinect deconectado."));
                Animations.StartBlinking(kinect_status, 1);
                Elements.SetImageSource(kinect_status, @"\Views\Resources\Icons\kinect_disconnected.png");
                _state = "Disconnected";
            }
        }

        private void Reading_Click(object sender, RoutedEventArgs e)
        {
            if (_state != "Disconnected")
            {
                FileUtils.PlayAudioFile("Sounds/ui/affirm.wav");
                Hide();
                new ReadingView(this, _kinect).Show();
            }
            else
            {
                MessageUtils.ShowWarn("Problema con Kinect.", "¡Ningun Kinect conectado! Conecte uno al puerto USB.");
            }
        }

        private void PairedTerms_Click(object sender, RoutedEventArgs e)
        {
            if (_state != "Disconnected")
            {
                FileUtils.PlayAudioFile("Sounds/ui/affirm.wav");
                Hide();
                new PairingTermsView(this, _kinect).Show();
            }
            else
            {
                MessageUtils.ShowWarn("Problema con Kinect.", "¡Ningun Kinect conectado! Conecte uno al puerto USB.");
            }
        }

        private void OnStatusChanged(object sender, StatusChangedEventArgs e)
        {
            switch (e.Status.ToString())
            {
                case "Connected":
                    Animations.StopBlinking(kinect_status);
                    Task.Factory.StartNew(() => _msgQueue.Enqueue("Kinect conectado."));
                    Elements.SetImageSource(kinect_status, @"\Views\Resources\Icons\kinect_connected.png");
                    _kinect.Connect();
                    _state = "Connected";
                    break;
                case "Initializing":
                    Animations.StopBlinking(kinect_status);
                    Task.Factory.StartNew(() => _msgQueue.Enqueue("Inicializando Kinect."));
                    Elements.SetImageSource(kinect_status, @"\Views\Resources\Icons\kinect_initializing.png");
                    break;
                case "Disconnected":
                    Task.Factory.StartNew(() => _msgQueue.Enqueue("Kinect deconectado."));
                    Animations.StartBlinking(kinect_status, 1);
                    Elements.SetImageSource(kinect_status, @"\Views\Resources\Icons\kinect_disconnected.png");
                    _state = "Disconnected";
                    break;
                case "NotPowered":
                    Task.Factory.StartNew(() => _msgQueue.Enqueue("Conecte su Kinect a alguna fuente de energía."));
                    Animations.StartBlinking(kinect_status, 1);
                    Elements.SetImageSource(kinect_status, @"\Views\Resources\Icons\kinect_disconnected.png");
                    _state = "Disconnected";
                    break;
                default:
                    //Task.Factory.StartNew(() => _msg_queue.Enqueue("Error al conectar dispositivo."));
                    Animations.StartBlinking(kinect_status, 1);
                    Elements.SetImageSource(kinect_status, @"\Views\Resources\Icons\kinect_disconnected.png");
                    _state = "Disconnected";
                    break;
            }
            KinectDisconnected?.Invoke(this, null);
        }

        private void Close_Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageUtils.ShowConfirmation("Salir de Kleng", "¿Está seguro que desea salir de Kleng?"))
                Close();
        }

        private void Author_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            new AboutView(this).Show();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            Animations.Zoom(Logo, 2, 1, 1.2, true, false, true);
            Animations.Zoom(Close_Button, 1, 0.4, 1, false, false, true);
        }

        private void Help(object sender, EventArgs e)
        {
            FileUtils.PlayAudioFile("Sounds/help/welcome.wav");
        }
    }
}