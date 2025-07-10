namespace Servicios_234TL.Exception_234TL
{
    public class ValidacionesException_234TL : Exception
    {
        public string Nombre { get; set; }
        public object[] Args { get; set; }
        public ValidacionesException_234TL(string claveMensaje, string nombre, params object[] args) : base(claveMensaje)
        {
            this.Nombre = nombre;
            this.Args = args; 
        }
    }
}