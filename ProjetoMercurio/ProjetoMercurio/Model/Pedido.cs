using MercurioCore.Model.Exceptions;
using ProjetoMercurioCore.db.DataManipulation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurioCore.Model
{
    public class Pedido
    {
        public int Id { get; private set; }
        public Usuario Usuario { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public Rota Rota { get; private set; }
        public List<Item> Items { get; set; }
        internal Pedido(int id, Usuario usuario, DateTime dataCriacao, Rota rota, List<Item> items)
        {
            Id = id;
            Usuario = usuario;
            DataCriacao = dataCriacao;
            Rota = rota;
            Items = items;
        }
        internal Pedido(Usuario usuario, DateTime dataCriacao, Rota rota, List<Item> items)
        {
            Usuario = usuario;
            DataCriacao = dataCriacao;
            Rota = rota;
            Items = items;
        }
        public Pedido(Usuario usuario)
        {
            Usuario = usuario;
            Items = new List<Item>();
        }
        public Pedido(int id)
        {
            PedidoManipulation<Pedido> item = new PedidoManipulation<Pedido>();
            Pedido i = item.FindByID(id);
            Id = id;
            Usuario = i.Usuario;
            Items = i.Items;
            DataCriacao = i.DataCriacao;
            Rota = i.Rota;

        }
        public static List<Pedido> FindAll()
        {
            PedidoManipulation<Pedido> item = new PedidoManipulation<Pedido>();
            List<Pedido> i = item.FindAll();
            return i;
        }
        public void CreatePedido()
        {
            if (Id != 0)
            {
                throw new MercurioCoreException("Objeto já criado no Banco de Dados");
            }

            if(Rota == null || Items.Count == 0)
            {
                throw new MercurioCoreException("Falta dados para a criação");
            }

            if(Rota.Id == 0)
            {
                Rota.CreateRota();
            }
            foreach(Item i in Items)
            {
                if(i.Id == 0)
                {
                    i.CreateItem();
                }
            }
            if(Usuario.Id == 0)
            {
                Usuario.CreateUsuario();
            }


            PedidoManipulation<Pedido> item = new PedidoManipulation<Pedido>();

            Pedido novo = item.Create(this);

            Id = novo.Id;
        }
        public void UpdatePedido()
        {
            foreach (Item i in Items)
            {
                if (i.Id == 0)
                {
                    i.CreateItem();
                }
            }
            PedidoManipulation<Pedido> item = new PedidoManipulation<Pedido>();

            item.Update(this);

        }
        public void DeletePedido()
        {
            PedidoManipulation<Pedido> item = new PedidoManipulation<Pedido>();

            item.Delete(this.Id);
        }
        public void AddItem(Item item, int quantidade)
        {
            List<Item> itens = Items.FindAll(i => i.Equals(item));
            if(itens.Count == 0 || itens == null)
            {
                item.Quantidade = quantidade;
                Items.Add(item);
            }
            
        }
        public void SetRota(Sensor inicial, Sensor final)
        {
            Rota rota = new Rota(inicial, final);
            Rota = rota;
        }
        public void SetRota(Rota rota)
        {
            Rota = rota;
        }
        public void ChangeItem(Item item, int quantidade)
        {
            Item i = Items.Find(it => it.Id == item.Id);
            i.ChangeItem(item.Id);
            i.Quantidade = quantidade;
        }
        public override bool Equals(object obj)
        {
            var item = obj as Pedido;

            if (item == null)
            {
                return false;
            }

            return this.Id.Equals(item.Id);
        }
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }



    }
}
