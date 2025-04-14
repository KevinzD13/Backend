using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;

namespace ProyectoFinal.Context
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext(DbContextOptions<BibliotecaContext> db) : base(db)
        {

        }

        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<Vehiculo> Vehiculo { get; set; }
        public DbSet<Usuario> Usuario { get; set; }

        
    }
}
