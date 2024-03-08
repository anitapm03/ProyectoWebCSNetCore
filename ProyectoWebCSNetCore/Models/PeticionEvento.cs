using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWebCSNetCore.Models
{
    [Table("PETICIONEVENTO")]
    public class PeticionEvento
    {
        [Key]
        [Column("IDPETICION")]
        public int IdPeticion { get; set; }
        [Column("NOMBREARTISTA")]
        public string NombreArtista { get; set; }
        [Column("IDPROVINCIA")]
        public int IdProvincia { get; set; }
        [Column("FECHA")]
        public DateTime Fecha { get; set; }
        [Column("TELEFONO")]
        public string Telefono { get; set; }
    }
}
