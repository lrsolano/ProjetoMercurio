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
        public string Hash { get; private set; }
        public bool Ativo { get; set; }
        private bool AtualizarTodas { get; set; }
        internal Sensor(long id, string nome, DateTime dataCriacao, bool inicial, int sensorAnterior, Direcao direcao, DirecaoRota direcaoRota, string hash) : base("sensor", "IdSensor")
        {
            Id = id;
            Nome = nome;
            DataCriacao = dataCriacao;
            Inicial = inicial;
            SensorAnterior = sensorAnterior;
            Direcao = direcao;
            DirecaoRota = direcaoRota;
            Hash = hash;
        }
        public Sensor(string nome, bool inicial, int sensorAnterior, Direcao direcao, DirecaoRota direcaoRota, string hash) : base("sensor", "IdSensor")
        {
            Nome = nome;
            Inicial = inicial;
            SensorAnterior = sensorAnterior;
            Direcao = direcao;
            DirecaoRota = direcaoRota;
            Hash = hash;
        }
        internal Sensor(int id) : base("sensor", "IdSensor")
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
        public static Sensor FindByHash(string hash)
        {
            SensorManipulation item = new SensorManipulation();
            Sensor i = item.FindByHash(hash);
            return i;
        }
        public static List<Sensor> FindAll()
        {
            SensorManipulation item = new SensorManipulation();
            List<Sensor> i = item.FindAll();
            return i;
        }
        public static Sensor FindById(long id)
        {
            SensorManipulation item = new SensorManipulation();
            Sensor i = item.FindByID(id);
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
                throw new MercurioCoreException("Sensor já criado no Banco de Dados");
            }

            SensorManipulation item = new SensorManipulation();
            if (item.FindByName(Nome) != null)
            {
                throw new MercurioCoreException("Sensor já criado no Banco de Dados");
            }
            if(item.FindByID(SensorAnterior) == null)
            {
                throw new MercurioCoreException("Sensor anterior não encontrado no Banco de Dados");
            }
            Sensor novo = item.Create(this);
            DataCriacao = novo.DataCriacao;
            Id = novo.Id;
        }
        public void UpdateSensor()
        {
            SensorManipulation item = new SensorManipulation();
            
            item.Update(this);
            if (AtualizarTodas)
            {
                UpdateTodasRotas();
            }

        }
        public void DeleteSensor()
        {
            SensorManipulation item = new SensorManipulation();
            if (item.CanDelete(Id))
            {
                item.Delete(Id);
            }
            else
            {
                throw new MercurioCoreException("Sensor em uso.");
            }

        }
        public void ChangeName(string nome)
        {
            SensorManipulation item = new SensorManipulation();
            if (item.FindByName(nome) != null)
            {
                throw new MercurioCoreException("Sensor já criado no Banco de Dados");
            }
            Nome = nome;
        }
        public void ChangeSensorAnterior(int sensorAnterior)
        {
            SensorManipulation item = new SensorManipulation();
            if (item.FindByID(sensorAnterior) == null)
            {
                throw new MercurioCoreException("Sensor anterior não encontrado no Banco de Dados");
            }
            SensorAnterior = sensorAnterior;
            AtualizarTodas = true;
        }
        public void ChangeDirecao(Direcao direcao)
        {
            Direcao = direcao;
            AtualizarTodas = true;
        }
        public void UpdateTodasRotas()
        {
            var rotas = Rota.FindAll();
            string tracado = string.Empty;
            foreach(Rota r in rotas)
            {
                tracado = r.Tracado;
                if(tracado != r.GerarRota())
                {
                    r.UpdateItem();
                }
            }

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
