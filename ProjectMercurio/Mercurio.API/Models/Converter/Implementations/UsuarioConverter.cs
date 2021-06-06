using Mercurio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    public class UsuarioConverter : IParser<UsuarioV, Usuario>, IParser<Usuario, UsuarioV>
    {
        public UsuarioV Parser(Usuario origin)
        {
            if (origin == null) return null;
            UsuarioV usuario = new UsuarioV { Id = origin.Id, Idade = origin.Idade, Nome = origin.Nome };
            return usuario;
        }

        public Usuario Parser(UsuarioV origin)
        {
            if (origin == null) return null;
            Usuario usuario = Usuario.FindById(origin.Id);
            if (usuario == null) return null;
            usuario.ChangeName(origin.Nome);
            usuario.ChangeIdade(origin.Idade);
            return usuario;
        }

        public List<Usuario> Parser(List<UsuarioV> origin)
        {
            if (origin.Count == 0) return null;
            List<Usuario> usuarios = new List<Usuario>();
            foreach(UsuarioV u in origin)
            {
                usuarios.Add(Parser(u));
            }
            return usuarios;
        }

        public List<UsuarioV> Parser(List<Usuario> origin)
        {
            if (origin.Count == 0) return null;
            List<UsuarioV> usuarios = new List<UsuarioV>();
            foreach (Usuario u in origin)
            {
                usuarios.Add(Parser(u));
            }
            return usuarios;
        }
    }
}
