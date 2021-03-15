using System;
using System.Collections.Generic;

#nullable disable

namespace PortalPedidosAnclafBackend.Entities
{
    public partial class Bonificacion
    {
        public int Id { get; set; }
        public string Idgrupobonificacion { get; set; }
        public string Tipoproducto { get; set; }
        public int? Idnumerorubro { get; set; }
        public string Valorrubro { get; set; }
        public decimal? Bonificacion1 { get; set; }
        public decimal? Bonificacion2 { get; set; }
        public decimal? Bonificacion3 { get; set; }
    }
}
