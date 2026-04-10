using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharkStyleApi.data.Models;

public class Usuario
{
    public int UsuarioId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public ICollection<Carrito> Carritos { get; set; } = new List<Carrito>();
    public ICollection<Compra> Compras { get; set; } = new List<Compra>();
}
