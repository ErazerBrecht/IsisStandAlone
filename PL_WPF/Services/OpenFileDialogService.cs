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
        void Open();
    }
    class OpenFileDialogService : IOpenFileDialogService
    {
        public void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Database files | *.mdf";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    File.Copy(openFileDialog.FileName, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ISIS Rijkevorsel", openFileDialog.SafeFileName));     //TODO Add Exception handling
                    Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kon het gekozen bestand niet openenen!\nKijk na of u de juiste rechten hebt\nHet bastand kan ook al in gebruik zijn!\n\nExtra informatie:\n" + ex.ToString());
                }
            }
        }
    }
}
