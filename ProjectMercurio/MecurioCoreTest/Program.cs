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

                Usuario u = new Usuario("teste",22);
                u.AddSenha("teste");
                u.AddGrupo(Grupo.FindById(1));
                u.CreateUsuario();

                Pedido p = new Pedido(Usuario.FindByName("leonardo"));
                Item martelo = new Item("Martelo");

                p.AddItem(martelo, 1);
                p.SetRota(Sensor.FindById(1), Sensor.FindById(5));

                p.CreatePedido();

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
