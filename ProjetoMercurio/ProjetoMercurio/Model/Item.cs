using MercurioCore.Model.Exceptions;
using ProjetoMercurio.db.DataManipulation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurio.Model
{
    public class Item
    {
        public int IdPedidoxItem { get; private set; }
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public int Quantidade { get; private set; }

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

        public void CreateItem()
        {
            if (Id != 0)
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
            DataCriacao = i.DataCriacao;
        }
        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", this.Id, this.Nome, this.DataCriacao);
        }
    }
}
