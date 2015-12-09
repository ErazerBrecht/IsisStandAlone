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

        public ISIS_DataEntities ctx { get; set; }
        public IEnumerable<Strijker> Strijkers { get; set; }
        public ObservableCollection<Datum> CurrentDates { get; set; }

        public override bool IsValid        //Used to disable or Enable Toevoegen button
        {
            get
            {
                //TODO: Add Datavalidation
                return true;
            }
        }

        public DatumBeheerViewModel(ISIS_DataEntities context)
        {
            ctx = context;
            CurrentDates = new ObservableCollection<Datum>();

            AddDatum = new Datum { Date = DateTime.Now };
            
            LoadData();
        }

        public void LoadData()
        {
            Strijkers = ctx.Strijkers.ToList();
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
            throw new NotImplementedException();
        }

        public override void Add()
        {
            ctx.Datum.Add(AddDatum);
            CurrentDates.Add(AddDatum);

            AddDatum = new Datum { Date = DateTime.Now };
        }

        public override void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
