using MercurioCore.db.DataManipulation;
using MercurioCore.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MercurioCore.Model
{
    public class Item
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Tipo { get; private set; }
        public int Quantidade { get; private set; }
        internal int IdPedidoXItem { get; set; }
        public Item(string nome, string tipo, int quantidade)
        {
            Nome = nome;
            Tipo = tipo;
            Quantidade = quantidade;
        }
        public Item(int id, string nome, string tipo, int quantidade)
        {
            Id = id;
            Nome = nome;
            Tipo = tipo;
            Quantidade = quantidade;
        }
        public Item(int id)
        {
            ItemManipulation<Item> item = new ItemManipulation<Item>();
            Item i = item.FindByID(id);
            Id = id;
            Nome = i.Nome;
            Tipo = i.Tipo;
            Quantidade = i.Quantidade;
        }
        public void CreateItem()
        {
            if(Id != 0)
            {
                throw new MercurioCoreException("Objeto já criado no Banco de Dados");
            }
            ItemManipulation<Item> item = new ItemManipulation<Item>();

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
            Tipo = i.Tipo;
            Quantidade = i.Quantidade;
        }
        public override string ToString()
        {
            return string.Format("{0} - {1} - {2} - {3}", this.Id, this.Nome, this.Tipo, this.Quantidade);
        }


    }
}
