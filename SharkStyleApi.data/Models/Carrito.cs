using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharkStyleApi.data.Models;
public class Carrito
{
    public int CarritoId { get; set; }
    public int UsuarioId { get; set; }
    public bool Pagado { get; set; } = false;
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public Usuario Usuario { get; set; }

    public ICollection<DetalleCarrito> Detalles { get; set; } = new List<DetalleCarrito>();
}
