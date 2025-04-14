    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualBasic;
    using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Text.Json.Serialization;

    namespace ProyectoFinal.Models
    {
          public class Ticket
          {
            [Key]
            public int Id_Vehiculo { get; set; }
            public string Tipo { get; set; }
            


            [Required]
            public DateTime FechaIngreso { get; set; }
            public DateTime? FechaSalida { get; set; }

             [Required]
            public int Tarifa { get; set; }




            public decimal? Costo
            {
                get
                {
                    if (!FechaSalida.HasValue)
                    {
                        return null; 
                    }

                    var duration = FechaSalida.Value - FechaIngreso;
                    var totalMinutes = duration.TotalMinutes;

                    if (totalMinutes <= 15)
                    {
                        return 0;
                    }

                    var horasCobrar = Math.Floor(totalMinutes / 60);

                if (totalMinutes % 60 > 15)
                {
                    horasCobrar++;
                }

                return (decimal)horasCobrar * Tarifa;
                }
           
            }


        }
    }
