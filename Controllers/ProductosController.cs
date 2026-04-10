using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharkStyleApi.DAL;
using SharkStyleApi.data.Models;

namespace SharkStyleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly Contexto _context;

        public ProductosController(Contexto context)
        {
            _context = context;
        }

        // GET: api/Productos
        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .Select(p => new
                {
                    p.ProductoId,
                    p.Titulo,
                    p.Precio,
                    p.Descripcion,
                    p.Imagen,
                    p.Impuesto,
                    p.FechaCreacion,
                    Categoria = p.Categoria.Nombre
                })
                .ToListAsync();

            return Ok(productos);
        }

        // GET: api/Productos/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducto(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .Include(p => p.Detalles)
                    .ThenInclude(d => d.Talla)
                .Where(p => p.ProductoId == id)
                .Select(p => new
                {
                    p.ProductoId,
                    p.Titulo,
                    p.Precio,
                    p.Descripcion,
                    p.Imagen,
                    p.Impuesto,
                    p.FechaCreacion,
                    Categoria = p.Categoria.Nombre,
                    TallasDisponibles = p.Detalles.Select(d => new
                    {
                        d.DetalleProductoId,
                        Talla = d.Talla.Medida,
                        d.Existencia
                    })
                })
                .FirstOrDefaultAsync();

            if (producto == null)
            {
                return NotFound(new { message = "Producto no encontrado." });
            }

            return Ok(producto);
        }

        // POST: api/Productos
        [HttpPost]
        public async Task<IActionResult> PostProducto(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProducto), new { id = producto.ProductoId }, producto);
        }

        // PUT: api/Productos/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducto(int id, Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return BadRequest(new { message = "Id del producto no coincide." });
            }

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Productos.Any(e => e.ProductoId == id))
                {
                    return NotFound(new { message = "Producto no encontrado." });
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Productos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound(new { message = "Producto no encontrado." });
            }

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Producto eliminado con éxito." });
        }
    }
}
