using System;
using Mercurio.Core;

namespace MecurioCoreTest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Pedido pedido = new Pedido(1);
                Item marreta = Item.FindByName("Marreta");
                pedido.RemoveItem(marreta);
                //pedido.ChangeItem(prego, 50);
                pedido.UpdatePedido();

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
