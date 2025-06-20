using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_234TL.Observer_234TL
{
    public abstract class Subject_234TL<T> : IObservable_234TL<T>
    {
        private readonly List<IObserver_234TL<T>> observers = new();

        public void Subscribe(IObserver_234TL<T> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            else
            {
                throw new ArgumentException("El observador ya está suscrito.");
            }
        }

        public void Unsubscribe(IObserver_234TL<T> observer)
        {
            if (observers.Contains(observer))
            {
                observers.Remove(observer);
            }
            else
            {
                throw new ArgumentException("El observador no está suscrito.");
            }
        }

        protected void Notify(T data)
        {
            foreach (var observer in observers)
            {
                observer.Update(data);
            }
        }
    }
}