using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharkStyleApi.data.Models;

public class Producto
{
    public int ProductoId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public int CategoriaId { get; set; }
    public decimal Precio { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public string Imagen { get; set; } = string.Empty;
    public decimal Impuesto { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public Categoria Categoria { get; set; }
    public ICollection<DetalleProducto> Detalles { get; set; } = new List<DetalleProducto>();
}
