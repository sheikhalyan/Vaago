using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaago.Models;

namespace Vaago.Controllers.Admin
{
    public interface IObserver
    {
        void Update(Order order, string status);
    }

}
