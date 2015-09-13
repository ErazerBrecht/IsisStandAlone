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
    class WinkelBeheerViewModel : BeheerViewModel
    {
        public Winkel AddWinkel { get; private set; }

        public override bool IsValid
        {
            get
            {
                //TODO: Add Datavalidation
                return true;
            }
        }

        public WinkelBeheerViewModel()
        {
            LoadData();
            AddWinkel = new Winkel();
        }

        public void LoadData()
        {
            Refresh();
        }

        public override void Delete(object o)
        {
            ctx.Winkels.Remove(o as Winkel);
            ctx.SaveChanges();
        }

        public override void Refresh()
        {
            ctx = new ISIS_DataEntities();
            ctx.Winkels.Load();
            ViewSource.Source = ctx.Winkels.Local;
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void SaveChanges()
        {
            ctx.Winkels.Add(AddWinkel);
            ctx.SaveChanges();
            AddWinkel = new Winkel();
        }
    }
}
