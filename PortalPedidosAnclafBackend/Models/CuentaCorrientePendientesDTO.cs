using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalPedidosAnclafBackend.Models
{
    public class CuentaCorrientePendientesDTO
    {
        public string Empresaaplicacion { get; set; }
        public string Formularioaplicacion { get; set; }
        public int Numeroformularioaplicacion { get; set; }
        public DateTime Fechavencimiento { get; set; }
        public decimal Importenacional { get; set; }
     
    }
}
