using BE_234TL;
using DAL_234TL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_234TL
{
    public class EquipoBLL_234TL : AbstractaBLL_234TL<Equipo_234TL, string>
    {
        public EquipoBLL_234TL() : base(new EquipoDAL_234TL())
        {
        }
        public bool ExisteEquipo(string numeroSerie)
        {
            return GetAll().Any(e => e.NumeroSerie.Equals(numeroSerie, StringComparison.OrdinalIgnoreCase));
        }
        public void IngresarEquipo(string numeroSerie, string modelo, string marca, string estado, string falla, bool desarmado, bool dañoVisible)
        {
            var nuevo = new Equipo_234TL(numeroSerie, marca, modelo, estado, falla, DateTime.Now, desarmado, dañoVisible);
            Guardar(nuevo);
        }

    }
}
