namespace ClimaxAI.API.Models
{
    public class ClimaDto
    {
        public double Temperatura { get; set; }
        public double VelocidadViento { get; set; }
        public int CodigoClima { get; set; }
        public string Descripcion { get; set; }

        public int IndiceImpacto { get; set; }
        public string NivelRiesgo { get; set; }
        public string Recomendacion { get; set; }
    }
}
