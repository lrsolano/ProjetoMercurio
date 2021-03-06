using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    public class PasswordChange
    {
        public string NovaSenha {  get; set; }
        public string ConfirmarSenha { get; set; }
        public string SenhaAntiga { get; set; }

        public PasswordChange(string novaSenha, string confirmarSenha, string senhaAntiga)
        {
            if(novaSenha != confirmarSenha)
            {
                throw new MercurioCoreException("Senhas digitadas diferentes");
            }
            NovaSenha = novaSenha;
            ConfirmarSenha = confirmarSenha;
            SenhaAntiga = senhaAntiga;
        }
    }
}
