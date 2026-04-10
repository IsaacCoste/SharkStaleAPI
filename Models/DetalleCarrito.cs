using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharkStyleApi.Models;

public class DetalleCarrito
{
    public int DetalleCarritoId { get; set; }
    public int CarritoId { get; set; }
    public int DetalleProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public Carrito Carrito { get; set; }
    public DetalleProducto DetalleProducto { get; set; }
}
