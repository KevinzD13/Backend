using System.ComponentModel.DataAnnotations;

namespace ProyectoFinal.Models
{
    public class Usuario
    {
        [Key]
        public int Id {  get; set; }
        public string Nombre_usuario { get; set; }
        public string Contra_usuario { get; set; }
        public string Rol_usuario { get; set; }
    }
}
