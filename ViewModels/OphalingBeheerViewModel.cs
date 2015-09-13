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
    class OphalingBeheerViewModel : BeheerViewModel
    {
        public Ophaling AddOphaling { get; private set; }

        public override bool IsValid
        {
            get
            {
                //TODO: Add Datavalidation
                return true;
            }
        }

        public OphalingBeheerViewModel()
        {
            LoadData();
            AddOphaling = new Ophaling();
        }

        public void LoadData()
        {
            Refresh();
        }

        public override void Delete(object o)
        {
            ctx.Ophalingen.Remove(o as Ophaling);
            ctx.SaveChanges();
        }

        public override void Refresh()
        {
            ctx = new ISIS_DataEntities();
            ctx.Ophalingen.Load();
            ViewSource.Source = ctx.Ophalingen.Local;
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void SaveChanges()
        {
            ctx.Ophalingen.Add(AddOphaling);
            ctx.SaveChanges();
            AddOphaling = new Ophaling();
        }
    }
}
