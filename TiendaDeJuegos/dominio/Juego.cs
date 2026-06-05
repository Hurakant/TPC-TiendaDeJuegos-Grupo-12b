using dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    public class Juego
    {
        public int id { get; set; }
        public string name { get; set; }
        public string released { get; set; }
        public double rating { get; set; }
        public string background_image { get; set; }
    }

    public class RespuestaRAWG
    {
        public List<Juego> results { get; set; }
    }
}
