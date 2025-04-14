using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using ProyectoFinal.Context;
using ProyectoFinal.Models;
using ProyectoFinal.Repository;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiculoController : ControllerBase
    {
        private readonly IRepository<Vehiculo> repository;
        private readonly BibliotecaContext context;



        public VehiculoController(IRepository<Vehiculo> repository)
        {
            this.repository = repository;
            this.context = context;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            return Ok(repository.GetAll());
        }
        [HttpPost]

        public void Create(Vehiculo vehiculo)
        {

            repository.Set(vehiculo);
        }


        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromBody] Vehiculo vehiculo)
        {
            var existingVehiculo = repository.GetById(id);
            if (existingVehiculo == null)
            {
                return NotFound($"No se encontró un vehículo con Id {id}");
            }

            existingVehiculo.Tipo = vehiculo.Tipo;
            existingVehiculo.CapacidadTotal = vehiculo.CapacidadTotal;
            existingVehiculo.Tarifa = vehiculo.Tarifa; 
            repository.Update(existingVehiculo);

            return Ok($"Vehículo con Id {id} actualizado correctamente.");

        }

        [HttpDelete("{id}")]

        public void Delete(int id)
        {
            repository.Delete(id);
        }
        
    }
}
