using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PL_WPF.Views;

namespace PL_WPF.Services
{
    interface IPrintWindow
    {
        void ShowPrintPreview(object datacontext);
    }

    public class PrintWindow : IPrintWindow
    {
        public void ShowPrintPreview(object datacontext)
        {
            TijdPrestatiePrint window = new TijdPrestatiePrint(datacontext);
            window.Show();
        }
    }
}
