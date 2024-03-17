using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectoWebCSNetCore.Models
{
    [Table("ARTISTA_GENERO")]
    public class ArtistaGenero
    {
        [Key]
        [Column("IDARTISTA_GENERO")]
        public int IdArtistaConcierto { get; set; }

        [Column("IDARTISTA")]
        public int IdArtista { get; set; }

        [Column("IDGENERO")]
        public int IdGenero { get; set; }
    }
}
