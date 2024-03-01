using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace ProyectoWebCSNetCore.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public string Imagen { get; set; }
        public string Entradas { get; set; }
        public string Direccion { get; set; }
        public string NombreSala { get; set; }
        public string Provincia { get; set; }
    }
}
