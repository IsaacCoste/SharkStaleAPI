using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharkStyleApi.DAL;
using SharkStyleApi.Models;

namespace SharkStyleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly Contexto _context;

        public ComprasController(Contexto context)
        {
            _context = context;
        }

        // POST: api/Compras/Checkout
        [HttpPost("Checkout")]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequest request)
        {
            var carrito = await _context.Carritos
                .Include(c => c.Detalles)
                    .ThenInclude(d => d.DetalleProducto)
                .FirstOrDefaultAsync(c => c.CarritoId == request.CarritoId && !c.Pagado);

            if (carrito == null) return NotFound(new { message = "Carrito no encontrado o ya pagado." });

            // Validate all items stock
            foreach (var item in carrito.Detalles)
            {
                if (item.DetalleProducto.Existencia < item.Cantidad)
                {
                    return BadRequest(new { message = $"Stock insuficiente para el producto {item.DetalleProductoId}." });
                }
            }

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var compra = new Compra
                {
                    UsuarioId = carrito.UsuarioId,
                    MetodoPagoId = request.MetodoPagoId,
                    Total = carrito.Detalles.Sum(d => d.Cantidad * d.Precio),
                    Fecha = DateTime.Now
                };

                _context.Compras.Add(compra);
                await _context.SaveChangesAsync();

                foreach (var item in carrito.Detalles)
                {
                    // Register detail
                    var detalleCompra = new DetalleCompra
                    {
                        CompraId = compra.CompraId,
                        DetalleProductoId = item.DetalleProductoId,
                        Cantidad = item.Cantidad,
                        Precio = item.Precio
                    };
                    _context.DetallesCompra.Add(detalleCompra);

                    // Update inventory
                    item.DetalleProducto.Existencia -= item.Cantidad;
                }

                // Mark cart as paid
                carrito.Pagado = true;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { message = "Compra realizada con éxito.", compraId = compra.CompraId });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return StatusCode(500, new { message = "Error al procesar la compra.", error = ex.Message });
            }
        }

        // GET: api/Compras/Usuario/5
        [HttpGet("Usuario/{usuarioId}")]
        public async Task<IActionResult> GetHistorial(int usuarioId)
        {
            var historial = await _context.Compras
                .Include(c => c.Detalles)
                    .ThenInclude(dc => dc.DetalleProducto)
                        .ThenInclude(dp => dp.Producto)
                .Where(c => c.UsuarioId == usuarioId)
                .OrderByDescending(c => c.Fecha)
                .Select(c => new
                {
                    c.CompraId,
                    c.Fecha,
                    c.Total,
                    Detalles = c.Detalles.Select(dc => new
                    {
                        dc.DetalleProducto.Producto.Titulo,
                        dc.Cantidad,
                        dc.Precio
                    })
                })
                .ToListAsync();

            return Ok(historial);
        }
    }

    public class CheckoutRequest
    {
        public int CarritoId { get; set; }
        public int MetodoPagoId { get; set; }
    }
}
