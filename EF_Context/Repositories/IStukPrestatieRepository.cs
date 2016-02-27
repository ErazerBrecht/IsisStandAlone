using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_Model;

namespace EF_Context.Repositories
{
    public interface IStukPrestatieRepository : IRepository<StukPrestatie>
    {
        bool Any();
        StukPrestatie GetLatestPrestatie(Klant klant);
    }
}
