using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesHandler
{
    public class Settings
    {
        public string SettingsFilePath { get; private set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\TrayUtilityApp", "Settings.xml");


        //private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    foreach (Utility item in e.RemovedItems)
        //    {
        //        _utilities.Remove(item);
        //    }

        //    foreach (Utility item in e.AddedItems)
        //    {
        //        _utilities.Add(item);
        //    }
        //}
    }
}
