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
    }
}
