using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Context;
using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Vehiculo
    {
        [Key]
        public int Id { get; set; }
        public string Tipo { get; set; }

        public int CapacidadTotal { get; set; }
        public int EspaciosOcupados  { get; set; }
        public int Tarifa { get; set; }
        public int EspaciosDisponibles => CapacidadTotal - EspaciosOcupados;


        public void ActualizarEspaciosOcupados(BibliotecaContext context)
        {
            this.EspaciosOcupados = context.Ticket.Count(t => t.Tipo == this.Tipo && t.FechaSalida == null);
                
        }
     
    }
}
