using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ISIS.ViewModels;

namespace ISIS.Commands
{
    class NextCommand : ICommand
    {
        private BeheerViewModel _viewModel;

        public NextCommand(BeheerViewModel viewModel)
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
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.ViewSource.View.MoveCurrentToNext();
            if (_viewModel.ViewSource.View.IsCurrentAfterLast)
                _viewModel.ViewSource.View.MoveCurrentToLast();
        }
    }
}
