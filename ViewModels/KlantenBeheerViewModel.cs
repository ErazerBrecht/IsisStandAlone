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
    class KlantenBeheerViewModel : BeheerViewModel, INotifyPropertyChanged
    {
        ISIS_DataEntities ctx;

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

        #region ButtonToevoegenContent
        private string _buttonToevoegenContent;

        public string ButtonToevoegenContent
        {
            get { return _buttonToevoegenContent; }
            set
            {
                _buttonToevoegenContent = value;
                NoticeMe("ButtonToevoegenContent");
            }
        }
        #endregion

        public NextCommand NextCommandEvent { get; private set; }
        public PreviousCommand PreviousCommandEvent { get; private set; }
        public SaveKlantCommand SaveCommandEvent { get; private set; }
        public DeleteKlantCommand DeleteCommandEvent { get; set; }
        public RefreshKlantCommand RefreshCommandEvent { get; set; }
        public AddKlantCommand AddCommandEvent { get; private set; }

        public KlantenBeheerViewModel() : base()
        {
            Header = "KlantenBeheer";
            GetData();
            NextCommandEvent = new NextCommand(this);
            PreviousCommandEvent = new PreviousCommand(this);
            SaveCommandEvent = new SaveKlantCommand(this);
            DeleteCommandEvent = new DeleteKlantCommand(this);
            RefreshCommandEvent = new RefreshKlantCommand(this);
            AddCommandEvent = new AddKlantCommand(this);
            ButtonToevoegenContent = "Toevoegen";
        }

        private void GetData()
        {
            Refresh(); 
            ViewSource.View.CollectionChanged += View_CurrentChanged;

            SelectedKlant = ViewSource.View.CurrentItem as Klant;           
        }

        public void AddKlant(Klant k)
        {
            ctx.Klanten.Add(k);
        }

        public void DeleteKlant(Klant k)
        {
            ctx.Klanten.Remove(k);
        }

        public void Refresh()
        {
            ctx = new ISIS_DataEntities();
            ctx.Klanten.Load();
            ViewSource.Source = ctx.Klanten.Local;
            ViewSource.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));            //Zorgt ervoor dat de DataGrid geordend is op ID!!
        }
        
        public void SaveChanges()
        {
            ctx.SaveChanges();
        } 

        protected override void View_CurrentChanged(object sender, EventArgs e)
        {
            SelectedKlant = (sender as CollectionView).CurrentItem as Klant;
        }

        #region INotifyPropertyChanged
        private void NoticeMe(string property)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(property));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

    }
}
