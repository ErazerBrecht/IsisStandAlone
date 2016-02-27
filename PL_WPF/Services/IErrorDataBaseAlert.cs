using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL_WPF.Services
{
    interface IErrorDataBaseWindowService
    {
        void Show(object dataContext);
    }

    public class ErrorDataBaseWindowService : IErrorDataBaseWindowService
    {
        public void Show(object dataContext)
        {
            //var window = new ErrorDatabase();
            //window.DataContext = dataContext;
            //window.Show();
        }
    }
}
