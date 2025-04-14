using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Context;
using ProyectoFinal.Models;
using ProyectoFinal.Repository;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IRepository<Ticket> service;
        private readonly BibliotecaContext context;

        public TicketController(IRepository<Ticket> service, BibliotecaContext context)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var tickets = await context.Ticket
                .Join(context.Vehiculo,
                    ticket => ticket.Tipo,
                    vehiculo => vehiculo.Tipo,
                    (ticket, vehiculo) => new
                    {
                        ticket.Id_Vehiculo,
                        ticket.Tipo,
                        ticket.FechaIngreso,
                        ticket.FechaSalida,
                        ticket.Tarifa,
                        ticket.Costo
                    })
                .ToListAsync();

            return Ok(tickets);
        }

        [HttpGet("{id}")]
        public Ticket GetById(int id)
        {
            return service.GetById(id);
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Ticket ticket)
        {
            
                Random rnr = new Random();
                int codigo = rnr.Next(100, 1000);
                ticket.Id_Vehiculo = codigo;

                ticket.FechaIngreso = DateTime.Now;

                var vehiculo = await context.Vehiculo.SingleOrDefaultAsync(v => v.Tipo == ticket.Tipo);

                if (vehiculo == null)
                {
                    return BadRequest("Vehículo no encontrado.");
                }

                ticket.Tarifa = vehiculo.Tarifa;
                ticket.FechaSalida = null;

                await context.Ticket.AddAsync(ticket);
                await context.SaveChangesAsync(); 

                vehiculo.ActualizarEspaciosOcupados(context);

                await context.SaveChangesAsync();
                

                return CreatedAtAction(nameof(GetById), new { id = ticket.Id_Vehiculo }, ticket);
            
            
            
        }

                [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromBody] Ticket ticket) 
        {
            var existingTicket = await context.Ticket.SingleOrDefaultAsync(t => t.Id_Vehiculo == ticket.Id_Vehiculo);

            if (existingTicket == null)
            {
                return NotFound($"No se encontró un ticket con Id_Vehiculo {ticket.Id_Vehiculo}");
            }

            var vehiculo = await context.Vehiculo.SingleOrDefaultAsync(v => v.Tipo == ticket.Tipo);
            if (vehiculo == null)
            {
                return BadRequest($"No se encontró un vehículo del tipo {ticket.Tipo}");
            }

            existingTicket.Tarifa = vehiculo.Tarifa;
            existingTicket.Tipo = ticket.Tipo;
            existingTicket.FechaIngreso = ticket.FechaIngreso;
            existingTicket.FechaSalida = ticket.FechaSalida;


            await context.SaveChangesAsync();

            vehiculo.ActualizarEspaciosOcupados(context);
              await context.SaveChangesAsync();

            


            return Ok(existingTicket);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ticketToDelete = await context.Ticket.SingleOrDefaultAsync(t => t.Id_Vehiculo == id);

            if (ticketToDelete == null)
            {
                return NotFound($"No se encontró un ticket con Id_Vehiculo {id}");
            }

            var vehiculo = await context.Vehiculo.SingleOrDefaultAsync(v => v.Tipo == ticketToDelete.Tipo);

            if (vehiculo == null)
            {
                return BadRequest($"No se encontró un vehículo del tipo {ticketToDelete.Tipo}");
            }

            context.Ticket.Remove(ticketToDelete);
            await context.SaveChangesAsync();

            vehiculo.ActualizarEspaciosOcupados(context);
            await context.SaveChangesAsync();

            return NoContent();  
        }
    }
}
