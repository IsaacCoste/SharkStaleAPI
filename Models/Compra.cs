using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharkStyleApi.Models;
public class Compra
{
    public int CompraId { get; set; }
    public int UsuarioId { get; set; }
    public int MetodoPagoId { get; set; }
    public decimal Total { get; set; }
    public string Estado { get; set; } = "Pendiente";
    public DateTime Fecha { get; set; } = DateTime.Now;
    public Usuario Usuario { get; set; }
    public MetodoPago MetodoPago { get; set; }
    public ICollection<DetalleCompra> Detalles { get; set; } = new List<DetalleCompra>();
}
