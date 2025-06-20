using DAL_234TL;
using Servicios_234TL.Composite_234TL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_234TL
{
    public class PerfilBLL_234TL : AbstractaBLL_234TL<Perfil_234TL, int>
    {
        public PerfilBLL_234TL() : base(new PerfilDAL_234TL())
        {
        }
    }
}
