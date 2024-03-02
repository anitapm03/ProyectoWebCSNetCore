using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace ProyectoWebCSNetCore.Models
{
    [Table("V_EVENTOS")]
    public class Evento
    {
        [Key]
        [Column("ID")]
        public Int64 Id { get; set; }
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
        [Column("DIRECCION")]
        public string Direccion { get; set; }
        [Column("NOMBRESALA")]
        public string NombreSala { get; set; }
        [Column("NOMBREPROVNCIA")]
        public string Provincia { get; set; }
    }
}
