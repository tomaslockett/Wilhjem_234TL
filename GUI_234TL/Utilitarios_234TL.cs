using BLL_234TL;
using Servicios_234TL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Wilhjem
{
    public static class Utilitarios_234TL
    {
        public static void TamañoOriginal(Control Boton)
        {
            Boton.Size = new Size(176, 38);
        }

        public static void Agrandar(Control Boton)
        {
            double altura = Boton.Size.Height;
            double ancho = Boton.Size.Width;

            Boton.Size = new Size((int)(ancho * 1.1), (int)(altura * 1.1));
        }

        public static void CambiarUsuarioToolStrip(ToolStripStatusLabel label, Usuario_234TL usuario)
        {
            try
            {
                if (usuario != null)
                {
                    label.Text = "Usuario: " + usuario.Nombre;
                }
                else
                {
                    label.Text = "Usuario: No hay usuario logueado";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}