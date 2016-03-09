using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PL_WPF.Services
{
    public interface IOpenFolderDialogService
    {
        string Open();
    }

    public class OpenFolderDialogService : IOpenFolderDialogService
    {
        public string Open()
        {
            var dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            return dialog.SelectedPath;
        }
    }
}
