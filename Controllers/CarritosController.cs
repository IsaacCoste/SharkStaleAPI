using Library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharkStyleApi.DAL;

namespace SharkStyleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarritosController : ControllerBase
    {
        private readonly Contexto _context;

        public CarritosController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Carritos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carritos>>> GetCarritos()
        {
            return await _context.Carritos.ToListAsync();
        }

        // GET: api/Carritos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carritos>> GetCarritos(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id);

            if (carrito == null)
            {
                return NotFound();
            }

            return carrito;
        }

        // PUT: api/Carritos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarritos(int id, Carritos carrito)
        {
            if (id != carrito.CarritoId)
            {
                return BadRequest();
            }

            _context.Entry(carrito).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarritosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Carritos
        [HttpPost]
        public async Task<ActionResult<Carritos>> PostCarritos(Carritos carrito)
        {
            _context.Carritos.Add(carrito);
            await _context.SaveChangesAsync();
            return Ok(carrito);
        }

        // DELETE: api/Carritos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarritos(int id)
        {
            var carrito = await _context.Carritos.FindAsync(id);
            if (carrito == null)
            {
                return NotFound();
            }

            _context.Carritos.Remove(carrito);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarritosExists(int id)
        {
            return _context.Carritos.Any(e => e.CarritoId == id);
        }
    }
}
