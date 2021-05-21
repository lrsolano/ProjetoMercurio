using System;
using ProjetoMercurioCore;
using ProjetoMercurioCore.Model;
using ProjetoMercurioCore.db.DataManipulation;
using ProjetoMercurioCore.Model.Enum;
using MercurioCore.Model.Exceptions;
using System.Collections.Generic;

namespace TestCore
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Pedido pedido = new Pedido(1);

                Item martelo = new Item(2);
                Item prego = new Item("Prego");
                pedido.ChangeItem(martelo, 1);
                pedido.AddItem(prego, 50);

                pedido.UpdatePedido();



            }
            catch (DBConnectionException ex)
            {
                Console.WriteLine(ex);
            }
            catch(MercurioCoreException ex)
            {
                Console.WriteLine(ex);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
    }
}
