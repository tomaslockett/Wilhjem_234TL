using DAL_234TL;
using Servicios_234TL;
using Servicios_234TL.Composite_234TL;
using Servicios_234TL.Exception_234TL;
using Servicios_234TL.Observer_234TL;
using Servicios_234TL.Singleton_234TL;
using System.Text.RegularExpressions;

namespace BLL_234TL
{
    public class UsuarioBLL_234TL : AbstractaBLL_234TL<Usuario_234TL, string>
    {
        private readonly PerfilBLL_234TL _perfilBLL;
        public UsuarioBLL_234TL() : base(new UsuarioDAL_234TL())
        {
            _perfilBLL = new PerfilBLL_234TL();
        }
        public IList<Perfil_234TL> ObtenerPerfilesDisponibles()
        {
            return _perfilBLL.GetAll();
        }

        public IList<Usuario_234TL> GetUsuariosActivos(bool Activos)
        {
            var usuarios = _repositorio.GetAll();
            return Activos ? usuarios.Where(u => u.Activo == true).ToList() : usuarios.ToList();
        }

        public void GenerarCredenciales(Usuario_234TL usuario)
        {
            try
            {
                usuario.Login = usuario.Nombre.ToLower() + usuario.Apellido.ToLower();
                string Dni = usuario.DNI.ToString().PadLeft(3, '0');
                if (string.IsNullOrWhiteSpace(Dni) || Dni.Length < 3)
                {
                    throw new ValidacionesException_234TL("ErrorGenerarPasswordDNI", "DNI");
                }
                string Contradni = Dni.Substring(Dni.Length - 3);
                string CONTRASEÑA = $"{usuario.Apellido.ToLower()}{Contradni}";
                usuario.Password = Encryptador_234TL.SHA256Encrpytar_234TL(CONTRASEÑA);
            }
            catch (ValidacionesException_234TL)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al generar las credenciales.", ex);
            }
        }

        public void GenerarLogin(Usuario_234TL usuario)
        {
            usuario.Login = usuario.Nombre.ToLower() + usuario.Apellido.ToLower();
        }

        public void DesbloquearUsuario(Usuario_234TL usuario)
        {
            usuario.Bloqueado = false;
            usuario.IntentosFallidos = 0;
            usuario.UltimoIntentoFallido = null;
            GenerarCredenciales(usuario);
            _repositorio.Update(usuario);
        }

        public void ActivarUsuario(Usuario_234TL usuario)
        {
            usuario.Activo = true;
            _repositorio.Update(usuario);
        }

        public void DesactivarUsuario(Usuario_234TL usuario)
        {
            usuario.Activo = false;
            _repositorio.Update(usuario);
        }

        public void Login(string login, string password)
        {
            if (string.IsNullOrWhiteSpace(login))
            {
                throw new ValidacionesException_234TL("LoginUsuarioRequerido", "General");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ValidacionesException_234TL("LoginPasswordRequerido", "General");
            }

            var sesion = SingletonT_234TL<Sesion_234TL>.GetInstance();
            if (sesion.IsLoggedIn_234TL())
            {
                throw new ValidacionesException_234TL("SesionYaActiva", "General");
            }

            var passwordHasheada = Encryptador_234TL.SHA256Encrpytar_234TL(password);

            if (login.Equals("superadmin", StringComparison.OrdinalIgnoreCase) &&
                passwordHasheada == Encryptador_234TL.SHA256Encrpytar_234TL("superadmin"))
            {
                var perfilAdmin = new Perfil_234TL("SuperAdmin");
                var superadmin = new Usuario_234TL
                {
                    Login = "superadmin",
                    Perfil = perfilAdmin,
                    Nombre = "Super",
                    Apellido = "Admin",
                    Activo = true,
                    Bloqueado = false
                };
                sesion.Login_234TL(superadmin);
                return;
            }

            var usuario = _repositorio.GetAll().FirstOrDefault(u => u.Login == login);

            if (usuario == null || usuario.Password != passwordHasheada)
            {
                if (usuario != null)
                {
                    usuario.IntentosFallidos++;
                    usuario.UltimoIntentoFallido = DateTime.Now;
                    if (usuario.IntentosFallidos >= 3)
                    {
                        usuario.Bloqueado = true;
                    }
                    _repositorio.Update(usuario);

                    if (usuario.Bloqueado)
                    {
                        throw new ValidacionesException_234TL("UsuarioBloqueado", "General");
                    }
                }

                throw new ValidacionesException_234TL("ContraseñaInvalida", "General");
            }

            if (!usuario.Activo)
            {
                throw new ValidacionesException_234TL("UsuarioInactivo", "General");
            }

            if (usuario.Bloqueado)
            {
                throw new ValidacionesException_234TL("UsuarioBloqueado", "General");
            }

            usuario.IntentosFallidos = 0;
            usuario.UltimoIntentoFallido = null;
            _repositorio.Update(usuario);

            sesion.Login_234TL(usuario);
            if (!string.IsNullOrWhiteSpace(usuario.Idioma))
            {
                IdiomasManager_234TL.Instancia.CambiarIdioma(usuario.Idioma);
            }
        }

        public Resultados_234TL Logout()
        {
            var sesion = SingletonT_234TL<Sesion_234TL>.GetInstance();

            if (!sesion.IsLoggedIn_234TL())
                return Resultados_234TL.NoHayLogueado;

            sesion.Logout_234TL();
            return Resultados_234TL.SesionCerrada;
        }

        public Usuario_234TL GetUsuarioLogueado()
        {
            var sesion = SingletonT_234TL<Sesion_234TL>.GetInstance();

            if (!sesion.IsLoggedIn_234TL())
                return null;

            return sesion.Usuario;
        }

