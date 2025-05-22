using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_234TL.Singleton_234TL
{
    public class Sesion_234TL
    {
        private Usuario_234TL usuario_ { get; set; }

        public Usuario_234TL Usuario
        {
            get { return usuario_; }
        }

        public void Login_234TL(Usuario_234TL usuario)
        {
            if (usuario != null)
            {
                usuario_ = usuario;
            }
            else
            {
                throw new ArgumentNullException("El usuario no puede ser nulo");
            }
        }

        public void Logout_234TL()
        {
            usuario_ = null;
        }

        public bool IsLoggedIn_234TL()
        {
            return usuario_ != null;
        }
    }
}