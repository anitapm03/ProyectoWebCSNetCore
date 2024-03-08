using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWebCSNetCore.Models
{
    [Table("V_PETICIONES")]
    public class Peticion
    {
        [Key]
        [Column("IDPETICION")]
        public int IdPeticion { get; set; }
        [Column("NOMBREARTISTA")]
        public string NombreArtista { get; set; }
        [Column("PROVINCIA")]
        public string Provincia { get; set; }
        [Column("FECHA")]
        public DateTime Fecha { get; set; }
        [Column("TELEFONO")]
        public string Telefono { get; set; }
    }
}
