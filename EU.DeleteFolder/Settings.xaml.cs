using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace EU.DeleteFolder
{
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var path = txtPath.Text;
            var sw = File.CreateText(DeleteFolder.dirName + "\\config.txt");
            sw.WriteLine(path);
            sw.Flush();
            sw.Close();
            this.Close();
        }
    }
}