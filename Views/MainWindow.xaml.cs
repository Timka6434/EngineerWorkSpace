using System.Windows;
using EngineerWorkSplace.ViewModels;

namespace EngineerWorkSplace.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vm = (DataContext as ServiceESPD_VM)?.CertificatesVM;
            if (vm != null)
            {
                SettingsWindow settingsWindow = new SettingsWindow(vm);
                settingsWindow.ShowDialog();
            }
        }
    }
}
