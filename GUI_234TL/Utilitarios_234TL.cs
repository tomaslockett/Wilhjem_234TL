using BLL_234TL;
using GUI_234TL;
using Servicios_234TL;
using Servicios_234TL.Observer_234TL;
using Servicios_234TL.Observer_234TL.Traducciones_234TL;
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

        public static void MensajeInformacion(string mensaje, params object[] args)
        {
            try
            {
                var traduccion = IdiomasManager_234TL.Instancia.ObtenerIdiomasActuales();
                string textoOriginal = traduccion.Messages.Informacion[mensaje];
                string textoFormateado = string.Format(textoOriginal, args);
                string titulo = traduccion.Comunes.Title_Informacion;
                MessageBox.Show(textoFormateado, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falló la traducción para la clave '{mensaje}': {ex.Message}", "Error de Traducción", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void MensajeError(string mensaje, Exception ex = null, params object[] args)
        {
            try
            {

                var traduccion = IdiomasManager_234TL.Instancia.ObtenerIdiomasActuales();
                string titulo = traduccion.Comunes.Title_Error;
                string textoOriginal = traduccion.Messages.Error[mensaje];
                string textoFormateado = string.Format(textoOriginal, args);

                if (ex != null)
                {
                    textoFormateado += $"\n\nDetalle: {ex.Message}";
                }
                MessageBox.Show(textoFormateado, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex2)
            {
                MessageBox.Show($"Falló la traducción para la clave '{mensaje}': {ex2.Message}", "Error de Traducción", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public static void MensajeAdvertencia(string mensaje, params object[] args)
        {
            try
            {
                var traduccion = IdiomasManager_234TL.Instancia.ObtenerIdiomasActuales();
                string textoOriginal = traduccion.Messages.Peligro[mensaje];
                string textoFormateado = string.Format(textoOriginal, args);
                string titulo = traduccion.Comunes.Title_Peligro;
                MessageBox.Show(textoFormateado, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falló la traducción para la clave '{mensaje}': {ex.Message}", "Error de Traducción", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void MensajeExito(string mensaje, params object[] args)
        {
            try
            {
                var traduccion = IdiomasManager_234TL.Instancia.ObtenerIdiomasActuales();
                string text = string.Format(traduccion.Messages.Exito[mensaje], args);
                string titulo = traduccion.Comunes.Title_Exito;
                MessageBox.Show(text, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Falló la traducción para la clave '{mensaje}': {ex.Message}", "Error de Traducción", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static DialogResult MensajeConfirmacion(Form owner,string claveMensaje, params object[] args)
        {
            return FormMessageBox_234TL.Mostrar(owner,claveMensaje,MessageBoxButtons.YesNo,MessageBoxIcon.Question,args);
        }

        public static void SuscribirAIdiomas(IObserver_234TL<TraduccionesClase_234TL> observer)
        {
            IdiomasManager_234TL.Instancia.Subscribe(observer);
        }
        public static void DesuscribirDeIdiomas(IObserver_234TL<TraduccionesClase_234TL> observer)
        {
            IdiomasManager_234TL.Instancia.Unsubscribe(observer);
        }

        public static void PoblarComboBox<TEnum>(ComboBox comboBox, Dictionary<string, string> traducciones) where TEnum : Enum
        {
            var items = new List<KeyValuePair<TEnum, string>>();

            foreach (TEnum valor in Enum.GetValues(typeof(TEnum)))
            {
                string textoTraducido = traducciones.ContainsKey(valor.ToString())
                    ? traducciones[valor.ToString()]
                    : valor.ToString();

                items.Add(new KeyValuePair<TEnum, string>(valor, textoTraducido));
            }

            object valorSeleccionado = comboBox.SelectedValue;

            comboBox.DataSource = items;
            comboBox.DisplayMember = "Value";
            comboBox.ValueMember = "Key";

            if (valorSeleccionado != null)
            {
                comboBox.SelectedValue = valorSeleccionado;
            }
        }

    }
}