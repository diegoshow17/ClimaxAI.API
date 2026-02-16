using Microsoft.AspNetCore.Mvc;
using ClimaxAI.API.Services;
using System.Threading.Tasks;

namespace ClimaxAI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ClimaService _climaService;

        public TestController(ClimaService climaService)
        {
            _climaService = climaService;
        }

        [HttpGet("clima")]
        public async Task<IActionResult> ObtenerClima(double lat, double lon, string sector)
        {
            var resultado = await _climaService.ObtenerClimaAsync(lat, lon, sector);

            if (resultado == null)
                return BadRequest("No se pudo obtener el clima");

            return Ok(resultado);
        }

    }
}
