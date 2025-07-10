using Servicios_234TL.Observer_234TL.Traducciones_234TL;
using Servicios_234TL.Singleton_234TL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Servicios_234TL.Observer_234TL
{
    public class IdiomasManager_234TL : Subject_234TL<TraduccionesClase_234TL>
    {

        private TraduccionesClase_234TL _idiomas = new();

        public IdiomasManager_234TL()
        {
            try
            {
                CambiarIdioma("es");
            }
            catch (Exception ex)
            {
                _idiomas = new TraduccionesClase_234TL();
                Console.WriteLine($"Error al cargar el idioma por defecto: {ex.Message}");
            }
        }

        public static IdiomasManager_234TL Instancia => SingletonT_234TL<IdiomasManager_234TL>.GetInstance();

        public void NotificarActuales()
        {
            if (_idiomas != null)
            {
                Notify(_idiomas);
            }
            else
            {
                throw new InvalidOperationException("No hay idiomas cargados para notificar.");
            }
        }

        public TraduccionesClase_234TL ObtenerIdiomasActuales()
        {
            if (_idiomas == null)
            {
                throw new InvalidOperationException("No hay traducciones cargadas.");
            }
            return _idiomas;
        }

        public void CambiarIdioma(string IdiomasCodigo)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string RutaArchivo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Idiomas", $"{IdiomasCodigo}.json");
            if (!File.Exists(RutaArchivo))
            {
                throw new FileNotFoundException($"El archivo de idioma '{IdiomasCodigo}.json' no se encontró en la ruta '{RutaArchivo}'.");
            }
            try
            {
                string json = File.ReadAllText(RutaArchivo);
                _idiomas = JsonSerializer.Deserialize<TraduccionesClase_234TL>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (_idiomas == null)
                {
                    throw new Exception($"El archivo de idioma '{IdiomasCodigo}.json' está vacío o no contiene datos válidos.");
                }
                Notify(_idiomas);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al cargar el idioma '{IdiomasCodigo}': {ex.Message}", ex);
            }
        }
    }
}