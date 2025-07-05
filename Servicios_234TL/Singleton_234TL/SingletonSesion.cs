namespace Servicios_234TL.Singleton_234TL
{
    public class SingletonSesion
    {
        private static Sesion_234TL sesion_;

        private static Object Lock = new();

        public static Sesion_234TL GetInstance()
        {
            if (sesion_ == null)
            {
                lock (Lock)
                {
                    if (sesion_ == null)
                    {
                        sesion_ = new Sesion_234TL();
                    }
                }
            }
            return sesion_;
        }
    }
}