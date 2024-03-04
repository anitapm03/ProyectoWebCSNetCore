using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectoWebCSNetCore.Models
{
    [Table("USUARIO")]
    public class Usuario
    {
        [Key]
        [Column("IDUSUARIO")]
        public int IdUsuario { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("EMAIL")]
        public string Email { get; set; }
        [Column("CONTRASENA")]
        public string Contrasena { get; set; }
        [Column("ROL")]
        public int Rol { get; set; }
        [Column("BIO")]
        public string Bio { get; set; }
        [Column("IMAGEN")]
        public string FotoPerfil { get; set; }
    }
}
