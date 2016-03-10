using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PL_WPF.Services
{
    public interface IMessageBoxService
    {
        MessageBoxResult AskForConfirmation(string message, string title, MessageBoxButton buttons);
        void ShowMessageBox(string message);
        void ShowErrorBox(string message);

    }

    public class MessageBoxService : IMessageBoxService
    {
        public MessageBoxResult AskForConfirmation(string message, string title, MessageBoxButton buttons = MessageBoxButton.YesNoCancel)
        {
            MessageBoxResult result = MessageBox.Show(message, title, buttons);
            return result;
        }

        public void ShowMessageBox(string message)
        {
            MessageBox.Show(message);
        }

        public void ShowErrorBox(string message)
        {
            MessageBox.Show(message, "Strijkdienst Conny ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
