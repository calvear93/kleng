using System;
using System.IO;
using System.Linq;
using Kleng.Components.Exceptions;
using Microsoft.Speech.AudioFormat;
using Microsoft.Speech.Recognition;

namespace Kleng.Components.VoiceRecognizer
{
    /// <summary>
    ///     Voice (Speech) recognition engine controller.
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>2.1.2</version>
    internal class VoiceRecognizer : IDisposable
    {
        /// <summary>
        ///     Create the voice recognition engine controller.
        /// </summary>
        public VoiceRecognizer()
        {
            // Gets the language pack from system.
            _recognizer = GetLanguagePack();
            // Creates the speech engines.
            _speechRecognitionEngine = GetSpeechRecognitionEngine();
        }

        #region PROPERTIES

        /// <summary>
        ///     Stores Speech recognition engine.
        /// </summary>
        private readonly SpeechRecognitionEngine _speechRecognitionEngine;

        /// <summary>
        ///     Stores the information of the recognizer libraries.
        /// </summary>
        private readonly RecognizerInfo _recognizer;

        /// <summary>
        ///     Allows to get current language pack as a string.
        /// </summary>
        public string Language => _recognizer.Culture.Name;

        #endregion

        #region EVENTS

        /// <summary>
        ///     Subscribes a function to Speech Recognition event.
        ///     Is recommended to use OnSpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        ///     header for listener function.
        /// </summary>
        public EventHandler<SpeechRecognizedEventArgs> SpeechRecognized
        {
            set { _speechRecognitionEngine.SpeechRecognized += value; }
        }

        /// <summary>
        ///     Subscribes a function to Speech Hypothesized event.
        ///     Is recommended to use OnSpeechHypothesized(object sender, SpeechHypothesizedEventArgs e)
        ///     header for listener function.
        /// </summary>
        public EventHandler<SpeechHypothesizedEventArgs> SpeechHypothesized
        {
            set { _speechRecognitionEngine.SpeechHypothesized += value; }
        }

        /// <summary>
        ///     Subscribes a function to Speech Rejected event.
        ///     Is recommended to use OnSpeechRejected(object sender, SpeechRecognitionRejectedEventArgs e)
        ///     header for listener function.
        /// </summary>
        public EventHandler<SpeechRecognitionRejectedEventArgs> SpeechRejected
        {
            set { _speechRecognitionEngine.SpeechRecognitionRejected += value; }
        }

        /// <summary>
        ///     Subscribes a function to Speech Detected event.
        ///     Is recommended to use OnSpeechDetected(object sender, SpeechDetectedEventArgs e)
        ///     header for listener function.
        /// </summary>
        public EventHandler<SpeechDetectedEventArgs> SpeechDetected
        {
            set { _speechRecognitionEngine.SpeechDetected += value; }
        }

        #endregion

        #region LANGUAGE AND ENGINE RETRIEVING

        /// <summary>
        ///     Searches for and returns a language pack for voice recognition.
        /// </summary>
        /// <returns>language pack information object.</returns>
        private static RecognizerInfo GetLanguagePack()
        {
            // Checks all RecognizerInfo in order to find the language pack.
            foreach (var recognizer in SpeechRecognitionEngine.InstalledRecognizers())
            {
                switch (recognizer.Culture.Name)
                {
                    // Spanish (Spain) language pack.
                    case "es-ES":
                        return recognizer;
                    // Spanish (Mexico) language pack.
                    case "es-MX":
                        return recognizer;
                }
            }
            return SpeechRecognitionEngine.InstalledRecognizers().FirstOrDefault();
        }

        /// <summary>
        ///     Creates a new speech engine for voice processing.
        /// </summary>
        /// <returns>Speech engine.</returns>
        private SpeechRecognitionEngine GetSpeechRecognitionEngine()
        {
            // If doesn't exists a language pack, throws an exception.
            if (_recognizer == null)
                throw new LanguagePackException("Language pack not found.");

            return new SpeechRecognitionEngine(_recognizer.Id);
        }

        #endregion

        #region INITIALIZATION

        /// <summary>
        ///     Creates the grammar with all the word and
        ///     phrases to will be recognized.
        /// </summary>
        /// <param name="lexicon">Array of words and phrases.</param>
        /// <param name="name">Name of the grammar.</param>
        /// <returns>Grammar containing words and phrases for the context.</returns>
        private Grammar CreateGrammar(Choices lexicon, string name)
        {
            // Creates the grammar builder.
            var grammarb = new GrammarBuilder {Culture = _recognizer.Culture};
            // Adds the command for the recognition.
            grammarb.Append(lexicon);
            // Creates and loads the grammar into the speech engine.
            var grammar = new Grammar(grammarb) {Name = name};
            return grammar;
        }

