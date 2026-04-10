using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharkStyleApi.Models;
public class MetodoPago
{
    public int MetodoPagoId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
