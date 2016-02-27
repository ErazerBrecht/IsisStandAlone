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
    class DatumRepository: Repository<Datum>, IDatumRepository
    {
        public DatumRepository(IsisContext context) : base(context)
        {
            context.Datums.Load();
        }

        public IEnumerable<Datum> GetDatumsFromPrestatie(int id)
        {
            return IsisContext.Datums.Local.Where(d => d.PrestatieId == id);
        }

        public IsisContext IsisContext
        {
            get { return Context as IsisContext; }
        }
    }
}
