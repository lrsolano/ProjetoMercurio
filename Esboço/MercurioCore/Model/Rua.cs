using System;
using System.Collections.Generic;
using System.Text;

namespace MercurioCore.Model
{
    public class Rua
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }

        public Rua(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
    }
}
