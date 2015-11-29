using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public SearchBoxStrijkerViewModel[] SearchBoxViewModel { get; set; }
        public ObservableCollection<Datum> CurrentDates { get; set; }

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

            SearchBoxViewModel = new SearchBoxStrijkerViewModel[5];
            for (int i = 0; i < SearchBoxViewModel.Length; i++)
            {
                SearchBoxViewModel[i] = new SearchBoxStrijkerViewModel();

                var index = i;     //Make a local variable, otherwise i is always 5 in the callback!

                SearchBoxViewModel[i].PropertyChanged += (s, e) => SearchBox_PropertyChanged(s, e, index);
            }
            
        }

        private void SearchBox_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e, int i)
        {
            //TODO: Search how to store array of strijkers in DB with EF!!!
            switch (i)
            {
                case 0:
                    AddDatum.Strijker1_ID = ((SearchBoxStrijkerViewModel)sender).SearchBoxSelectedStrijker.ID;
                    break;
                case 1:
                    AddDatum.Strijker2_ID = ((SearchBoxStrijkerViewModel)sender).SearchBoxSelectedStrijker.ID;
                    break;
                case 2:
                    AddDatum.Strijker3_ID = ((SearchBoxStrijkerViewModel)sender).SearchBoxSelectedStrijker.ID;
                    break;
                case 3:
                    AddDatum.Strijker4_ID = ((SearchBoxStrijkerViewModel)sender).SearchBoxSelectedStrijker.ID;
                    break;
                case 4:
                    AddDatum.Strijker5_ID = ((SearchBoxStrijkerViewModel)sender).SearchBoxSelectedStrijker.ID;
                    break;
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
            CurrentDates.Add(AddDatum);
            AddDatum = new Datum { Date = DateTime.Now };
        }

        public override void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
