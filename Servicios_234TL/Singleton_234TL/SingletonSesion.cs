namespace Servicios_234TL.Singleton_234TL
{
    public class SingletonSesion
    {
        private static Sesion_234TL sesion_;

        private static Object Lock = new();

        // se puede poner new() en vez de new object(), porque se sobre entiende
        //https://learn.microsoft.com/es-mx/dotnet/fundamentals/code-analysis/style-rules/ide0090

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