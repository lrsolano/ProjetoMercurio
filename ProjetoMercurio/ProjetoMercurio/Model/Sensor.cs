using MercurioCore.Model.Exceptions;
using ProjetoMercurioCore.db.DataManipulation;
using ProjetoMercurioCore.Model.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurioCore.Model
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

        public Sensor(int id)
        {
            SensorManipulation<Sensor> manipulation = new SensorManipulation<Sensor>();
            Sensor i = manipulation.FindByID(id);
            Id = i.Id;
            Nome = i.Nome;
            DataCriacao = i.DataCriacao;
            Inicial = i.Inicial;
            SensorAnterior = i.SensorAnterior;
            Direcao = i.Direcao;
            DirecaoRota = i.DirecaoRota;
        }

        public void ChangeType(int id)
        {
            SensorManipulation<Sensor> manipulation = new SensorManipulation<Sensor>();
            Sensor i = manipulation.FindByID(id);
            Id = i.Id;
            Nome = i.Nome;
            DataCriacao = i.DataCriacao;
            Inicial = i.Inicial;
            SensorAnterior = i.SensorAnterior;
            Direcao = i.Direcao;
            DirecaoRota = i.DirecaoRota;
        }
        public void CreateSensor()
        {
            if (Id != 0)
            {
                throw new MercurioCoreException("Objeto já criado no Banco de Dados");
            }
            SensorManipulation<Sensor> item = new SensorManipulation<Sensor>();

            Sensor novo = item.Create(this);

            Id = novo.Id;
        }
        public void UpdateSensor()
        {
            SensorManipulation<Sensor> item = new SensorManipulation<Sensor>();

            item.Update(this);

        }
        public void DeleteSensor()
        {
            SensorManipulation<Sensor> item = new SensorManipulation<Sensor>();

            item.Delete(this.Id);
        }
    }
}
