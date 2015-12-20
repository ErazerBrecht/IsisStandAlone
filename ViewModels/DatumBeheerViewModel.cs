using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using ISIS.Models;
using ISIS.Commands;

namespace ISIS.ViewModels
{
    class DatumBeheerViewModel : BeheerViewModel
    {
        #region AddDatum fullproperty
        private Datum _addDatum;
        public Datum AddDatum
        {
            get
            {
                return _addDatum;
            }
            private set
            {
                _addDatum = value;
                NoticeMe("AddDatum");
            }
        }
        #endregion

        private ObservableCollection<Datum> _currentDates;

        public ObservableCollection<Datum> CurrentDates
        {
            get { return _currentDates; }
            set
            {
                _currentDates = value;
                NoticeMe("CurrentDates");
            }
        }
        
        #region Strijkers list for searchbox full property
        private IEnumerable<Strijker> _strijkers;
        public IEnumerable<Strijker> Strijkers
        {
            get { return _strijkers; }
            set
            {
                _strijkers = value;
                NoticeMe("Strijkers");
            }
        }
        #endregion

        public override bool IsValid        //Used to disable or Enable Toevoegen button
        {
            get
            {
                //TODO: Add Datavalidation
                return true;
            }
        }

        public DatumBeheerViewModel()
        {
            CurrentDates = new ObservableCollection<Datum>();
            AddDatum = new Datum { Date = DateTime.Now };
        }

        //Gets called by MainvViewModel
        public override void LoadData()
        {
            using (var context = new ISIS_DataEntities())
            {
                Strijkers = context.Strijkers.ToList();
            }
        }

        public AutoCompleteFilterPredicate<object> StrijkerFilter
        {
            get
            {
                return (searchText, obj) =>
                    Convert.ToString((obj as Strijker).ID).Contains(searchText) || (obj as Strijker).Naam.Contains(searchText);
            }
        }

        public override void Delete()
        {
            //TODO
        }

        public override void Refresh()
        {
            CurrentDates = new ObservableCollection<Datum>();
        }

        public override void Add()
        {
            CurrentDates.Add(AddDatum);
            AddDatum = new Datum { Date = DateTime.Now };
        }

        public override void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
