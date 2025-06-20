using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_234TL.Observer_234TL
{
    public interface IObservable_234TL<T>
    {
        void Subscribe(IObserver_234TL<T> observer);

        void Unsubscribe(IObserver_234TL<T> observer);
    }
}