using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharkStyleApi.Models;

public class Talla
{
    public int TallaId { get; set; }
    public string Medida { get; set; } = string.Empty;
    public ICollection<DetalleProducto> Detalles { get; set; } = new List<DetalleProducto>();
}
