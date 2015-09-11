using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using ISIS.Commands;
using System.Drawing;
using System.Windows;

namespace ISIS.ViewModels
{
    public class ErrorWindowViewModel
    {
        public ImageSource ErrorIcon
        {
            get
            {
                return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(SystemIcons.Error.Handle, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
            }
        }
        public string DefaultLocation
        {
            get
            {
                return "Standaard locatie: " + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ISIS Rijkevorsel", "ISIS_Data.mdf");
            }
        }

        public ShutDownCommand ShutDownCommandEvent { get; private set; }
        public OpenFileDialogCommand OpenFileDialogCommandEvent { get; private set; }

        public ErrorWindowViewModel()
        {
            ShutDownCommandEvent = new ShutDownCommand();
            OpenFileDialogCommandEvent = new OpenFileDialogCommand();
        }

    }
}
