using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_Context;
using EF_Context.Repositories;
using EF_Model;

namespace DAL_Repository.Repositories
{
    public class StukPrestatieRepository: Repository<StukPrestatie>, IStukPrestatieRepository
    {
        public StukPrestatieRepository(IsisContext context) : base(context)
        {
            context.StukPrestaties.Load();
        }

        public bool Any()
        {
           return IsisContext.StukPrestaties.Local.Any();
        }

        public StukPrestatie GetLatestPrestatie(Klant klant)
        {
            return IsisContext.StukPrestaties.Local.Where(p => p.Klant.Id == klant.Id).OrderByDescending(p => p.Id).FirstOrDefault();
        }

        public IsisContext IsisContext
        {
            get { return Context as IsisContext; }
        }
    }
}
