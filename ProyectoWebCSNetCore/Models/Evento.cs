﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace ProyectoWebCSNetCore.Models
{
    [Table("V_EVENTOS")]
    public class Evento
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
        [Column("GRUPO")]
        public string Grupo { get; set; }
        [Column("DESTACADO")]
        public bool Destacado { get; set; }
        [Column("DIRECCION")]
        public string Direccion { get; set; }
        [Column("NOMBRESALA")]
        public string NombreSala { get; set; }
        [Column("NOMBREPROVNCIA")]
        public string Provincia { get; set; }
    }
}
