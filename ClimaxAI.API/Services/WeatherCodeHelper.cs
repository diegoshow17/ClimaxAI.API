namespace ClimaxAI.API.Services
{
    public static class WeatherCodeHelper
    {
        public static string ObtenerDescripcion(int code)
        {
            return code switch
            {
                0 => "Cielo despejado",
                1 or 2 => "Parcialmente nublado",
                3 => "Nublado",
                45 or 48 => "Niebla",
                51 or 53 or 55 => "Llovizna",
                61 or 63 or 65 => "Lluvia",
                71 or 73 or 75 => "Nieve",
                80 or 81 or 82 => "Lluvia fuerte",
                _ => "Condición desconocida"
            };
        }
    }
}
