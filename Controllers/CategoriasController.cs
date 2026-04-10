using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharkStyleApi.DAL;
using SharkStyleApi.data.Models;

namespace SharkStyleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly Contexto _context;

        public CategoriasController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<IActionResult> GetCategorias()
        {
            var categorias = await _context.Categorias
                .Select(c => new { c.CategoriaId, c.Nombre })
                .ToListAsync();
            return Ok(categorias);
        }

        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoria(int id)
        {
            var categoria = await _context.Categorias
                .Where(c => c.CategoriaId == id)
                .Select(c => new { c.CategoriaId, c.Nombre })
                .FirstOrDefaultAsync();

            if (categoria == null)
            {
                return NotFound(new { message = "Categoría no encontrada." });
            }

            return Ok(categoria);
        }

        // POST: api/Categorias
        [HttpPost]
        public async Task<IActionResult> PostCategoria(Categoria categoria)
        {
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCategoria), new { id = categoria.CategoriaId }, categoria);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound(new { message = "Categoría no encontrada." });
            }

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Categoría eliminada." });
        }
    }
}
