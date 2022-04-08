using BD_Usuarios.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Usuarios.Repositories
{
    internal class UserRepository
    {
        usuariosContext context = new usuariosContext();
        public IEnumerable<Usuario> GetAll()
        {
            return context.Usuarios.OrderBy(x => x.Nombre);
        }

        public Usuario GetByEmail(Usuario u)
        {
            return context.Usuarios.First(x => x.EMail == u.EMail);
        }

        public void insert(Usuario n)
        {
            context.Database.ExecuteSqlRaw($"call sp_Registro" +
                $"('{n.EMail}','{n.Nombre}','{n.Direccion}', " +
                $"'{n.EMail}','{n.Contraseña}');");
        }


        public void Update(Usuario n)
        {

            context.Database.ExecuteSqlRaw($"call sp_Editar" +
                $"('{n.Id}','{n.EMail}','{n.Nombre}','{n.Direccion}', " +
                $"'{n.EMail}','{n.Contraseña}');");
        }

        public void Delete(Usuario n)
        {

            if (n == null)
            {
                throw new ArgumentException("Eliga un registro para eliminar");
            }
            else
            {
                context.Remove(n);
                context.SaveChanges();
            }

        }


        public bool Validate(Usuario n)
        {
            if (string.IsNullOrWhiteSpace(n.Nombre))
            {
                throw new ArgumentException("Especifique un nombre para el Usuario");
            }
            if (string.IsNullOrWhiteSpace(n.EMail))
            {
                throw new ArgumentException("Especifique una dirección E-Mail");
            }
            if (string.IsNullOrWhiteSpace(n.Direccion))
            {
                throw new ArgumentException("Especifique una Dirección");
            }
            if (string.IsNullOrWhiteSpace(n.Telefono))
            {
                throw new ArgumentException("Especifique un Telefono");
            }
            if (string.IsNullOrWhiteSpace(n.Contraseña))
            {
                throw new ArgumentException("Especifique una Contraseña");
            }

            return true;

        }
    }
}
