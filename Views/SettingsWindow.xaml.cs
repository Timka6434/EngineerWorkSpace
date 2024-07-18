using System.Windows;
using System.Windows.Forms;
using EngineerWorkSplace.ViewModels;

namespace EngineerWorkSplace.Views
{
    public partial class SettingsWindow : Window
    {
        private Certificates_VM certificatesVM;

        public SettingsWindow(Certificates_VM vm)
        {
            InitializeComponent();
            certificatesVM = vm;
            PathTextBox.Text = certificatesVM.PathToCertificates;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            using (var dialog = new FolderBrowserDialog())
            {
                DialogResult result = dialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    PathTextBox.Text = dialog.SelectedPath;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            certificatesVM.PathToCertificates = PathTextBox.Text;
            this.Close();
        }
    }
}
