using ProjetoMercurio.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurio.Model
{
    public class Sensor
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public Boolean Inicial { get; private set; }
        public Sensor SensorAnterior { get; set; }
        public Direcao Direcao { get; set; }
        public DirecaoRota DirecaoRota { get; private set; }

        public Sensor(int id, string nome, DateTime dataCriacao, bool inicial, Sensor sensorAnterior, Direcao direcao, DirecaoRota direcaoRota)
        {
            Id = id;
            Nome = nome;
            DataCriacao = dataCriacao;
            Inicial = inicial;
            SensorAnterior = sensorAnterior;
            Direcao = direcao;
            DirecaoRota = direcaoRota;
        }

        public Sensor(string nome, bool inicial, Sensor sensorAnterior, Direcao direcao, DirecaoRota direcaoRota)
        {
            Nome = nome;
            Inicial = inicial;
            SensorAnterior = sensorAnterior;
            Direcao = direcao;
            DirecaoRota = direcaoRota;
        }
    }
}
