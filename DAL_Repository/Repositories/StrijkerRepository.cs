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
    class StrijkerRepository : Repository<Strijker>, IStrijkerRepository
    {
        public StrijkerRepository(IsisContext context) : base(context)
        {
            context.Strijkers.Load();
        }
        public bool Exists(int id)
        {
            return IsisContext.Strijkers.Local.Any(s => s.Id == id);
        }

        public IEnumerable<Strijker> GetEnabledStrijkers()
        {
            return IsisContext.Strijkers.Local.Where(s => s.ActiefBool);
        }

        public IsisContext IsisContext => Context as IsisContext;
    }
}