        public void CrearNuevoUsuario(Usuario_234TL usuario)
        {
            if (usuario.Perfil == null)
            {
                throw new ValidacionesException_234TL("RolRequerido", "Perfil");
            }

            if (string.IsNullOrWhiteSpace(usuario.DNI))
            {
                throw new ValidacionesException_234TL("DNIRequerido", "DNI");
            }

            if (!Regex.IsMatch(usuario.DNI, @"^\d{8}$"))
            {
                throw new ValidacionesException_234TL("DNIFormato", "DNI");
            }

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                throw new ValidacionesException_234TL("NombreRequerido", "Nombre");
            }

            if (!Regex.IsMatch(usuario.Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]{2,50}$"))
            {
                throw new ValidacionesException_234TL("NombreFormato", "Nombre");
            }

            if (string.IsNullOrWhiteSpace(usuario.Apellido))
            {
                throw new ValidacionesException_234TL("ApellidoRequerido", "Apellido");
            }

            if (!Regex.IsMatch(usuario.Apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]{2,50}$"))
            {
                throw new ValidacionesException_234TL("ApellidoFormato", "Apellido");
            }

            if (string.IsNullOrWhiteSpace(usuario.Email))
            {
                throw new ValidacionesException_234TL("EmailRequerido", "Email");
            }

            if (!Regex.IsMatch(usuario.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                throw new ValidacionesException_234TL("EmailFormato", "Email");
            }

            if (ExisteDni(usuario.DNI))
            {
                throw new ValidacionesException_234TL("DNIPerteneceOtro", "DNI");
            }

            if (ExisteEmail(usuario.Email))
            {
                throw new ValidacionesException_234TL("EmailPerteneceOtro", "Email");
            }

            usuario.Bloqueado = false;
            usuario.Activo = true;
            usuario.IntentosFallidos = 0;
            usuario.Idioma = "es"; 
            GenerarCredenciales(usuario);

            _repositorio.Guardar(usuario);
        }

        public void ModificarUsuario(Usuario_234TL usuario)
        {
            if (usuario.Perfil == null)
            {
                throw new ValidacionesException_234TL("RolRequerido", "Perfil");
            }

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
            {
                throw new ValidacionesException_234TL("NombreRequerido", "Nombre");
            }

            if (!Regex.IsMatch(usuario.Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]{2,50}$"))
            {
                throw new ValidacionesException_234TL("NombreFormato", "Nombre");
            }

            if (string.IsNullOrWhiteSpace(usuario.Apellido))
            {
                throw new ValidacionesException_234TL("ApellidoRequerido", "Apellido");
            }

            if (!Regex.IsMatch(usuario.Apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]{2,50}$"))
            {
                throw new ValidacionesException_234TL("ApellidoFormato", "Apellido");
            }

            if (string.IsNullOrWhiteSpace(usuario.Email))
            {
                throw new ValidacionesException_234TL("EmailRequerido", "Email");
            }

            if (!Regex.IsMatch(usuario.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                throw new ValidacionesException_234TL("EmailFormato", "Email");
            }

            if (ExisteEmail(usuario.Email, usuario.Login))
            {
                throw new ValidacionesException_234TL("EmailPerteneceOtro", "Email");
            }

            GenerarLogin(usuario);
            _repositorio.Update(usuario);
        }

        // busca un usuario que tenga el mismo dni o que tenga mismo dni y mismo login por si se modifica :D
        public bool ExisteDni(string dni, string loginActual = null)
        {
            return GetAll().Any(u => u.DNI == dni && u.Login != loginActual);
        }

        public bool ExisteEmail(string Email, string loginActual = null)
        {
            return GetAll().Any(u => u.Email == Email && u.Login != loginActual);
        }

        public void CambiarIdiomaUsuario(Usuario_234TL usuario, string nuevoCodigoIdioma)
        {
            if (usuario == null || string.IsNullOrWhiteSpace(nuevoCodigoIdioma))
            {
                throw new ValidacionesException_234TL("ErrorCambioIdioma", "General");
            }

            usuario.Idioma = nuevoCodigoIdioma;

            _repositorio.Update(usuario);

            IdiomasManager_234TL.Instancia.CambiarIdioma(nuevoCodigoIdioma);
        }

        public void CambiarContraseña(string contraseñaActual, string contraseñaNueva, string confirmacion)
        {
            if (string.IsNullOrWhiteSpace(contraseñaActual) || string.IsNullOrWhiteSpace(contraseñaNueva) || string.IsNullOrWhiteSpace(confirmacion))
            {
                throw new ValidacionesException_234TL("CamposRequeridos", "General");
            }

            var sesion = SingletonT_234TL<Sesion_234TL>.GetInstance();
            if (!sesion.IsLoggedIn_234TL())
            {
                throw new ValidacionesException_234TL("NoHayLogueado", "General");
            }
            var usuario = sesion.Usuario;

            string actualHash = Encryptador_234TL.SHA256Encrpytar_234TL(contraseñaActual);
            if (usuario.Password != actualHash)
            {
                throw new ValidacionesException_234TL("ContraseñaIncorrecta", "ContraseñaActual");
            }

            if (contraseñaNueva != confirmacion)
            {
                throw new ValidacionesException_234TL("ContraseñaNoCoincide", "Confirmacion");
            }

            string nuevaHash = Encryptador_234TL.SHA256Encrpytar_234TL(contraseñaNueva);
            if (usuario.Password == nuevaHash)
            {
                throw new ValidacionesException_234TL("ContraseñaRepetida", "ContraseñaNueva");
            }

            usuario.Password = nuevaHash;
            _repositorio.Update(usuario); 

            sesion.Logout_234TL();
        }
    }
}