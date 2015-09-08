using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Data.Entity;
using ISIS.Commands;

namespace ISIS.ViewModels
{
    class KlantenBeheerViewModel : BeheerViewModel
    {
        #region SelectedKlant fullproperty

        private Klant _selectedKlant;

        public Klant SelectedKlant
        {
            get { return _selectedKlant; }
            set
            {
                _selectedKlant = value;
                NoticeMe("SelectedKlant");
            }
        }
        #endregion

        public AddKlantCommand AddCommandEvent { get; private set; }

        public KlantenBeheerViewModel() : base()
        {
            Header = "KlantenBeheer";
            GetData();
            AddCommandEvent = new AddKlantCommand(this);
        }

        private void GetData()
        {
            Refresh(); 
            ViewSource.View.CollectionChanged += View_CurrentChanged;

            SelectedKlant = ViewSource.View.CurrentItem as Klant;           
        }

        public override void Add()
        {
            ctx.Klanten.Add(SelectedKlant);
        }

        public override void Delete()
        {
            ctx.Klanten.Remove(SelectedKlant);
        }

        public override void Refresh()
        {
            ctx = new ISIS_DataEntities();
            ctx.Klanten.Load();
            ViewSource.Source = ctx.Klanten.Local;
            ViewSource.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));            //Zorgt ervoor dat de DataGrid geordend is op ID!!
        }
        
        public override void SaveChanges()
        {
            ctx.SaveChanges();
        } 

        protected override void View_CurrentChanged(object sender, EventArgs e)
        {
            SelectedKlant = (sender as CollectionView).CurrentItem as Klant;
        }
    }
}
