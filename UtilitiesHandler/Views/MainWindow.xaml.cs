using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace UtilitiesHandler.Views
{
    public partial class MainWindow : Window, IWindowsService
    {
        private readonly MainViewModel mainViewModel;
        private readonly NotifyIconManager notifyIconManager;
        public MainWindow()
        {
            InitializeComponent();
            var utilityService = new UtilityService();
            notifyIconManager = new NotifyIconManager(utilityService, this);
            notifyIconManager.RefreshTrayItems();
            StartupUIConfigure();
            DataContext = mainViewModel = new MainViewModel(utilityService, notifyIconManager);
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            mainViewModel.ViewAppearing();
        }

        private void StartupUIConfigure()
        {
            Visibility = Visibility.Hidden;
            WindowState = WindowState.Minimized;
            ResizeMode = ResizeMode.CanMinimize;
        }

        public void ShowWindow()
        {
            ScrollBar1.Focus();
            ScrollBar1.ScrollToEnd();
            WindowState = WindowState.Normal;
            Show();
        }

        public void HideWindow()
        {
            Visibility = Visibility.Hidden;
            WindowState = WindowState.Minimized;
            Hide();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true; // this will prevent to close
            Hide();
        }

        public void ExitApp()
        {
            Application.Current.Shutdown();
        }

        public void AddMessageToLogger(string message)
        {
            LoggerBox.Text += message;
        }
    }
}