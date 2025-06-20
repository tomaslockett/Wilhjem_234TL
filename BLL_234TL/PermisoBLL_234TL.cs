using DAL_234TL;
using Servicios_234TL.Composite_234TL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_234TL
{
    public class PermisoBLL_234TL : AbstractaBLL_234TL<Permiso_234TL, int>
    {
        public PermisoBLL_234TL() : base(new PermisoDAL_234TL())
        {
        }
    }
}
