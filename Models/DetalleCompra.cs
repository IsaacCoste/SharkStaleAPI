using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharkStyleApi.Models;
public class DetalleCompra
{
    public int DetalleCompraId { get; set; }
    public int CompraId { get; set; }
    public int DetalleProductoId { get; set; }
    public int Cantidad { get; set; }
    public decimal Precio { get; set; }
    public Compra Compra { get; set; }
    public DetalleProducto DetalleProducto { get; set; }
}
