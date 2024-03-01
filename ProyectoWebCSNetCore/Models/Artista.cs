using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWebCSNetCore.Models
{
    [Table("ARTISTA")]
    public class Artista
    {
        [Key]
        [Column("IDARTISTA")]
        public int IdArtista { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }
        [Column("SPOTIFY")]
        public string Spotify { get; set; }
        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }
    }
}
