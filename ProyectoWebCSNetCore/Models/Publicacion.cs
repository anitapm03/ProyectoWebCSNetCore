using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWebCSNetCore.Models
{
    [Table("PUBLICACION")]
    public class Publicacion
    {
        [Key]
        [Column("IDPUBLICACION")]
        public int IdPublicacion { get; set; }
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Column("TEXTO")]
        public string Texto { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }
        [Column("FECHA")]
        public DateTime Fecha { get; set; }


    }
}
