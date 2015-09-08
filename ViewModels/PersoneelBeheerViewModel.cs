using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using ISIS.Commands;

namespace ISIS.ViewModels
{
    class PersoneelBeheerViewModel : BeheerViewModel, INotifyPropertyChanged
    {
        ISIS_DataEntities ctx = new ISIS_DataEntities();

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

        public NextCommand NextCommandEvent { get; private set; }
        public PreviousCommand PreviousCommandEvent { get; private set; }

        public PersoneelBeheerViewModel() : base()
        {
            Header = "PersoneelBeheer";
            GetData();
            NextCommandEvent = new NextCommand(this);
            PreviousCommandEvent = new PreviousCommand(this);
        }

        private void GetData()
        {
            Personeel = ctx.Strijkers.ToList();
            ViewSource.Source = Personeel;
            ViewSource.View.CollectionChanged += View_CurrentChanged;

            SelectedPersoneel = ViewSource.View.CurrentItem as Strijker;
        }
        protected override void View_CurrentChanged(object sender, EventArgs e)
        {
            SelectedPersoneel = (sender as CollectionView).CurrentItem as Strijker;
        }
    }
}
