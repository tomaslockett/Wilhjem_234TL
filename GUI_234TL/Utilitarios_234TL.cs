using BLL_234TL;
using Servicios_234TL;
using Servicios_234TL.Observer_234TL;
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

        //public static void CambiarUsuarioToolStrip(ToolStripStatusLabel label, Usuario_234TL usuario)
        //{
        //    try
        //    {
        //        if (usuario != null)
        //        {
        //            label.Text = $"¡Bienvenido/a, {usuario.Nombre}! ¡Que tengas una excelente jornada!";
        //        }
        //        else
        //        {
        //            label.Text = "Usuario: No hay usuario logueado";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
        //Basura 
        public static void MensajeInformacion(string mensaje)
        {
            var traduccion = IdiomasManager_234TL.Instancia.ObtenerIdiomasActuales();
            MessageBox.Show(traduccion[mensaje], traduccion["MensajeTitulo_Informacion"], MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void MensajeError(string mensaje)
        {
            var traduccion = IdiomasManager_234TL.Instancia.ObtenerIdiomasActuales();
            MessageBox.Show(traduccion[mensaje], traduccion["MensajeTitulo_Error"], MessageBoxButtons.OK, MessageBoxIcon.Error);
        }


        public static void MensajeAdvertencia(string mensaje)
        {
            var traduccion = IdiomasManager_234TL.Instancia.ObtenerIdiomasActuales();
            MessageBox.Show(traduccion[mensaje], traduccion["MensajeTitulo_Advertencia"], MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static void MensajeExito(string mensaje)
        {
            var traduccion = IdiomasManager_234TL.Instancia.ObtenerIdiomasActuales();
            MessageBox.Show(traduccion[mensaje], traduccion["MensajeTitulo_Exito"], MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void SuscribirAIdiomas(IObserver_234TL<Dictionary<string, string>> observer)
        {
            IdiomasManager_234TL.Instancia.Subscribe(observer);
        }
        public static void DesuscribirDeIdiomas(IObserver_234TL<Dictionary<string, string>> observer)
        {
            IdiomasManager_234TL.Instancia.Unsubscribe(observer);
        }
    }
}