using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Kleng.Components;
using Kleng.Components.HandControl;
using Kleng.Views.Utils;

namespace Kleng.Views
{
    /// <summary>
    ///     Interaction logical segment for PairingTermsActivityView.xaml
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>4.2.1.7</version>
    public partial class PairingTermsActivityView : Window
    {
        private readonly PairingTerms _pairing;
        private readonly PairingTermsView _parent;
        private Button _lastPair;
        private int _pairsCompleted, _corrects, _wrongs;

        public PairingTermsActivityView(PairingTermsView parent, PairingTerms pairing)
        {
            InitializeComponent();
            Topmost = true;
            var leftTermsButtons = new List<Button>();
            var rightTermsButtons = new List<Button>();
            _parent = parent;
            _pairing = pairing;
            pairing_name.Text = _pairing.Title;

            var pairingTerms = _pairing.GetShuffledTerms();
            ListLeft.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            ListRight.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            Elements.Disable(ListRight);

            _pairsCompleted = 0;

            // Each terms has a max length of 10 words or 60 chars.
            Populate_Lists(pairingTerms[0], leftTermsButtons, ListLeft);

            Populate_Lists(pairingTerms[1], rightTermsButtons, ListRight);

            Cursor = CursorsUtils.Arrow();
            Help_Button.Cursor = CursorsUtils.HelpSelect();
            left_pair.Cursor = CursorsUtils.Loading();
            right_pair.Cursor = CursorsUtils.Loading();
        }

        private void Pair_Click(object sender, RoutedEventArgs e)
        {
            FileUtils.PlayAudioFile("Sounds/ui/affirm.wav");
            if (ListLeft.IsEnabled)
            {
                Elements.Disable(ListLeft);
                Elements.Enable(ListRight);
            }
            else
            {
                Elements.Disable(ListRight);
                Elements.Enable(ListLeft);
            }

            var buttonTriggered = (Button) sender;
            if (_lastPair != null)
            {
                if (_pairing.IsCorrectPair(((TextBlock) _lastPair.Content).Text,
                    ((TextBlock) buttonTriggered.Content).Text))
                {
                    Pair_Match(_lastPair, buttonTriggered, Brushes.DarkGreen);
                    _corrects++;
                    _parent.UpdateScores(_corrects, _wrongs);

                    SetPairsIndicator(left_pair, right_pair, buttonTriggered, Brushes.Green);
                }
                else
                {
                    Pair_Match(_lastPair, buttonTriggered, Brushes.Red);
                    _wrongs++;
                    _parent.UpdateScores(_corrects, _wrongs);

                    SetPairsIndicator(left_pair, right_pair, buttonTriggered, Brushes.Red);
                }
                _pairsCompleted++;

                if (_pairsCompleted == _pairing.Count)
                    _parent.Finish("FINALIZADA...");

                _lastPair = null;
                return;
            }

            _lastPair = buttonTriggered;
            SetPairIndicator(left_pair, _lastPair, Brushes.DarkOrange);
            right_pair.Content = "";
            right_pair.Background = Brushes.Gray;
        }

        private void SetPairsIndicator(Button left, Button right, Button last, Brush color)
        {
            left.Background = color;
            SetPairIndicator(right, last, color);
            Animations.Highlight(right, 0.5f);
        }

        private void SetPairIndicator(Button pair, Button origin, Brush color)
        {
            pair.Content = new TextBlock
            {
                TextWrapping = TextWrapping.Wrap,
                Text = ((TextBlock)origin.Content).Text
            };
            pair.FontSize = origin.FontSize - 8;
            pair.Background = color;
            Animations.Highlight(pair, 0.5f);
        }

        private void Populate_Lists(string[] listTerms, List<Button> buttonList, ListBox listView)
        {
            listView.Items.Add(new ProgressBar {Width = listView.Width - 32, Background = Brushes.DeepPink});

            foreach (var term in listTerms)
            {
                var lines = term.Length/20;
                var fsize = (lines - 2);

                var pair = new Button {Height = 64 + lines*32, Width = listView.Width - 32, FontSize = 26 - fsize*2};

                var wrapper = new TextBlock
                {
                    TextWrapping = TextWrapping.Wrap,
                    Text = term
                };
                pair.Content = wrapper;

                pair.Click += Pair_Click;
                pair.Cursor = CursorsUtils.Link();
                buttonList.Add(pair);
                listView.Items.Add(pair);
            }
            listView.Items.Add(new ProgressBar {Width = listView.Width - 32, Background = Brushes.DeepPink});
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Topmost = true;
            Activate();
        }

        private void pair_Click(object sender, RoutedEventArgs e)
        {
            FileUtils.PlayAudioFile("Sounds/ui/load.wav");
            left_pair.Content = "";
            left_pair.Background = Brushes.Gray;
            right_pair.Content = "";
            right_pair.Background = Brushes.Gray;
            Elements.Enable(ListLeft);
            Elements.Disable(ListRight);
            _lastPair = null;
        }

        private void Pair_Match(Button last, Button trigger, Brush color)
        {
            Elements.Disable(last, trigger);
            _lastPair.Background = color;
            trigger.Background = color;
        }

        private void Help(object sender, EventArgs e)
        {
            FileUtils.PlayAudioFile("Sounds/help/pairing_terms_view.wav");
        }
    }
}