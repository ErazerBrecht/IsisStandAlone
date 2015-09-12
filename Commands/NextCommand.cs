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
        private BeheerExtendViewModel _viewModel;

        public NextCommand(BeheerExtendViewModel viewModel)
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
            if (_viewModel.ViewSource.View.CurrentPosition < _viewModel.ViewSource.View.SourceCollection.Cast<Klant>().Count() - 1)
                _viewModel.ViewSource.View.MoveCurrentToNext();
        }
    }
}
