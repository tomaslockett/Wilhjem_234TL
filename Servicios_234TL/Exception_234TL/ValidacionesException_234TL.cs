namespace Servicios_234TL.Exception_234TL
{
    public class ValidacionesException_234TL : Exception
    {
        public string Nombre { get; set; }
        public ValidacionesException_234TL(string mensaje, string nombre) : base(mensaje)
        {
            this.Nombre = nombre;
        }
    }
}