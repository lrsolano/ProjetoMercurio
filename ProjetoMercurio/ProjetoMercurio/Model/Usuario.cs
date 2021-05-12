using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurio.Model
{
    public class Usuario
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public int Idade { get; private set; }
        public Usuario(int id, string nome, DateTime dataCriacao, int idade)
        {
            Id = id;
            Nome = nome;
            DataCriacao = dataCriacao;
            Idade = idade;
        }
        public Usuario(string nome, int idade)
        {
            Nome = nome;
            Idade = idade;
        }
    }
}
