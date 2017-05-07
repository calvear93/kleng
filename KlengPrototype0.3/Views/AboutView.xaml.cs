using System.Windows;
using Kleng.Views.Utils;

namespace Kleng.Views
{
    /// <summary>
    ///     Interaction logical segment for AboutView.xaml
    /// </summary>
    /// <author>Cristopher Alvear Candia</author>
    /// <version>1.0.1</version>
    public partial class AboutView : Window
    {
        private readonly Window _parent;

        public AboutView(Window parent)
        {
            InitializeComponent();
            _parent = parent;
            Cursor = CursorsUtils.Arrow();
            Back_Button.Cursor = CursorsUtils.Link();
        }

        private void Back_Button_Click(object sender, RoutedEventArgs e)
        {
            _parent.Show();
            Close();
        }
    }
}