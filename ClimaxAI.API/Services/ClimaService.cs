using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ClimaxAI.API.Models;

namespace ClimaxAI.API.Services
{
    public class ClimaService
    {
        private readonly HttpClient _httpClient;

        public ClimaService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ClimaDto?> ObtenerClimaAsync(double lat, double lon, string sector)
        {
            var url = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true";

            var response = await _httpClient.GetAsync(url);
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return null;

            using var doc = JsonDocument.Parse(content);
            var root = doc.RootElement;

            var current = root.GetProperty("current_weather");

            var temperatura = current.GetProperty("temperature").GetDouble();
            var viento = current.GetProperty("windspeed").GetDouble();
            var codigo = current.GetProperty("weathercode").GetInt32();

            // 🔥 Cálculo de índice
            int indice = 0;

            sector = sector?.ToLower() ?? "";

            // 🌾 AGRICULTURA
            if (sector == "agricultura")
            {
                if (temperatura < 5 || temperatura > 35)
                    indice += 20;

                if (viento > 25)
                    indice += 20;

                if (codigo >= 80 && codigo <= 82)
                    indice += 60; // lluvia pesa mucho
            }

            // 🚛 LOGISTICA
            else if (sector == "logistica")
            {
                if (temperatura < 5 || temperatura > 35)
                    indice += 20;

                if (viento > 25)
                    indice += 50; // viento pesa mucho

                if (codigo >= 80 && codigo <= 82)
                    indice += 30;
            }

            // 🏗 CONSTRUCCION
            else if (sector == "construccion")
            {
                if (temperatura < 5 || temperatura > 35)
                    indice += 40; // temperatura pesa más

                if (viento > 25)
                    indice += 40;

                if (codigo >= 80 && codigo <= 82)
                    indice += 20;
            }

            // Sector desconocido
            else
            {
                if (temperatura < 5 || temperatura > 35)
                    indice += 30;

                if (viento > 25)
                    indice += 30;

                if (codigo >= 80 && codigo <= 82)
                    indice += 40;
            }

            if (indice > 100)
                indice = 100;


            string nivel = indice switch
            {
                <= 30 => "Bajo",
                <= 70 => "Medio",
                _ => "Alto"
            };

            // 🔥 Recomendación por sector
            string recomendacion = GenerarRecomendacion(sector, nivel);

            return new ClimaDto
            {
                Temperatura = temperatura,
                VelocidadViento = viento,
                CodigoClima = codigo,
                Descripcion = WeatherCodeHelper.ObtenerDescripcion(codigo),
                IndiceImpacto = indice,
                NivelRiesgo = nivel,
                Recomendacion = recomendacion
            };
        }

        private string GenerarRecomendacion(string sector, string nivel)
        {
            sector = sector?.ToLower() ?? "";

            return sector switch
            {
                "agricultura" => nivel switch
                {
                    "Alto" => "Alto riesgo de afectación en cultivos. Implementar protección inmediata.",
                    "Medio" => "Monitorear humedad del suelo y ajustar riego.",
                    _ => "Condiciones favorables para actividades agrícolas."
                },

                "logistica" => nivel switch
                {
                    "Alto" => "Posibles retrasos en transporte y distribución.",
                    "Medio" => "Planificar rutas alternativas y tiempos de entrega.",
                    _ => "Operaciones logísticas normales."
                },

                "construccion" => nivel switch
                {
                    "Alto" => "Suspender trabajos en altura o exteriores.",
                    "Medio" => "Aplicar protocolos de seguridad adicionales.",
                    _ => "Condiciones adecuadas para construcción."
                },

                _ => "Sector no especificado. Evaluar condiciones manualmente."
            };
        }
    }
}
