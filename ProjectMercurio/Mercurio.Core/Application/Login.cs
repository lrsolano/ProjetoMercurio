using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    public static class Login
    {
        public static bool GetLogin(string usuario, string senha)
        {
            bool result = false;

            Usuario user = Usuario.FindByName(usuario);
            if(user == null)
            {
                throw new MercurioCoreException("Usuario não existe");
            }

            string senhaHash = Password.ComputeHash(senha, "SHA256");
            if (senhaHash == user.Senha)
            {
                result = true;
            }

            return result;
        }

        public static Usuario Signin(string usuario, string senha)
        {

            Usuario user = Usuario.FindByName(usuario);
            if (user == null)
            {
                throw new MercurioCoreException("Usuario/senha incorretos");
            }

            string senhaHash = Password.ComputeHash(senha, "SHA256");
            if (senhaHash != user.Senha)
            {
                throw new MercurioCoreException("Usuario/senha incorretos");
            }

            return user;
        }
    }
}
