using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    public class Pedido : CommonColumns, IColumnAtivo
    {
        public Usuario Usuario { get; private set; }
        public Rota Rota { get; private set; }
        public List<Item> Items { get; set; }
        public bool Ativo { get; set; }
        internal Pedido(int id, Usuario usuario, DateTime dataCriacao, Rota rota, List<Item> items) : base("pedido", "IdPedido")
        {
            Id = id;
            Usuario = usuario;
            DataCriacao = dataCriacao;
            Rota = rota;
            Items = items;
        }
        internal Pedido(Usuario usuario, DateTime dataCriacao, Rota rota, List<Item> items) : base("pedido", "IdPedido")
        {
            Usuario = usuario;
            DataCriacao = dataCriacao;
            Rota = rota;
            Items = items;
        }
        public Pedido(Usuario usuario) : base("pedido", "IdPedido")
        {
            Usuario = usuario;
            Items = new List<Item>();
        }
        internal Pedido(int id) : base("pedido", "IdPedido")
        {
            PedidoManipulation item = new PedidoManipulation();
            Pedido i = item.FindByID(id);
            Id = id;
            Usuario = i.Usuario;
            Items = i.Items;
            DataCriacao = i.DataCriacao;
            Rota = i.Rota;

        }
        public static List<Pedido> FindAll()
        {
            PedidoManipulation item = new PedidoManipulation();
            List<Pedido> i = item.FindAll();
            return i;
        }
        public static Pedido FindById(long id)
        {
            PedidoManipulation item = new PedidoManipulation();
            Pedido i = item.FindByID(id);
            return i;
        }
        public void CreatePedido()
        {
            if (Id != 0)
            {
                throw new MercurioCoreException("Objeto já criado no Banco de Dados");
            }

            if (Rota == null || Items.Count == 0)
            {
                throw new MercurioCoreException("Falta dados para a criação");
            }

            if (Rota.Id == 0)
            {
                Rota.CreateRota();
            }
            foreach (Item i in Items)
            {
                if (i.Id == 0)
                {
                    i.CreateItem();
                }
            }
            if (Usuario.Id == 0)
            {
                Usuario.CreateUsuario();
            }


            PedidoManipulation item = new PedidoManipulation();

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
            PedidoManipulation item = new PedidoManipulation();

            item.Update(this);

        }
        public void DeletePedido()
        {
            PedidoManipulation item = new PedidoManipulation();

            item.Delete(this.Id);
        }
        public void AddItem(Item item, int quantidade)
        {
            List<Item> itens = Items.FindAll(i => i.Equals(item));
            if (itens.Count == 0 || itens == null)
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
        public void RemoveItem(Item item)
        {
            Item i = Items.Find(it => it.Id == item.Id);
            i.RemoveItem = true;
        }
        public void ChangeItem(Item item, int quantidade)
        {
            Item i = Items.Find(it => it.Id == item.Id);
            i.ChangeItem(Convert.ToInt32(item.Id));
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
