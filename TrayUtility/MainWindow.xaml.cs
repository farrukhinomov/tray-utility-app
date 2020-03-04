using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace UtilitiesHandler
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeData();

            StartupUIConfigure();
        }

        private void InitializeData()
        {
            UtilitiesManager utilitiesManager = new UtilitiesManager();
            utilitiesManager.LoadUtilities();

            NotifyIconManager notifyIconManager = new NotifyIconManager();
            notifyIconManager.LoadUtilitiesToContextMenuStrip(utilitiesManager.Utilities);
        }

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true; // this will prevent to close
            Hide();
        }
        private void StartupUIConfigure()
        {
            //Visibility = Visibility.Hidden;
            //WindowState = WindowState.Minimized;
            ResizeMode = ResizeMode.CanMinimize;
        }

        //private void notifyIcon_DoubleClick(object sender, EventArgs e)
        //{
        //    WindowState = WindowState.Normal;
        //    Show();
        //}

    }
}
