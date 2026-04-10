using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharkStyleApi.Models;

public class DetalleProducto
{
    public int DetalleProductoId { get; set; }
    public int ProductoId { get; set; }
    public int TallaId { get; set; }
    public int Existencia { get; set; }
    public Producto Producto { get; set; }
    public Talla Talla { get; set; }
}
