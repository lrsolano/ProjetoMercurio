using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurio.Model
{
    public class Pedido
    {
        public int Id { get; private set; }
        public Usuario Usuario {get; private set;}
        public DateTime DataCriacao { get; private set; }
        public string Rota { get; private set; }
        public List<Item> Items { get; set; }
    }
}
