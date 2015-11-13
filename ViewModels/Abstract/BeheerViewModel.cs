using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ISIS.Commands;
using ISIS.Services;
using System.Windows;
using System.Windows.Input;

namespace ISIS.ViewModels
{
    abstract class BeheerViewModel : WorkspaceViewModel
    {
        public CollectionViewSource ViewSource { get; protected set; }

        #region Commands
        public ICommand AddCommandEvent { get; protected set; }
        public ICommand SaveCommandEvent { get; protected set; }
        public ICommand DeleteCommandEvent { get; protected set; }
        public ICommand RefreshCommandEvent { get; protected set; }
        #endregion

        abstract public bool IsValid { get; }

        public BeheerViewModel()
        {
            ViewSource = new CollectionViewSource();
            AddCommandEvent = new AddCommand(this);
            SaveCommandEvent = new SaveCommand(this);
            DeleteCommandEvent = new DeleteCommand(this);
            RefreshCommandEvent = new RefreshCommand(this);
        }

        public abstract void Delete();
        public abstract void Refresh();
        public abstract void Add();
        public abstract void SaveChanges();

        //TODO: It could be this has to change to WorkspaceViewModel, don't forgot to think about this!!!
        public virtual bool Close()
        {
            if (ctx.ChangeTracker.HasChanges())
            {
                MessageBoxService messageService = new MessageBoxService();

                var result = messageService.AskForConfirmation("Er zijn nog onopgeslagen wijzigingen.\nWilt u deze wijzingen nog opslaan?", Header);
                if (result == MessageBoxResult.Yes)
                {
                    //TODO: Check if there are validation errors!!!
                    SaveChanges();
                }
                else if (result == MessageBoxResult.No)
                {
                    Refresh();
                }
                else
                {
                    return true;
                }
            }

            return false;

        }
    }
}
