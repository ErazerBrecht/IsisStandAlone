using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ISIS.Commands;

namespace ISIS.ViewModels
{
    class PersoneelBeheerViewModel : BeheerExtendViewModel
    {
        private Strijker _errorPersoneel;

        #region SelectedPersoneel
        private Strijker _selectedPersoneel;

        public Strijker SelectedPersoneel
        {
            get { return _selectedPersoneel; }
            set
            {
                _selectedPersoneel = value;
                ButtonToevoegenContent = "Toevoegen";               //Reset button back to original content, otherwise it keeps on annuleren...
                NoticeMe("SelectedPersoneel");
            }
        }

        #endregion

        #region Lijst Personeel
        private List<Strijker> _personeel;
        public List<Strijker> Personeel
        {
            get { return _personeel; }
            set
            {
                _personeel = value;
                NoticeMe("Klanten");
            }
        }
        #endregion

        #region PersoneelBeheerViewModel specific commands
        public AddPersoneelCommand AddCommandEvent { get; private set; }
        #endregion

        #region Overrided properties
        public override bool IsValid
        {
            get
            {
                if (!SelectedPersoneel.CanSave)
                    return false;

                foreach (Strijker s in ViewSource.View.SourceCollection)           //Check if there is somewhere a validation error!
                {
                    if (!s.CanSave)
                    {
                        _errorPersoneel = s;
                        return false;
                    }
                }
                return true;
            }
        }
        public override string ButtonToevoegenContent
        {
            get
            {
                return base.ButtonToevoegenContent;
            }

            set
            {
                if (SelectedPersoneel != null)
                {
                    if (value == "Annuleren")
                        SelectedPersoneel.CanValidateID = true;             //We should ony check the ID for unicity when the user is adding a new strijker!
                    else
                        SelectedPersoneel.CanValidateID = false;
                }

                base.ButtonToevoegenContent = value;
            }
        }

        #endregion

        public PersoneelBeheerViewModel() : base()
        {
            Header = "PersoneelBeheer";
            GetData();
            DeleteCommandEvent = new DeleteExtendCommand(this);
            RefreshCommandEvent = new RefreshExtendCommand(this);
            AddCommandEvent = new AddPersoneelCommand(this);
        }

        private void GetData()
        {
            Refresh();
            SelectedPersoneel = ViewSource.View.CurrentItem as Strijker;
        }

        public override void Delete(object o)
        {
            ctx.Strijkers.Remove(SelectedPersoneel);
        }

        public override void Refresh()
        {
            ctx = new ISIS_DataEntities();
            ctx.Strijkers.Load();
            ViewSource.Source = ctx.Strijkers.Local;
            ViewSource.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));            //Zorgt ervoor dat de DataGrid geordend is op ID!!
        }

        public override void Add()
        {
            ctx.Strijkers.Add(SelectedPersoneel);
        }

        public override void SaveChanges()
        {
            if (ButtonToevoegenContent == "Annuleren")
            {
                Add();
            }

            ctx.SaveChanges();
            ButtonToevoegenContent = "Toevoegen";
        }

        public override void SetErrorAsSelected()
        {
            if (_errorPersoneel != null)            //If this is null the errorPersoneel is already the selected one!
                SelectedPersoneel = _errorPersoneel;
        }
    }
}
