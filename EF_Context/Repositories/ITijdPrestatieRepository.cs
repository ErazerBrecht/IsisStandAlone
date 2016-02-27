using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_Model;

namespace EF_Context.Repositories
{
    public interface ITijdPrestatieRepository : IRepository<TijdPrestatie>
    {
        bool Any();
        TijdPrestatie GetLatestPrestatie(Klant klant);
    }
}
