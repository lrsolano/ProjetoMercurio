using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurio.Model
{
    public class Local
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public Sensor Sensor { get; private set; }
        public Local(int id, string nome, DateTime dataCriacao, Sensor sensor)
        {
            Id = id;
            Nome = nome;
            DataCriacao = dataCriacao;
            Sensor = sensor;
        }
        public Local(string nome, Sensor sensor)
        {
            Nome = nome;
            Sensor = sensor;
        }
    }
}
