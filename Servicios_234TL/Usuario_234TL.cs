using Servicios_234TL.Composite_234TL;

namespace Servicios_234TL
{
    public class Usuario_234TL
    {
        public string DNI { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public Perfil_234TL Perfil { get; set; }
        public bool Bloqueado { get; set; }
        public bool Activo { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int IntentosFallidos { get; set; }

        public DateTime? UltimoIntentoFallido { get; set; }

        public string Idioma { get; set; } = "es"; 

        public Usuario_234TL()
        { }

        public Usuario_234TL(string login, string password)
        {
            Login = login;
            Password = password;
        }

        public Usuario_234TL(string dni, string nombre, string apellido, string email, Perfil_234TL perfil, bool bloqueado, bool activo, string login, string password, int intentosFallidos)
        {
            DNI = dni;
            Nombre = nombre;
            Apellido = apellido;
            Email = email;
            Perfil = perfil;
            Bloqueado = bloqueado;
            Activo = activo;
            Login = login;
            Password = password;
            IntentosFallidos = intentosFallidos;
        }

        //public override string ToString()
        //{
        //    return $"Login: {Login}, Password: {Password}";
        //}
    }
}