using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISIS.Views;

namespace ISIS.Services
{
    interface IErrorDataBaseWindowService
    {
        void Show(object dataContext);
    }

    public class ErrorDataBaseWindowService : IErrorDataBaseWindowService
    {
        public void Show(object dataContext)
        {
            var window = new ErrorDatabase();
            window.DataContext = dataContext;
            window.Show();
        }
    }
}
