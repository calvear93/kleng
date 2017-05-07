using System;
using System.IO;
using System.Linq;
using System.Windows;
using Kleng.Components.Exceptions;
using Microsoft.Kinect;
using NAudio.Wave;

namespace Kleng.Components
{
    /// <summary>
    ///     Controller of the Kinect sensor, eases the management of the
    ///     audio stream processing, voice recognition and cameras initialization.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>3.4.2</version>
    public class KinectController : IDisposable
    {
        /// <summary>
        ///     Creates the Kinect controller.
        /// </summary>
        public KinectController()
        {
            // Gets the Kinect sensor connected.
            Connect();
        }

        #region PROPERTIES

        /// <summary>
        ///     Stores the Kinect sensor object.
        /// </summary>
        public KinectSensor Sensor { get; private set; }

        /// <summary>
        ///     Audio stream from Kinect sensor.
        /// </summary>
        public Stream AudioStream { get; private set; }

        /// <summary>
        ///     Skeleton stream from Kinect sensor.
        /// </summary>
        public SkeletonStream SkeletonStream { get; private set; }

        /// <summary>
        ///     Allows recording audio from primary audio source.
        /// </summary>
        private WaveIn _waveInStream;

        /// <summary>
        ///     Writes the raw data from primary audio source to a PCM file (WAV).
        /// </summary>
        private WaveFileWriter _audioWriter;

        #endregion

        #region EVENTS

        /// <summary>
        ///     Subscribes a function to Status Changed event from Kinect.
        ///     Is recommended to use OnStatusChanged(object sender, StatusChangedEventArgs e)
        ///     header for listener function.
        /// </summary>
        public EventHandler<StatusChangedEventArgs> StatusChanged
        {
            set { KinectSensor.KinectSensors.StatusChanged += value; }
        }

        /// <summary>
        ///     Subscribes a function to Skeleton Frame Ready event.
        ///     Is recommended to use OnSkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        ///     header for listener function.
        ///     <precondition>Sensor must be connected and initialized.</precondition>
        /// </summary>
        public EventHandler<SkeletonFrameReadyEventArgs> SkeletonFrameReady
        {
            set { Sensor.SkeletonFrameReady += value; }
        }

        #endregion

        #region SENSOR

        /// <summary>
        ///     Tries to initialize and connect to a Kinect sensor connected to USB.
        /// </summary>
        public void Connect()
        {
            // Gets the Kinect sensor connected.
            Sensor = KinectSensor.KinectSensors.FirstOrDefault(sensor => sensor.Status == KinectStatus.Connected);
        }

        /// <summary>
        ///     Starts Kinect sensor.
        /// </summary>
        public void Start()
        {
            // If sensor isn't initialized or not found, throws an exception.
            if (Sensor == null)
                throw new KinectNotInitializedException(
                    "Kinect sensor not initialized or disconnected.\n\t[MISSING FUNCTION]: Connect.");

            // Starts Kinect sensor.
            Sensor.Start();
            try
            {
                Sensor.ElevationAngle = KinectCapabilities.InitAngle;
            }
            catch
            {
                Console.WriteLine("Kinect Init Angle failed to set up!");
            }
        }

        /// <summary>
        ///     Stops Kinect sensor.
        /// </summary>
        public void Stop()
        {
            // If sensor isn't initialized or not found, throws an exception.
            if (Sensor == null)
                throw new KinectNotInitializedException(
                    "Kinect sensor not initialized or disconnected.\n\t[MISSING FUNCTION]: Connect.");

            // Stops Kinect sensor.
            Sensor.Stop();
        }

        /// <summary>
        ///     Stops Kinect sensor and remove all references from it.
        /// </summary>
        public void Dispose()
        {
            // Disposes the audio writer.
            _audioWriter?.Dispose();
            // Disposes the Kinect sensor.
            Sensor.Stop();
            Sensor.Dispose();
        }

        #endregion

        #region AUDIO ENGINE

        /// <summary>
        ///     Initializes and starts the audio source for streaming.
        /// </summary>
        public void StartAudioEngine()
        {
            // If sensor isn't initialized or not found, throws an exception.
            if (Sensor == null)
                throw new KinectNotInitializedException(
                    "Kinect sensor not initialized or disconnected.\n\t[MISSING FUNCTION]: Connect.");

            if (!Sensor.IsRunning)
                throw new KinectNotStartedException("Kinect sensor not started.\n\t[MISSING FUNCTION]: Start.");

            // Sets audio source features.
            Sensor.AudioSource.AutomaticGainControlEnabled = KinectCapabilities.AutomaticGain;
            Sensor.AudioSource.EchoCancellationMode = KinectCapabilities.EchoCancellation;
            Sensor.AudioSource.BeamAngleMode = KinectCapabilities.BeamAngle;
            // Begins the audio streaming and stores it.
            AudioStream = Sensor.AudioSource.Start();
        }

