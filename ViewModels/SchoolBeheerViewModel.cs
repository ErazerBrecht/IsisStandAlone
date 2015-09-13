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
    class SchoolBeheerViewModel : BeheerViewModel
    {
        public School AddSchool { get; private set; }

        public override bool IsValid
        {
            get
            {
                //TODO: Add Datavalidation
                return true;
            }
        }

        public SchoolBeheerViewModel()
        {
            LoadData();
            AddSchool = new School();
        }

        public void LoadData()
        {
            Refresh();
        }

        public override void Delete(object o)
        {
            ctx.Scholen.Remove(o as School);
            ctx.SaveChanges();
        }

        public override void Refresh()
        {
            ctx = new ISIS_DataEntities();
            ctx.Scholen.Load();
            ViewSource.Source = ctx.Scholen.Local;
        }

        public override void Add()
        {
            throw new NotImplementedException();
        }

        public override void SaveChanges()
        {
            ctx.Scholen.Add(AddSchool);
            ctx.SaveChanges();
            AddSchool = new School();
        }
    }
}
