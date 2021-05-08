using MercurioCore.db.DataManipulation;
using MercurioCore.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MercurioCore.Model
{
    public class Pedido
    {
        public int Id { get; private set; }
        public virtual Pedinte pedinte { get; private set; }
        public virtual List<Item> Itens { get; private set; }
        public DateTime DataPedido { get; private set; }

        public Pedido(int id, Pedinte pedinte, List<Item> itens, DateTime dataPedido)
        {
            Id = id;
            this.pedinte = pedinte;
            Itens = itens;
            DataPedido = dataPedido;
        }

        public Pedido(Pedinte pedinte, List<Item> itens)
        {
            this.pedinte = pedinte;
            Itens = itens;
        }

        public Pedido(int id)
        {
            PedidoManipulation<Pedido> item = new PedidoManipulation<Pedido>();
            Pedido i = item.FindByID(id);
            Id = id;
            pedinte = i.pedinte;
            Itens = i.Itens;
            DataPedido = i.DataPedido;
        }

        public void CreatePedido()
        {
            if (Id != 0)
            {
                throw new MercurioCoreException("Objeto já criado no Banco de Dados");
            }
            PedidoManipulation<Pedido> item = new PedidoManipulation<Pedido>();

            Pedido novo = item.Create(this);

            Id = novo.Id;
        }
        public void UpdatePedido()
        {
            PedidoManipulation<Pedido> item = new PedidoManipulation<Pedido>();

            item.Update(this);

        }
        public void DeletePedido()
        {
            PedidoManipulation<Pedido> item = new PedidoManipulation<Pedido>();

            item.Delete(this.Id);
        }
    }
}
