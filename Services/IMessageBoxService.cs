using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ISIS.Services
{
    public interface IMessageBoxService
    {
        MessageBoxResult AskForConfirmation(string message, string title);
    }

    public class MessageBoxService : IMessageBoxService
    {
        public MessageBoxResult AskForConfirmation(string message, string title)
        {
            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNoCancel);
            return result;
        }
    }
}
