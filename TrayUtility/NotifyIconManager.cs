using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UtilitiesHandler
{
    public class NotifyIconManager
    {
        private NotifyIcon _notifyIcon;

        public NotifyIconManager()
        {
            _notifyIcon = new NotifyIcon()
            {
                ContextMenuStrip = new ContextMenuStrip(),
                Icon = new System.Drawing.Icon("notify.ico"),
                Text = "Tray Utilities app",
                Visible = true,
            };
            _notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(notifyIcon_Click);
            //_notifyIcon.DoubleClick += notifyIcon_DoubleClick;
            _notifyIcon.ShowBalloonTip(500, "Utility app is running...", "You can start the commands here", System.Windows.Forms.ToolTipIcon.Info);
        }

        public void LoadUtilitiesToContextMenuStrip(List<Utility> utilities)
        {
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

        private string TrayContextMenuHandler(object sender, EventArgs e, Func<string> message)
        {
            return message.Invoke() + "\n";
        }

        private void ExitApp(object sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

    }
}
