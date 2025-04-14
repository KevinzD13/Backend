using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using ProyectoFinal.Models;
using ProyectoFinal.Repository;

namespace ProyectoFinal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IRepository<Usuario> context;

        public UsuarioController(IRepository<Usuario> context)
        {
            this.context = context;
        }

        [HttpGet]

        public IActionResult GetAll()
        {
            return Ok(context.GetAll());
        }

        [HttpPost]

        public void Agregar(Usuario usuario)
        {
            context.Set(usuario);
        }

        [HttpPut("{id}")]

        public void Editar(Usuario usuario)
        {
            context.Update(usuario);
        }
        
        [HttpDelete("{id}")]
        public void Eliminar(int id)
        {
            context.Delete(id);
        }

        [HttpPost("Login")]

        public IActionResult Login(Login request)
        {
            var user = context.Login(request.Username, request.Password);
          
            if (user != null)
            {
                return Ok("Login exitoso");
            }
            else
            {
                return Unauthorized("Credenciales incorrectas");
            }
        }
    }
}
