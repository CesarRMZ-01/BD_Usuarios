using BD_Usuarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BD_Usuarios.Repositories
{
    class UsrLoggedRepository
    {
        usuariosContext context = new usuariosContext();
        public bool Login(Usuario u)
        {
            
            context.Database.ExecuteSqlRaw($"Call sp_InicioSesion" + $"('{u.EMail}','{u.Contraseña}');");
            return ValidarLogin(u);
        }
        public static String GetMD5Hash(string input)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(input);
            bs = x.ComputeHash(bs);
            StringBuilder s = new StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            string hash = s.ToString();
            return hash;
        }
        public bool ValidarLogin(Usuario u)
        {
            string P = GetMD5Hash(u.Contraseña);
            if (context.Usuarios.Any(x => x.EMail == u.EMail & x.Contraseña == P))
                return true;
            else
                return false;
        }
    }
}
