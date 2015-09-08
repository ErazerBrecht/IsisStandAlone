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
    class PersoneelBeheerViewModel : BeheerViewModel, INotifyPropertyChanged
    {

        #region SelectedPersoneel
        private Strijker _selectedPersoneel;

        public Strijker SelectedPersoneel
        {
            get { return _selectedPersoneel; }
            set
            {
                _selectedPersoneel = value;
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

        public AddPersoneelCommand AddCommandEvent { get; private set; }

        public override bool IsValid
        {
            get
            {
                //return SelectedKlant.CanSave;
                return true;
            }
        }
    
        public PersoneelBeheerViewModel() : base()
        {
            Header = "PersoneelBeheer";
            GetData();
            AddCommandEvent = new AddPersoneelCommand(this);
        }

        private void GetData()
        {
            Refresh();
            ViewSource.View.CollectionChanged += View_CurrentChanged;

            SelectedPersoneel = ViewSource.View.CurrentItem as Strijker;
        }
        protected override void View_CurrentChanged(object sender, EventArgs e)
        {
            SelectedPersoneel = (sender as CollectionView).CurrentItem as Strijker;
        }

        public override void Delete()
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
            ctx.SaveChanges();
        }
    }
}
