using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registro_De_Roles.Entidades
{
    public class DetalleRoles
    {
        [Key]
        public int Id { get; set; }
        public int RolId { get; set; }
        public bool EsAsignado { get; set; }
        public int PermisoId { get; set; }

        [ForeignKey("PermisoId")]
        public virtual Permisos Permisos { get; set; }
    }
}
