using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWebCSNetCore.Models
{

    [Table("V_USERPUBLI")]
    public class UserPubli
    {
		[Key]
		[Column("IDPUBLICACION")]
		public int IdPubli { get; set; }
		[Column("NOMBRE")]
		public string NombreUsuario { get; set; }
        [Column("IMGUSUARIO")]
        public string ImgUser { get; set; }
        [Column("TEXTO")]
		public string Texto { get; set; }
        [Column("IMAGEN")]
        public string Imagen { get; set; }
        [Column("FECHA")]
        public DateTime Fecha { get; set; }

    }
}
/*
 ALTER VIEW V_USERPUBLI
AS
	SELECT P.IDPUBLICACION, U.NOMBRE, U.IMAGEN AS IMGUSUARIO, P.TEXTO, 
	P.IMAGEN, P.FECHA 
	FROM PUBLICACION P
	INNER JOIN USUARIO U
	ON U.IDUSUARIO = P.IDUSUARIO
GO
*/
