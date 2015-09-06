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
    class KlantenBeheerViewModel : BeheerViewModel, INotifyPropertyChanged
    {
        ISIS_DataEntities ctx = new ISIS_DataEntities();

        #region Lijst Klanten
        private List<Klant> _klanten;
        public List<Klant> Klanten
        {
            get { return _klanten; }
            set
            {
                _klanten = value;
                NoticeMe("Klanten");
            }
        }
        #endregion

        public NextCommand NextCommandEvent { get; private set; }
        public PreviousCommand PreviousCommandEvent { get; private set; }

        public KlantenBeheerViewModel()
        {
            Header = "KlantenBeheer";
            GetData();
            NextCommandEvent = new NextCommand(this);
            PreviousCommandEvent = new PreviousCommand(this);
        }

        private void GetData()
        {
            Klanten = ctx.Klanten.ToList();
            ViewSource = new CollectionViewSource();
            ViewSource.Source = Klanten;
        }

        public CollectionView GetKlantenCollectionView(List<Klant> klantenLijst)
        {
            return (CollectionView)CollectionViewSource.GetDefaultView(klantenLijst);
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
