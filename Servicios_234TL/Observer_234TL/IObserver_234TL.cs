using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_234TL.Observer_234TL
{
    public interface IObserver_234TL<T>
    {
        void Update(T Traduccion);
    }
}