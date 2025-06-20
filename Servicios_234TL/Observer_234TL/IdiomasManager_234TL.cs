using Servicios_234TL.Singleton_234TL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servicios_234TL.Observer_234TL
{
    public class IdiomasManager_234TL : Subject_234TL<Dictionary<string, string>>
    {

        private Dictionary<string, string> _idiomas = new();

        public IdiomasManager_234TL()
        { }

        public static IdiomasManager_234TL Instancia => SingletonT_234TL<IdiomasManager_234TL>.GetInstance();

        public void NotificarActuales()
        {
            if (_idiomas != null && _idiomas.Any())
            {
                Notify(_idiomas);
            }
            else
            {
                throw new InvalidOperationException("No hay idiomas cargados para notificar.");
            }
        }

        public Dictionary<string, string> ObtenerIdiomasActuales()
        {
            if (_idiomas == null || !_idiomas.Any())
            {
                throw new InvalidOperationException("No hay idiomas cargados.");
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
                _idiomas = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, string>>(json);

                if (_idiomas == null || !_idiomas.Any())
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