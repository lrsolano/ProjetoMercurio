using System;
using Mercurio.Core;
using System.Text;
using System.Security.Cryptography;

namespace MecurioCoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Carrinho c = Carrinho.FindByName("Carrinho 1");
                c.FinalizarCorrida();

            }
            catch(MercurioCoreException ex)
            {
                Console.WriteLine(string.Format("ERRO NO CORE => {0}", ex));
            }
            catch(DBConnectionException ex)
            {
                Console.WriteLine(string.Format("ERRO NO BANCO DE DADOS => {0}", ex));
            }
            catch(Exception ex)
            {
                Console.WriteLine(string.Format("ERRO GENÉRICO => {0}", ex));
            }
            



        }
    }
}
