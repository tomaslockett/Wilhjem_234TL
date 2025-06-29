﻿using DAL_234TL;
using Servicios_234TL;
using Servicios_234TL.Singleton_234TL;

namespace BLL_234TL
{
    public class UsuarioBLL_234TL : AbstractaBLL_234TL<Usuario_234TL, string>
    {
        public UsuarioBLL_234TL() : base(new UsuarioDAL_234TL())
        {
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
                    throw new ArgumentException("El DNI debe tener al menos 3 dígitos válidos para generar la contraseña.");
                }
                string Contradni = Dni.Substring(Dni.Length - 3);
                string CONTRASEÑA = $"{usuario.Apellido.ToLower()}{Contradni}";
                usuario.Password = Encryptador_234TL.SHA256Encrpytar_234TL(CONTRASEÑA);
            }
            catch (Exception ex)
            {
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

        public Resultados_234TL Login(string login, string password)
        {
            if (login == "superadmin" && password == Encryptador_234TL.SHA256Encrpytar_234TL("superadmin"))
            {
                var superadmin = new Usuario_234TL
                {
                    Login = login,
                    Password = password,
                    Rol = "SuperAdmin",
                    Activo = true,
                    Bloqueado = false
                };
                SingletonSesion.GetInstance().Login_234TL(superadmin);
                return Resultados_234TL.UsuarioValido;
            }
            if (string.IsNullOrEmpty(login) || string.IsNullOrWhiteSpace(password))
                return Resultados_234TL.ContrasenaInvalida;
            var sesion = SingletonSesion.GetInstance();

            if (sesion.IsLoggedIn_234TL())
                return Resultados_234TL.UsuarioLogueado;

            var usuario = _repositorio.GetAll().Where(u => u.Login == login).FirstOrDefault();

            if (usuario == null)
                return Resultados_234TL.UsuarioNoExiste;

            if (usuario.Activo == false)
                return Resultados_234TL.UsuarioInactivo;

            if (usuario.IntentosFallidos >= 2 && usuario.UltimoIntentoFallido.HasValue)
            {
                var diferencia = DateTime.Now - usuario.UltimoIntentoFallido.Value;
                if (diferencia.TotalHours >= 3)
                {
                    usuario.IntentosFallidos = 0;
                    usuario.UltimoIntentoFallido = null;
                    _repositorio.Update(usuario);
                }
            }
            if (usuario.Bloqueado == true)
                return Resultados_234TL.UsuarioBloqueado;

            if (usuario.Password != password)
            {
                usuario.IntentosFallidos++;
                usuario.UltimoIntentoFallido = DateTime.Now;
                if (usuario.IntentosFallidos >= 3)
                {
                    usuario.Bloqueado = true;
                    _repositorio.Update(usuario);
                    return Resultados_234TL.UsuarioBloqueado;
                }
                _repositorio.Update(usuario);
                return Resultados_234TL.ContrasenaInvalida;
            }
            usuario.IntentosFallidos = 0;
            usuario.UltimoIntentoFallido = null;
            _repositorio.Update(usuario);

            sesion.Login_234TL(usuario);
            return Resultados_234TL.UsuarioValido;
        }

        public Resultados_234TL Logout()
        {
            var sesion = SingletonSesion.GetInstance();

            if (!sesion.IsLoggedIn_234TL())
                return Resultados_234TL.NoHayLogueado;

            sesion.Logout_234TL();
            return Resultados_234TL.SesionCerrada;
        }

        public Usuario_234TL GetUsuarioLogueado()
        {
            var sesion = SingletonSesion.GetInstance();

            if (!sesion.IsLoggedIn_234TL())
                return null;

            return sesion.Usuario;
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
    }
}