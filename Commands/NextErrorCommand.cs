using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ISIS.ViewModels;

namespace ISIS.Commands
{
    class NextErrorCommand : ICommand
    {
        private BeheerExtendViewModel _viewModel;

        public NextErrorCommand(BeheerExtendViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return !_viewModel.IsValid;
        }

        public void Execute(object parameter)
        {
            _viewModel.SetErrorAsSelected();
        }
    }
}
