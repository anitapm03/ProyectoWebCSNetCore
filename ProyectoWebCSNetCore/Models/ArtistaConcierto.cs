using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWebCSNetCore.Models
{
    [Table("ARTISTA_CONCIERTO")]
    public class ArtistaConcierto
    {
        [Key]
        [Column("IDARTISTA_CONCIERTO")]
        public int IdArtistaConcierto { get; set; }

        [Column("IDARTISTA")]
        public int IdArtista { get; set; }

        [Column("IDCONCIERTO")]
        public int IdConcierto { get; set; }
    }
}
