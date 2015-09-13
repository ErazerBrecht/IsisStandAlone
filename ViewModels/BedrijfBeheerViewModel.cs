using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISIS.Models;
using ISIS.Commands;

namespace ISIS.ViewModels
{
    class BedrijfBeheerViewModel : BeheerViewModel
    {
        public Bedrijf AddBedrijf { get; private set; }

        public override bool IsValid
        {
            get
            {
                //TODO: Add Datavalidation
                return true;
            }
        }

        public BedrijfBeheerViewModel()
        {
            LoadData();
            AddBedrijf = new Bedrijf();
        }

        public void LoadData()
        {
            Refresh();
        }

        public override void Delete(object o)
        {
            ctx.Bedrijven.Remove(o as Bedrijf);
            ctx.SaveChanges();
        }

        public override void Refresh()
        {
            ctx = new ISIS_DataEntities();
            ctx.Bedrijven.Load();
            ViewSource.Source = ctx.Bedrijven.Local;
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void SaveChanges()
        {
            ctx.Bedrijven.Add(AddBedrijf);
            ctx.SaveChanges();
            AddBedrijf = new Bedrijf();
        }
    }
}
