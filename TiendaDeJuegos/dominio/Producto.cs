using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Producto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public bool Activo { get; set; }

        public string ImagenUrl { get; set; }

        // Relación
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public List<JuegoAccesibilidad> Accesibilidades { get; set; } = new List<juegoAccesibilidad>();
    }
}
