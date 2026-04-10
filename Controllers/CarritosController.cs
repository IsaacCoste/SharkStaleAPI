using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharkStyleApi.DAL;
using SharkStyleApi.Models;

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

        // GET: api/Carritos/Usuario/5
        [HttpGet("Usuario/{usuarioId}")]
        public async Task<IActionResult> GetCarritoPorUsuario(int usuarioId)
        {
            var carrito = await _context.Carritos
                .Include(c => c.Detalles)
                    .ThenInclude(d => d.DetalleProducto)
                        .ThenInclude(dp => dp.Producto)
                .Include(c => c.Detalles)
                    .ThenInclude(d => d.DetalleProducto)
                        .ThenInclude(dp => dp.Talla)
                .FirstOrDefaultAsync(c => c.UsuarioId == usuarioId && !c.Pagado);

            if (carrito == null)
            {
                return Ok(new { message = "Carrito vacío" });
            }

            var response = new
            {
                carrito.CarritoId,
                carrito.UsuarioId,
                Detalles = carrito.Detalles.Select(d => new
                {
                    d.DetalleCarritoId,
                    d.DetalleProductoId,
                    d.Cantidad,
                    d.Precio,
                    Producto = d.DetalleProducto.Producto.Titulo,
                    Imagen = d.DetalleProducto.Producto.Imagen,
                    Talla = d.DetalleProducto.Talla.Medida
                }),
                Total = carrito.Detalles.Sum(d => d.Cantidad * d.Precio)
            };

            return Ok(response);
        }

        // POST: api/Carritos/Agregar
        [HttpPost("Agregar")]
        public async Task<IActionResult> AgregarAlCarrito([FromBody] AddToCartRequest request)
        {
            var stock = await _context.DetallesProducto
                .Include(dp => dp.Producto)
                .FirstOrDefaultAsync(dp => dp.DetalleProductoId == request.DetalleProductoId);

            if (stock == null) return NotFound(new { message = "Producto no encontrado." });
            if (stock.Existencia < request.Cantidad) return BadRequest(new { message = "Stock insuficiente." });

            var carrito = await _context.Carritos
                .Include(c => c.Detalles)
                .FirstOrDefaultAsync(c => c.UsuarioId == request.UsuarioId && !c.Pagado);

            if (carrito == null)
            {
                carrito = new Carrito { UsuarioId = request.UsuarioId };
                _context.Carritos.Add(carrito);
                await _context.SaveChangesAsync();
            }

            var detalle = carrito.Detalles.FirstOrDefault(d => d.DetalleProductoId == request.DetalleProductoId);
            if (detalle != null)
            {
                detalle.Cantidad += request.Cantidad;
            }
            else
            {
                detalle = new DetalleCarrito
                {
                    CarritoId = carrito.CarritoId,
                    DetalleProductoId = request.DetalleProductoId,
                    Cantidad = request.Cantidad,
                    Precio = stock.Producto.Precio
                };
                _context.DetallesCarrito.Add(detalle);
            }

            await _context.SaveChangesAsync();
            return Ok(new { message = "Producto agregado al carrito." });
        }

        // PUT: api/Carritos/ActualizarCantidad
        [HttpPut("ActualizarCantidad")]
        public async Task<IActionResult> ActualizarCantidad([FromBody] UpdateCartRequest request)
        {
            var detalle = await _context.DetallesCarrito
                .Include(d => d.DetalleProducto)
                .FirstOrDefaultAsync(d => d.DetalleCarritoId == request.DetalleCarritoId);

            if (detalle == null) return NotFound(new { message = "Item no encontrado en el carrito." });

            if (detalle.DetalleProducto.Existencia < request.NuevaCantidad)
                return BadRequest(new { message = "Stock insuficiente." });

            detalle.Cantidad = request.NuevaCantidad;
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cantidad actualizada." });
        }

        // DELETE: api/Carritos/Eliminar/5
        [HttpDelete("Eliminar/{detalleId}")]
        public async Task<IActionResult> EliminarDelCarrito(int detalleId)
        {
            var detalle = await _context.DetallesCarrito.FindAsync(detalleId);
            if (detalle == null) return NotFound(new { message = "Item no encontrado." });

            _context.DetallesCarrito.Remove(detalle);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Item eliminado del carrito." });
        }
    }

    public class AddToCartRequest
    {
        public int UsuarioId { get; set; }
        public int DetalleProductoId { get; set; }
        public int Cantidad { get; set; }
    }

    public class UpdateCartRequest
    {
        public int DetalleCarritoId { get; set; }
        public int NuevaCantidad { get; set; }
    }
}
