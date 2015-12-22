﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using ISIS.Commands;
using ISIS.Services;
using ISIS.Models;
using PropertyChanged;

namespace ISIS.ViewModels
{
    [ImplementPropertyChanged]
    class BerekenModuleViewModel: BeheerViewModel, IBereken, IDataErrorInfo
    {
        public DatumBeheerViewModel DatumViewModel { get; set; }
        public PrestatieBerekenModuleViewModel CurrentView { get; set; }
        //public SearchBoxKlantViewModel SearchBoxViewModel { get; set; }

        //Extra button
        public BerekenCommand BerekenCommandEvent { get; set; }

        #region SelectedKlant LOGIC
        public bool IsKlantSelected
        {
            get
            {
                if (SelectedKlant != null)
                    return true;
                return false;
            }
        }

        private Klant _selectedKlant;
        public Klant SelectedKlant
        {
            get
            {
                return _selectedKlant;
            }

            set
            {
                _selectedKlant = value;
                if (_selectedKlant != null)
                {
                    if (_selectedKlant.SoortKlant.StukTarief)
                        CurrentView = new StukBerekenModuleViewModel(ctx);
                    else
                        CurrentView = new TijdBerekenModuleViewModel(ctx);
                }
                CurrentView.SelectedKlant = value;
                DatumViewModel.Refresh();
                NoticeMe("SelectedKlant");
                NoticeMe("IsKlantSelected");
            }
        }
        #endregion

        #region Searchbox Klant LOGIC
        public IEnumerable<Klant> Klanten { get; set; }
        public AutoCompleteFilterPredicate<object> KlantenFilter
        {
            get
            {
                return (searchText, obj) =>
                {
                    var klant = obj as Klant;
                    return klant != null && (klant.ToString().Trim().ToUpper().Contains(searchText.ToUpper()));
                };
            }
        }
        #endregion

        public BerekenModuleViewModel()
        {
            Header = "Berekenmodule";
            ctx = new ISIS_DataEntities();
            DatumViewModel = new DatumBeheerViewModel();
            CurrentView = new TijdBerekenModuleViewModel(ctx);
            BerekenCommandEvent = new BerekenCommand(this);
        }

        public override bool IsValid
        {
            get { return CurrentView.IsValid; }
        }

        public void Bereken()
        {
            CurrentView.Bereken();
        }

        public override void Refresh()
        {
            CurrentView.Refresh();
        }

        public override void SaveChanges()
        {
            if (DatumViewModel.CurrentDates.Count < 1)
            {
                MessageBoxService messageService = new MessageBoxService();
                messageService.ShowMessageBox("Je hebt nog geen datum gekozen!");
                return;
            }

            //Add prestatie to DB
            CurrentView.SaveChanges();

            //Add Dates to DB
            foreach (var d in DatumViewModel.CurrentDates)
            {
                d.Id = CurrentView.AddPrestatie.Id;
                ctx.Datum.Add(d);

                //EF is retarded and thinks that I readded the Strijkers to the db, while I didn't.
                //I Just use them as foreign relantionschip, I can't just use the id, because I need the name
                //Manually said this is not true
                //https://msdn.microsoft.com/en-us/magazine/dn166926.aspx
                //This link explains it!

                if (d.Strijker1 != null)
                    ctx.Entry(d.Strijker1).State = EntityState.Unchanged;
                if (d.Strijker2 != null)
                    ctx.Entry(d.Strijker2).State = EntityState.Unchanged;
                if (d.Strijker3 != null)
                    ctx.Entry(d.Strijker3).State = EntityState.Unchanged;
                if (d.Strijker4 != null)
                    ctx.Entry(d.Strijker4).State = EntityState.Unchanged;
                if (d.Strijker5 != null)
                    ctx.Entry(d.Strijker5).State = EntityState.Unchanged;
            }

            //Save changes to DB
            ctx.SaveChanges();

        }

        public override void Delete()
        {
            throw new NotImplementedException();
        }
        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void LoadData()
        {
            Klanten = ctx.Klanten.ToList();
            DatumViewModel.LoadData();
            CurrentView.LoadData();
        }

        public string this[string propertyName]
        {
            get
            {
                string result = null;

                switch (propertyName)
                {
                    case "SelectedKlant":
                    {
                        if (SelectedKlant == null)
                        {
                            result = "Selecteer een klant voor je verder gaat!";
                        }

                        break;
                    }
                }

                return result;
            }
        }

        public string Error { get; }
    }
}
