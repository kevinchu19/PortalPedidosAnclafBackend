using System;
using System.Collections.Generic;

#nullable disable

namespace PortalPedidosAnclafBackend.Entities
{
    public partial class CuentaCorriente
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public string Codigoformulario { get; set; }
        public int Numeroformulario { get; set; }
        public string Empresaaplicacion { get; set; }
        public string Formularioaplicacion { get; set; }
        public int Numeroformularioaplicacion { get; set; }
        public int Cuota { get; set; }
        public string Idcliente { get; set; }
        public DateTime Fechamovimiento { get; set; }
        public DateTime Fechavencimiento { get; set; }
        public decimal Importenacional { get; set; }
        public decimal Importeextranjera { get; set; }
    }
}
