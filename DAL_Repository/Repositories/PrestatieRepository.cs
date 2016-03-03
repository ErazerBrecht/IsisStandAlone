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
    public class PrestatieRepository : Repository<Prestatie>, IPrestatieRepository
    {
        public PrestatieRepository(IsisContext context) : base(context)
        {
            context.Prestaties.Load();
        }

        public IsisContext IsisContext => Context as IsisContext;
    }
}
