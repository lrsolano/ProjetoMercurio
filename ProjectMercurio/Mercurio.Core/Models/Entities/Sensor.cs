using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    public class Sensor : CommonColumns, IColumnAtivo
    {
        public string Nome { get; private set; }
        public Boolean Inicial { get; private set; }
        public int SensorAnterior { get; set; }
        public Direcao Direcao { get; set; }
        public DirecaoRota DirecaoRota { get; private set; }
        public bool Ativo { get; set; }
        public Sensor(int id, string nome, DateTime dataCriacao, bool inicial, int sensorAnterior, Direcao direcao, DirecaoRota direcaoRota) : base("sensor", "IdSensor")
        {
            Id = id;
            Nome = nome;
            DataCriacao = dataCriacao;
            Inicial = inicial;
            SensorAnterior = sensorAnterior;
            Direcao = direcao;
            DirecaoRota = direcaoRota;
        }
        public Sensor(string nome, bool inicial, int sensorAnterior, Direcao direcao, DirecaoRota direcaoRota) : base("sensor", "IdSensor")
        {
            Nome = nome;
            Inicial = inicial;
            SensorAnterior = sensorAnterior;
            Direcao = direcao;
            DirecaoRota = direcaoRota;
        }
        public Sensor(int id) : base("sensor", "IdSensor")
        {
            if (base.Exists(id))
            {
                SensorManipulation manipulation = new SensorManipulation();
                Sensor i = manipulation.FindByID(id);
                Id = i.Id;
                Nome = i.Nome;
                DataCriacao = i.DataCriacao;
                Inicial = i.Inicial;
                SensorAnterior = i.SensorAnterior;
                Direcao = i.Direcao;
                DirecaoRota = i.DirecaoRota;
            }  
        }
        public static Sensor FindByName(string nome)
        {
            SensorManipulation item = new SensorManipulation();
            Sensor i = item.FindByName(nome);
            return i;
        }
        public static List<Sensor> FindAll()
        {
            SensorManipulation item = new SensorManipulation();
            List<Sensor> i = item.FindAll();
            return i;
        }
        public void ChangeType(int id)
        {
            SensorManipulation manipulation = new SensorManipulation();
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
            SensorManipulation item = new SensorManipulation();

            Sensor novo = item.Create(this);

            Id = novo.Id;
        }
        public void UpdateSensor()
        {
            SensorManipulation item = new SensorManipulation();

            item.Update(this);

        }
        public void DeleteSensor()
        {
            SensorManipulation item = new SensorManipulation();

            item.Delete(this.Id);
        }
        public override bool Equals(object obj)
        {
            var item = obj as Sensor;

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
