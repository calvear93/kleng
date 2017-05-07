using Microsoft.Kinect;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;

namespace Kleng.Components
{
    /// <summary>
    ///     Constants for the Kinect sensor, audio processing and skeleton tracking.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>3.9.1</version>
    internal static class KinectCapabilities
    {
        #region AUDIO CAPABILITIES

        /// <summary>
        ///     Average of bytes processed each second.
        ///     <default>32000</default>
        /// </summary>
        public const int AverageBytesPerSecond = 32000;

        /// <summary>
        ///     Number of bits of the audio in each Kinect audio stream sample.
        ///     <default>16</default>
        /// </summary>
        public const int BitsPerSample = 16;

        /// <summary>
        ///     Number of channels of the streaming from Kinect. Channel count of the audio.
        ///     <default>1</default>
        /// </summary>
        public const int Channels = 1;

        /// <summary>
        ///     Number of channels of the recording from Kinect. Channel count of the record.
        ///     <default>2</default>
        /// </summary>
        public const int RecordingChannels = 2;

        /// <summary>
        ///     Software for playback and recording of audio handles audio data in blocks.
        ///     The sizes of these blocks are multiples of the value of the BlockAlign property.
        ///     Block alignment value is the number of bytes in an atomic unit (that is, a block)
        ///     of audio for a particular format.
        ///     For Pulse Code Modulation (PCM) formats, the formula for calculating
        ///     block alignment is as follows: Block Alignment = Bytes per Sample x Number of Channels.
        ///     <default>2</default>
        /// </summary>
        public const int BlockAlign = 2;

        /// <summary>
        ///     Initial elevation angle for Kinect sensor.
        ///     <default>20;</default>
        /// </summary>
        public const int InitAngle = 20;

        /// <summary>
        ///     Number of samples captured from Kinect audio stream each millisecond.
        ///     <default>16000</default>
        /// </summary>
        public const int SamplesPerSecond = 16000;

        /// <summary>
        ///     Automatic volume gain control.
        ///     <default>false</default>
        /// </summary>
        public const bool AutomaticGain = false;

        /// <summary>
        ///     Format in which audio stream from Kinect is encoded.
        ///     <default>EncodingFormat.Pcm</default>
        /// </summary>
        public const EncodingFormat Format = EncodingFormat.Pcm;

        /// <summary>
        ///     Recognition mode for speech engine.
        ///     <default>RecognizeMode.Multiple</default>
        /// </summary>
        public const RecognizeMode RecognitionMode = RecognizeMode.Multiple;

        /// <summary>
        ///     Echo cancellation and suppression mode.
        ///     <default>EchoCancellationMode.None</default>
        /// </summary>
        public const EchoCancellationMode EchoCancellation = EchoCancellationMode.CancellationAndSuppression;

        /// <summary>
        ///     Beam angle modes, which adjust the beam-forming capabilities for improving audio capture.
        ///     <default>BeamAngleMode.Automatic</default>
        /// </summary>
        public const BeamAngleMode BeamAngle = BeamAngleMode.Automatic;

        /// <summary>
        ///     Confidence required for acceptation of the atoms recognized in reading (voice recognizer).
        ///     <default>0.1</default>
        /// </summary>
        public const double ConfidenceRequired = 0.05;

        #endregion

        #region SKELETON TRACKING CAPABILITIES

        /// <summary>
        ///     Determines how many joints to track.
        ///     <default>SkeletonTrackingMode.Default</default>
        /// </summary>
        public const SkeletonTrackingMode TrackingMode = SkeletonTrackingMode.Default;

        /// <summary>
        ///     Range adjust for the sensor viewing.
        /// </summary>
        /// <default>DepthRange.Default</default>
        public const DepthRange DepthStreamRange = DepthRange.Default;

        /// <summary>
        ///     Image formats for a DepthImageStream. Each image format contains the data type, the resolution, and the frame rate.
        ///     <default>DepthImageFormat.Resolution640x480Fps30</default>
        /// </summary>
        public const DepthImageFormat ImageFormat = DepthImageFormat.Resolution640x480Fps30;

        /// <summary>
        ///     Near range mode.
        ///     <default>false</default>
        /// </summary>
        public const bool NearModeRange = false;

        /// <summary>
        ///     Global size of the joint cache.
        /// </summary>
        /// <default>25</default>
        public const int CacheSize = 25;

        /// <summary>
        ///     Wait time for depth gestures.
        /// </summary>
        /// <default>180</default>
        public const int DepthTimeStabilization = 180;

        /// <summary>
        ///     Wait time for swipe gestures.
        /// </summary>
        /// <default>260</default>
        public const int SwipeTimeStabilization = 260;

        /// <summary>
        ///     Wait time for lock gestures.
        /// </summary>
        /// <default>80</default>
        public const int GestureLockTime = 80;

        /// <summary>
        ///     Roughness movement percentage for continuous movement. From ]0,1].
        ///     Values close to 0 does smooth movements.
        /// </summary>
        /// <default>0.2f</default>
        public const double RoughnessFactor = 0.2f;

        /// <summary>
        ///     Sensitivity of move on depth axis. Measured in millimeters of Kinect axis plane.
        /// </summary>
        /// <default>0.12f</default>
        public const double DepthSensitivity = 0.12f;

        /// <summary>
        ///     Sensitivity of move on horizontal axis. Measured in millimeters of Kinect axis plane.
        /// </summary>
        /// <default>0.25f</default>
        public const double HorizontalSensitivity = 0.25f;

        /// <summary>
        ///     Sensitivity of move on vertical axis. Measured in millimeters of Kinect axis plane.
        /// </summary>
        /// <default>0.25f</default>
        public const double VerticalSensitivity = 0.25f;

        /// <summary>
        ///     Sensitivity of push gesture on depth axis. Measured in millimeters of Kinect axis plane.
        ///     Value to ignore sudden movements.
        /// </summary>
        /// <default>0.2f</default>
        public const double PushStabilization = 0.2f;

        /// <summary>
        ///     Sensitivity of push gesture on depth axis. Measured in millimeters of Kinect axis plane.
        ///     Value to ignore sudden movements.
        /// </summary>
        /// <default>0.3f</default>
        public const double PullStabilization = 0.3f;

        /// <summary>
        ///     Distance considered as close from one joint to another.
        /// </summary>
        /// <default>0.1f</default>
        public const double NearDistance = 0.1f;

        /// <summary>
        ///     Distance for wheel mouse operation when is triggered.
        /// </summary>
        /// <default>1</default>
        public const int MouseWheelTriggerValue = 1;

        #endregion
    }
}