using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Context;
using ProyectoFinal.Models;

namespace ProyectoFinal.Repository
{
    public class RepositoryServices<T> : IRepository<T> where T : class
    {
        private readonly BibliotecaContext contexto;
        DbSet<T> dbSet;

        public RepositoryServices(BibliotecaContext contexto)
        {
            this.contexto = contexto;
            dbSet = contexto.Set<T>();
        }



        public void Delete(int id)
        {
            var entidad = GetById(id);
                dbSet.Remove(entidad);
            contexto.SaveChanges();
        }

        public List<T> GetAll()
        {
            
            return dbSet.ToList();
        }

        public T GetById(int id)
        {
            return dbSet.Find(id);
        }

        public Ticket GetById(object idVehiculo)
        {
            throw new NotImplementedException();
        }

        public void Set(T entidad)
        {
            dbSet.Add(entidad);
            contexto.SaveChanges();
        }

        public void Update(T entidad)
        {
            dbSet.Attach(entidad);
            
            contexto.Entry(entidad).State = EntityState.Modified;
            contexto.SaveChanges();
        }

        public Usuario Login(string usuario, string clave)
        {
            return contexto.Usuario.FirstOrDefault(r => r.Nombre_usuario == usuario && r.Contra_usuario == clave);
        }
    }
}
