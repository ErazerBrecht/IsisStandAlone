using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL_WPF.Services
{
    interface IOpenFileDialogService
    {
        string Open();
    }
    class OpenFileDialogService : IOpenFileDialogService
    {
        public string Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog {Filter = "Backup Database files | *.bak"};

            //Return the path of the selected file or null if the user cancels
            return openFileDialog.ShowDialog() == true ? openFileDialog.FileName : null;
        }
    }
}
