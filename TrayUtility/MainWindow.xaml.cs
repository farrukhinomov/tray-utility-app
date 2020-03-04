using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Annotations;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UtilitiesHandler
{
    public partial class MainWindow : Window
    {
        private NotifyIcon _notifyIcon;
        string _outputFolderPath { get; set; } = System.IO.Directory.GetCurrentDirectory();

        List<Utility> _utilities = new List<Utility>();

        public MainWindow()
        {
            Visibility = Visibility.Hidden;
            WindowState = WindowState.Minimized;
            ResizeMode = ResizeMode.CanMinimize;

            var fileName = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DateLinks.xml");
            InitializeComponent();
            InitializeUtilities();
        }

        private void InitializeUtilities()
        {
            DirectoryInfo utilitiesDirectory = new DirectoryInfo(_outputFolderPath);
            var utilitiesFiles = utilitiesDirectory.GetFiles("EU.*.dll", SearchOption.AllDirectories);
            foreach (var file in utilitiesFiles)
            {
                Assembly assembly = Assembly.LoadFile(file.FullName);
                var types = assembly.GetTypes();

                foreach (var type in types)
                    _utilities.Add(new Utility(type));
            }
            trayIconSettings();
        }

        private void trayIconSettings()
        {

            _notifyIcon = new NotifyIcon()
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = new System.Drawing.Icon("notify.ico"),
                Text = "Tray Utilities app",
                Visible = true,
            };
            _notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_Click);
            _notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            _notifyIcon.ShowBalloonTip(500, "Utility app is running...", "You can start the commands here", System.Windows.Forms.ToolTipIcon.Info);
            foreach (var item in _utilities)
                _notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler(item));
            _notifyIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Exit", null, new EventHandler(ExitApp)));
        }

        private ToolStripMenuItem ToolStripMenuItemWithHandler(Utility utility)
        {
            var item = new ToolStripMenuItem(utility.Name);
            item.Click += (sender, e) => TrayContextMenuHandler(sender, e, utility.Execute);
            item.ToolTipText = utility.Help;
            return item;
        }

        private void notifyIcon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //shows contextMenu even click any mouse buttons
            typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(_notifyIcon, null);
        }

        private void TrayContextMenuHandler(object sender, EventArgs e, Func<string> message)
        {
            LoggerBox.Text += message.Invoke() + "\n";
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true; // this will prevent to close
            Hide();
        }

        #region utilities commands
        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            WindowState = WindowState.Normal;
            Show();
        }

        private void ExitApp(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        #endregion

        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Utility item in e.RemovedItems)
            {
                _utilities.Remove(item);
            }

            foreach (Utility item in e.AddedItems)
            {
                _utilities.Add(item);
            }
        }
    }
}
