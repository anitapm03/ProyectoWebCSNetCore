using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWebCSNetCore.Models
{
    [Table("SALA")]
    public class Sala
    {
        [Key]
        [Column("IDSALA")]
        public int IdSala { get; set; }
        [Column("DIRECCION")]
        public string Direccion { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("IDPROVINCIA")]
        public int IdProvincia { get; set; }
        
    }
}
