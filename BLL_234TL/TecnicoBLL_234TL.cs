using BE_234TL;
using DAL_234TL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_234TL
{
    public class TecnicoBLL_234TL : AbstractaBLL_234TL<Tecnico_234TL, string>
    {
        public TecnicoBLL_234TL() : base(new TecnicoDAL_234TL())
        {
        }
    }
}
