using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable

namespace BD_Usuarios.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Bitacoras = new HashSet<Bitacora>();
        }

        public int Id { get; set; }
        public string EMail { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public string Contraseña { get; set; }

        public virtual ICollection<Bitacora> Bitacoras { get; set; }

        public static implicit operator Usuario(ObservableCollection<Usuario> v)
        {
            throw new NotImplementedException();
        }
    }
}
