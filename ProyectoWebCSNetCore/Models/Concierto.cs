using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWebCSNetCore.Models
{
    [Table("CONCIERTO")]
    public class Concierto
    {
        [Key]
        [Column("IDCONCIERTO")]
        public int IdConcierto { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("FECHA")]
        public DateTime Fecha { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }
        [Column("ENTRADAS")]
        public string Entradas { get; set; }
        [Column("IDSALA")]
        public int IdSala { get; set; }
        [Column("DESTACADO")]
        public bool Destacado { get; set; }
    }
}