        /// <summary>
        ///     Creates the choices (words and phrases) from an array for speech engine.
        /// </summary>
        /// <param name="lexicon">Array of words and phrases.</param>
        /// <returns>Choices for the speech engine.</returns>
        private static Choices GetChoicesFromArray(string[] lexicon)
        {
            // Creates choices that will contains words and phrases for recognition.
            var semantic = new Choices();
            // Adds the lexicon to the choices for recognition.
            semantic.Add(lexicon);
            return semantic;
        }

        /// <summary>
        ///     Initialize the voice recognition from an audio stream.
        /// </summary>
        /// <param name="stream">Audio stream.</param>
        /// <param name="lexicon">Array of words and phrases to will be recognized by speech engine.</param>
        public void InitializeRecognitionFromStream(Stream stream, string[] lexicon)
        {
            // Initializes Speech recognition engine.
            _speechRecognitionEngine.LoadGrammarAsync(CreateGrammar(GetChoicesFromArray(lexicon), "Default"));
            // Defines the speech audio format for the streaming.
            var speechFormat = new SpeechAudioFormatInfo(KinectCapabilities.Format, KinectCapabilities.SamplesPerSecond,
                KinectCapabilities.BitsPerSample,
                KinectCapabilities.Channels, KinectCapabilities.AverageBytesPerSecond, KinectCapabilities.BlockAlign,
                null);
            // Initialize Speech engine with the stream for the recognition.
            _speechRecognitionEngine.SetInputToAudioStream(stream, speechFormat);
        }

        /// <summary>
        ///     Initialize the voice recognition from an audio file.
        /// </summary>
        /// <param name="uri">Uniform Resource Identifier of the audio file to will be write.</param>
        /// <param name="lexicon">Array of words and phrases to will be recognized by speech engine.</param>
        public void InitializeRecognitionFromAudioFile(string uri, string[] lexicon)
        {
            // Initializes Speech recognition engine.
            _speechRecognitionEngine.LoadGrammarAsync(CreateGrammar(GetChoicesFromArray(lexicon), "Default"));
            // Initialize Speech engine with the audio file for the recognition.
            _speechRecognitionEngine.SetInputToWaveFile(@uri);
        }

        #endregion

        #region CONTROL

        /// <summary>
        ///     Starts asynchronous recognition.
        /// </summary>
        public void Start()
        {
            // If speech engine isn't initialized, throws an exception.
            if (_speechRecognitionEngine == null)
                throw new SpeechEngineNotInitializedException("Speech engine not initialized.");

            // Initializes the speech engines and starts the recognition.
            _speechRecognitionEngine.RecognizeAsync(KinectCapabilities.RecognitionMode);
        }

        /// <summary>
        ///     Update the grammar of the asynchronous recognition.
        /// </summary>
        /// <param name="lexicon">Array of words and phrases to will be recognized by speech engine.</param>
        /// <param name="reset">Boolean indicating if current grammars must be removed or not.</param>
        public void Update(string[] lexicon, bool reset = false)
        {
            // If speech engine isn't initialized, throws an exception.
            if (_speechRecognitionEngine == null)
                throw new SpeechEngineNotInitializedException("Speech engine not initialized.");

            // Stops the recognition engine.
            _speechRecognitionEngine.RequestRecognizerUpdate();
            if (reset)
                _speechRecognitionEngine.UnloadAllGrammars();
            _speechRecognitionEngine.LoadGrammarAsync(CreateGrammar(GetChoicesFromArray(lexicon), "Default"));
        }

        /// <summary>
        ///     Stops asynchronous recognition after the current recognition operation completes.
        /// </summary>
        public void Stop()
        {
            // If speech engine isn't initialized, throws an exception.
            if (_speechRecognitionEngine == null)
                throw new SpeechEngineNotInitializedException("Speech engine not initialized.");

            // Stops the recognition engine.
            _speechRecognitionEngine.RecognizeAsyncStop();
        }

        /// <summary>
        ///     Terminates asynchronous recognition without waiting for the current recognition operation to complete.
        /// </summary>
        public void Finish()
        {
            // If speech engine isn't initialized, throws an exception.
            if (_speechRecognitionEngine == null)
                throw new SpeechEngineNotInitializedException("Speech engine not initialized.");

            // Stops the recognition engine.
            _speechRecognitionEngine.RecognizeAsyncCancel();
        }

        /// <summary>
        ///     Dispose the recognition engine.
        /// </summary>
        public void Dispose()
        {
            // If speech engine isn't initialized, throws an exception.
            if (_speechRecognitionEngine == null)
                throw new SpeechEngineNotInitializedException("Speech engine not initialized.");

            // Finalizes the engine.
            _speechRecognitionEngine.Dispose();
        }

        #endregion
    }
}