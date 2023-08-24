using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vaago.Models;

namespace Vaago.Controllers.Admin
{
    public class OrderSubject
    {
        private List<IObserver> _observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(Order order, string status)
        {
            foreach (var observer in _observers)
            {
                observer.Update(order, status);
            }
        }
    }

}