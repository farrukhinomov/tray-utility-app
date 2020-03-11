using System;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace UtilitiesHandler
{
    public class NotifyIconManager : ITrayService
    {
        private NotifyIcon _notifyIcon;
        private readonly IWindowsService _windowsService;
        private readonly IUtilityService UtilityService;

        public NotifyIconManager(IUtilityService utilityService, IWindowsService windowsService)
        {
            UtilityService = utilityService;
            _windowsService = windowsService;
            _notifyIcon = new NotifyIcon()
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = new System.Drawing.Icon($"{System.AppDomain.CurrentDomain.BaseDirectory}..\\..\\Assets\\app_icon.ico"),
                Text = "Utilities Handler",
                Visible = true,
            };
            _notifyIcon.MouseClick += new MouseEventHandler(notifyIcon_Click);
            _notifyIcon.DoubleClick += Double_Click;
            _notifyIcon.ShowBalloonTip(500, "Utility Handler app is running...", "You can start the utilities here", ToolTipIcon.Info);
            _notifyIcon.BalloonTipClicked += BalloonTipClicked;
        }

        public void RefreshTrayItems()
        {
            var utilities = UtilityService.GetUtilities().Where(item => item.Enabled == true);
            _notifyIcon.ContextMenuStrip.Items.Clear();
            _notifyIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Open UI", null, new EventHandler(ShowWindow)));
            _notifyIcon.ContextMenuStrip.Items.Add(new ToolStripMenuItem("Exit", null, new EventHandler(ExitApp)));
            _notifyIcon.ContextMenuStrip.Items.Add(new ToolStripSeparator()); //Divider
            foreach (var item in utilities)
                _notifyIcon.ContextMenuStrip.Items.Add(ToolStripMenuItemWithHandler(item));
        }

        private ToolStripMenuItem ToolStripMenuItemWithHandler(Utility utility)
        {
            var item = new ToolStripMenuItem(utility.Name);
            item.Click += (sender, e) => TrayContextMenuHandler(sender, e, utility.Name, utility.Execute);
            item.ToolTipText = utility.Help;
            return item;
        }

        private void notifyIcon_Click(object sender, MouseEventArgs e)
        {
            //shows contextMenu even click any mouse buttons
            typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(_notifyIcon, null);
        }

        private void TrayContextMenuHandler(object sender, EventArgs e, string name, Func<string> run)
        {
            string takenMessage =string.Empty;
            takenMessage += $"\n--------------RUNNING--------------\nUtility name: \"{name}\"\n";
            var startTime = DateTime.Now;
            takenMessage += $"Message: {run.Invoke()}\n";
            takenMessage += $"Execution time: {DateTime.Now.Subtract(startTime)}\n---------------!-!-!---------------\n";
            _windowsService.AddMessageToLogger(takenMessage);
            RefreshTrayItems();
        }
        private void Double_Click(object sender, EventArgs e)
        {
            _windowsService.ShowWindow();
        }
        private void BalloonTipClicked(object sender, EventArgs e)
        {
            _windowsService.ShowWindow();
        }

        private void ExitApp(object sender, EventArgs e)
        {
            _windowsService.ExitApp();
        }
        private void ShowWindow(object sender, EventArgs e)
        {
            _windowsService.ShowWindow();
        }
    }
}
