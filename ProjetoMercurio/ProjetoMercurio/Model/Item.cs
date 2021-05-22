using MercurioCore.Model.Exceptions;
using ProjetoMercurioCore.db.DataManipulation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurioCore.Model
{
    public class Item
    {
        internal int IdPedidoxItem { get; set; }
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataCriacao { get; private set; }
        internal int Quantidade { get; set; }

        public Item(int id, string nome, DateTime dataCriacao)
        {
            Id = id;
            Nome = nome;
            DataCriacao = dataCriacao;
        }
        public Item(string nome)
        {
            Nome = nome;
        }
        public Item(int id)
        {
            ItemManipulation<Item> item = new ItemManipulation<Item>();
            Item i = item.FindByID(id);
            Id = id;
            Nome = i.Nome;
            DataCriacao = i.DataCriacao;
        }
        public static Item FindByName(string nome)
        {
            ItemManipulation<Item> item = new ItemManipulation<Item>();
            Item i = item.FindByName(nome);
            return i;
        }
        public static List<Item> FindAll()
        {
            ItemManipulation<Item> item = new ItemManipulation<Item>();
            List<Item> i = item.FindAll();
            return i;
        }
        public void CreateItem()
        {
            if (Id != 0)
            {
                throw new MercurioCoreException("Item já criado no Banco de Dados");
            }
            ItemManipulation<Item> item = new ItemManipulation<Item>();
            if (item.FindByName(Nome) == null)
            {
                throw new MercurioCoreException("Item já criado no Banco de Dados");
            }
            Item novo = item.Create(this);

            Id = novo.Id;
        }
        public void UpdateItem()
        {
            ItemManipulation<Item> item = new ItemManipulation<Item>();

            item.Update(this);

        }
        public void DeleteItem()
        {
            ItemManipulation<Item> item = new ItemManipulation<Item>();

            item.Delete(this.Id);
        }
        public void ChangeItem(int id)
        {
            ItemManipulation<Item> item = new ItemManipulation<Item>();
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
