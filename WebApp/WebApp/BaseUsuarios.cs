using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
    public static class BaseUsuarios
    {
        public static IEnumerable<Usuario> Usuarios()
        {
            return new List<Usuario>
            {
                new Usuario {Nome = "Fulano", Senha = "1234"},
                new Usuario {Nome = "Beltrano", Senha = "1234"},
                new Usuario {Nome = "Cicrano", Senha = "1234"}
            };
        }
    }

    public class Usuario
    {
        public string Nome { get; set; }
        public string Senha { get; set; }
    }
}