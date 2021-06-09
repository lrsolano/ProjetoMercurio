using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    public class Item : CommonColumns, IColumnAtivo
    {
        internal int IdPedidoxItem { get; set; }
        public string Nome { get; private set; }
        public int Quantidade { get; internal set; }
        public bool Ativo { get; set ; }
        internal bool RemoveItem { get; set; }
        internal Item(int id, string nome, DateTime dataCriacao) : base("item", "IdItem")
        {
            Id = id;
            Nome = nome;
            DataCriacao = dataCriacao;
            RemoveItem = false;
        }
        public Item(string nome) : base("item", "IdItem")
        {
            Nome = nome;
            RemoveItem = false;
        }
        internal Item(int id) : base("item", "IdItem")
        {
            if (base.Exists(id))
            {
                ItemManipulation item = new ItemManipulation();
                Item i = item.FindByID(id);
                Id = id;
                Nome = i.Nome;
                DataCriacao = i.DataCriacao;
                RemoveItem = false;
            }
                
        }
        public static Item FindByName(string nome)
        {
            ItemManipulation item = new ItemManipulation();
            Item i = item.FindByName(nome);
            return i;
        }
        public static List<Item> FindAll()
        {
            ItemManipulation item = new ItemManipulation();
            List<Item> i = item.FindAll();
            return i;
        }
        public static Item FindById(long id)
        {
            ItemManipulation item = new ItemManipulation();
            Item i = item.FindByID(id);
            return i;
        }
        public void CreateItem()
        {
            if (Id != 0)
            {
                throw new MercurioCoreException("Item já criado no Banco de Dados");
            }
            ItemManipulation item = new ItemManipulation();
            if (item.FindByName(Nome) != null)
            {
                throw new MercurioCoreException("Item já criado no Banco de Dados");
            }
            Item novo = item.Create(this);

            Id = novo.Id;
        }
        public void UpdateItem()
        {
            ItemManipulation item = new ItemManipulation();

            item.Update(this);

        }
        public void DeleteItem()
        {
            ItemManipulation item = new ItemManipulation();
            if (item.CanDelete(Id))
            {
                item.Delete(this.Id);
            }
            else
            {
                throw new MercurioCoreException("Item em uso.");
            }

        }
        public void ChangeName(string nome)
        {
            ItemManipulation item = new ItemManipulation();
            if (item.FindByName(nome) != null)
            {
                throw new MercurioCoreException("Item já criado no Banco de Dados");
            }
            Nome = nome;
        }
        public void AddQuantidade(int quantidade)
        {
            Quantidade = quantidade;
        }
        public void ChangeItem(int id)
        {
            ItemManipulation item = new ItemManipulation();
            Item i = item.FindByID(id);
            Id = id;
            Nome = i.Nome;
            DataCriacao = i.DataCriacao;
        }
        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", this.Id, this.Nome, this.DataCriacao);
        }
        public override bool Equals(object obj)
        {
            var item = obj as Item;

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
