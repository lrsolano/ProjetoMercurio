using System;
using System.Collections.Generic;
using MercurioCore;
using Serilog;
using MySqlConnector;
using MercurioCore.Model;
using MercurioCore.db.DataManipulation;
using MercurioCore.Model.Exceptions;

namespace ApplicationTest
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {

                /*Pedinte matheus = new Pedinte(26, "Matheus Boldrini");
                matheus.CreatePedinte();

                Item teclado = new Item("Teclado", "Eletronico", 1);
                teclado.CreateItem();
                Item mouse = new Item("Mouse", "Eletronico", 1);
                mouse.CreateItem();

                List<Item> items = new List<Item>();

                items.Add(teclado);
                items.Add(mouse);


                Pedido pedido = new Pedido(matheus, items);

                pedido.CreatePedido();*/


                Pedido pedido = new Pedido(5);
                pedido.DeletePedido();
                




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
