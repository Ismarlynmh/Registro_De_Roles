using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registro_De_Roles.Entidades
{
    public class Roles
    {
        [Key]
        public int RolId { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public string Descripcion { get; set; }
        public bool EsActivo { get; set; }

        [ForeignKey("RolId")]
        public virtual List<DetalleRoles> Detalle { get; set; } = new List<DetalleRoles>();
    }
}