        /// <summary>
        ///     Stops the audio engine.
        /// </summary>
        public void StopAudioEngine()
        {
            // If sensor isn't initialized or not found, throws an exception.
            if (Sensor == null)
                throw new KinectNotInitializedException(
                    "Kinect sensor not initialized or disconnected.\n\t[MISSING FUNCTION]: Connect.");

            // If recorder engine isn't initialized, skips.
            if (AudioStream == null)
                return;

            // Disposes audio stream from Kinect.
            Sensor.AudioSource.Stop();
            AudioStream.Dispose();
            AudioStream = null;
        }

        /// <summary>
        ///     Initializes the audio stream recording into a file.
        /// </summary>
        /// <param name="uri">Uniform Resource Identifier (Path or directory) where audio file will be saved.</param>
        /// <param name="name">Name of the audio file.</param>
        public void InitializeVoiceRecorder(string uri, string name)
        {
            // Creates the reading and writing objects.
            _waveInStream = new WaveIn
            {
                WaveFormat =
                    new WaveFormat(KinectCapabilities.SamplesPerSecond, KinectCapabilities.BitsPerSample,
                        KinectCapabilities.RecordingChannels)
            };
            _audioWriter = new WaveFileWriter(uri + "/" + name + ".wav", _waveInStream.WaveFormat);

            // Handles audio events.
            _waveInStream.DataAvailable += OnDataAvailable;
        }

        /// <summary>
        ///     Starts the audio recorder.
        /// </summary>
        public void StartRecording()
        {
            // If recorder engine isn't initialized, throws an exception.
            if (_waveInStream == null || _audioWriter == null)
                throw new RecorderNotInializedException(
                    "Recorder engine not initialized.\n\t[MISSING FUNCTION]: InitializeVoiceRecorder.");

            // Starts the writing from Kinect audio streaming.
            _waveInStream.StartRecording();
        }

        /// <summary>
        ///     Stops the audio recorder.
        /// </summary>
        public void StopRecording()
        {
            // If recorder engine isn't initialized, throws an exception.
            if (_waveInStream == null)
                throw new RecorderNotInializedException(
                    "Recorder engine not initialized.\n\t[MISSING FUNCTION]: InitializeVoiceRecorder.");

            // Stops the writing from Kinect audio streaming.
            _waveInStream.StopRecording();
            _waveInStream.Dispose();
            _audioWriter.Close();
            _audioWriter.Dispose();
        }

        /// <summary>
        ///     Writes the raw audio data into a file. The function is fired when
        ///     any data for write is available in audio stream.
        /// </summary>
        /// <param name="sender">Object triggering to the function.</param>
        /// <param name="e">Event object with important information.</param>
        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            // Writes the bytes from the audio stream.
            _audioWriter.Write(e.Buffer, 0, e.BytesRecorded);
        }

        #endregion

        #region SKELETON TRACKING

        /// <summary>
        ///     Initializes and starts the skeleton tracking system.
        /// </summary>
        public void StartSkeletonTrackingEngine()
        {
            // If sensor isn't initialized or not found, throws an exception.
            if (Sensor == null)
                throw new KinectNotInitializedException("Kinect sensor not initialized or disconnected.");

            // Sets the depth mode for sensor viewing.
            Sensor.DepthStream.Range = KinectCapabilities.DepthStreamRange;
            // Sets near range mode.
            Sensor.SkeletonStream.EnableTrackingInNearRange = KinectCapabilities.NearModeRange;
            // Enable skeleton (camera) stream.
            Sensor.SkeletonStream.Enable();
            // Stores the skeleton tracking stream to a property.
            SkeletonStream = Sensor.SkeletonStream;
        }

        /// <summary>
        ///     Stops the skeleton tracking system.
        /// </summary>
        public void StopSkeletonTrackingEngine()
        {
            // If sensor isn't initialized or not found, throws an exception.
            if (Sensor == null)
                throw new KinectNotInitializedException("Kinect sensor not initialized or disconnected.");

            // Enable skeleton (camera) stream.
            Sensor.SkeletonStream.Disable();
        }

        /// <summary>
        ///     Map out the joint position (X and Y coordinates) on the given dimensions.
        /// </summary>
        /// <param name="joint">Tracked part from the skeleton.</param>
        /// <param name="width">Width of the mapping.</param>
        /// <param name="height">Height of the mapping.</param>
        /// <returns>Mapped point.</returns>
        public Point MapSkeletonPoint(Joint joint, double width, double height)
        {
            var jointDepthImagePoint = Sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(joint.Position,
                KinectCapabilities.ImageFormat);
            return new Point(jointDepthImagePoint.X*(width/Sensor.DepthStream.FrameWidth),
                jointDepthImagePoint.Y*(height/Sensor.DepthStream.FrameHeight));
        }

        #endregion
    }
}