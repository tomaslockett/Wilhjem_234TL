using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_234TL.Singleton_234TL
{
    public class SingletonT_234TL<T> where T : class, new()
    {
        private static T Instancia;
        private static readonly object lockObjeto = new();

        public static T GetInstance()
        {
            if (Instancia == null)
            {
                lock (lockObjeto)
                {
                    if (Instancia == null)
                    {
                        Instancia = new T();
                    }
                }
            }
            return Instancia;
        }
    }
}