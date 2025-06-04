using Microsoft.AspNetCore.Mvc;
using RotaViagem.Application;
using RotaViagem.Domain;

namespace RotaViagem.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RotaController : ControllerBase
    {
        private readonly RotaService _service;

        public RotaController(RotaService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll() => Ok(_service.GetAll());

        [HttpPost]  
        public IActionResult Add(Rota rota)
        {
            _service.Add(rota);
            return Created("", rota);
        }

        [HttpPut]
        public IActionResult Update(Rota rota)
        {
            _service.Update(rota);
            return NoContent();
        }

        [HttpDelete("{origem}/{destino}")]
        public IActionResult Delete(string origem, string destino)
        {
            _service.Delete(origem, destino);
            return NoContent();
        }

        [HttpGet("melhor")]
        public IActionResult MelhorRota([FromQuery] string origem, [FromQuery] string destino)
        {
            var (caminho, custo) = _service.BuscarMelhorRota(origem, destino);
            if (custo == -1) return NotFound("Rota não encontrada.");

            return Ok(new
            {
                rota = string.Join(" - ", caminho),
                custo
            });
        }
    }
}
