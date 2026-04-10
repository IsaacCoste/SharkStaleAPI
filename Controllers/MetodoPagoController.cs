using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharkStyleApi.DAL;
using SharkStyleApi.Models;

namespace SharkStyleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetodoPagoController : ControllerBase
    {
        private readonly Contexto _context;

        public MetodoPagoController(Contexto context)
        {
            _context = context;
        }

        // GET: api/MetodoPago
        [HttpGet]
        public async Task<IActionResult> GetMetodos()
        {
            var metodos = await _context.MetodosPago
                .Select(m => new { m.MetodoPagoId, m.Nombre })
                .ToListAsync();

            return Ok(metodos);
        }
    }
}
