using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    public class Rota : CommonColumns
    {
        public Sensor SensorInicial { get; private set; }
        public Sensor SensorFinal { get; set; }
        public string Tracado { get; set; }
        internal Rota(int id, Sensor sensorInicial, Sensor sensorFinal, string tracado) : base("rota", "IdRota")
        {
            Id = id;
            SensorInicial = sensorInicial;
            SensorFinal = sensorFinal;
            Tracado = tracado;
        }
        public Rota(Sensor sensorInicial, Sensor sensorFinal) : base("rota", "IdRota")
        {
            SensorInicial = sensorInicial;
            SensorFinal = sensorFinal;
            GerarRota();
        }
        internal Rota(int id) : base("rota", "IdRota")
        {
            if (base.Exists(id))
            {
                RotaManipulation item = new RotaManipulation();
                Rota i = item.FindByID(id);
                Id = id;
                SensorFinal = i.SensorFinal;
                SensorInicial = i.SensorInicial;
                Tracado = i.Tracado;
            }
            
        }
        public static List<Rota> FindAll()
        {
            RotaManipulation item = new RotaManipulation();
            return item.FindAll();
        }
        public static Rota FindById(long id)
        {
            RotaManipulation item = new RotaManipulation();
            return item.FindByID(id);
        }
        public void CreateRota()
        {
            if (Id != 0)
            {
                throw new MercurioCoreException("Objeto já criado no Banco de Dados");
            }
            int idRetornado = Exist();
            if (idRetornado != 0)
            {
                Id = idRetornado;
                Console.WriteLine("Já existe");
            }
            else
            {
                RotaManipulation item = new RotaManipulation();

                Rota novo = item.Create(this);

                Id = novo.Id;
                Console.WriteLine("Novo Id");
            }

        }
        public void UpdateItem()
        {
            RotaManipulation item = new RotaManipulation();

            item.Update(this);

        }
        public void DeleteItem()
        {
            RotaManipulation item = new RotaManipulation();
            if (item.CanDelete(Id))
            {
                item.Delete(this.Id);
            }
            else
            {
                throw new MercurioCoreException("Rota em uso.");
            }
            
        }
        public void ChangeItem(int id)
        {
            RotaManipulation item = new RotaManipulation();
            Rota i = item.FindByID(id);
            Id = id;
            SensorFinal = i.SensorFinal;
            SensorInicial = i.SensorInicial;
            Tracado = i.Tracado;
        }
        public string GerarRota()
        {
            List<Sensor> sensores = Sensor.FindAll();
            string rota = string.Empty;
            string direcao = "Final";
            string direcaoAnterior = direcao;

            Sensor atual = SensorFinal;
            Sensor anterior;

            while (!atual.Equals(SensorInicial))
            {
                if (direcaoAnterior != "Frente")
                {
                    rota += string.Format("{0}-{1};", atual.Id, direcaoAnterior);
                }

                anterior = atual;
                atual = sensores.Find(item => atual.SensorAnterior.Equals(item));
                direcaoAnterior = anterior.Direcao.Movimento;
            }

            rota += string.Format("{0}-{1};", atual.Id, direcaoAnterior);


            string[] lista = rota.Split(';');

            Array.Reverse(lista);

            string rotas = string.Empty;

            for (int i = 1; i < lista.Length; i++)
            {
                rotas += lista[i] + ";";
            }

            Tracado = rotas;
            return rotas;


        }
        private int Exist()
        {
            RotaManipulation item = new RotaManipulation();
            return item.RotaExist(this);
        }
        public override bool Equals(object obj)
        {
            var item = obj as Rota;

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
