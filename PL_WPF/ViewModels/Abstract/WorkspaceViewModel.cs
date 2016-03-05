using System;
using System.ComponentModel;
using System.Windows;
using DAL_Repository;
using PL_WPF.Services;

namespace PL_WPF.ViewModels
{
    public abstract class WorkspaceViewModel : INotifyPropertyChanged
    {
        #region Header full-property
        private string _header;

        public string Header
        {
            get { return _header; }
            protected set {
                _header = value;
                NoticeMe("Header");
            }
        }
        #endregion

        protected UnitOfWork Ctx;

        protected WorkspaceViewModel(UnitOfWork ctx)
        {
            Ctx = ctx;
        }

        public virtual void LoadData()
        {
            //TODO
            //Make abstract?
        }

        public virtual bool Leave(bool changes = false, bool errors = false)
        {
            if(Ctx.HasChanges() || changes)
            {
                MessageBoxService messageService = new MessageBoxService();

                var result = messageService.AskForConfirmation("Er zijn nog onopgeslagen wijzigingen.\nWilt u deze wijzingen nog opslaan?", Header);
                if (result == MessageBoxResult.Yes)
                {
                    //Check if there are validation errors!!!
                    try
                    {
                        if (errors)
                            throw new Exception("There is a specific validation error");
                        Ctx.Complete();
                    }
                    catch
                    {
                        messageService.ShowMessageBox("Er bevinden zich nog fouten in de data! Kan dit niet opslaan!");
                        return false;
                    }
                }
                else if (result == MessageBoxResult.No)
                {
                    Ctx.DiscardChanges();
                }
                else
                {
                    return false;
                }
            }      

            return true;

        }

        #region INotifyPropertyChanged
        protected void NoticeMe(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
