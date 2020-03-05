using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
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
                Icon = new System.Drawing.Icon("notify.ico"),
                Text = "Tray Utilities app",
                Visible = true,
            };
            _notifyIcon.MouseClick += new MouseEventHandler(notifyIcon_Click);
            _notifyIcon.DoubleClick += Double_Click;
            _notifyIcon.ShowBalloonTip(500, "Utility app is running...", "You can start the commands here", ToolTipIcon.Info);
        }

        public void RefreshTrayItems()
        {
            var utilities = UtilityService.GetUtilities().Where(item => item.Enabled == true);
            _notifyIcon.ContextMenuStrip.Items.Clear();
            foreach (var item in utilities)
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

        private void notifyIcon_Click(object sender, MouseEventArgs e)
        {
            //shows contextMenu even click any mouse buttons
            typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(_notifyIcon, null);
        }

        private void TrayContextMenuHandler(object sender, EventArgs e, Func<string> run)
        {
            var takenMessage = run.Invoke() + "\n";
            _windowsService.AddMessageToLogger(takenMessage);
        }
        private void Double_Click(object sender, EventArgs e)
        {
            _windowsService.ShowWindow();
        }

        private void ExitApp(object sender, EventArgs e)
        {
            _windowsService.ExitApp();
        }
    }
}
