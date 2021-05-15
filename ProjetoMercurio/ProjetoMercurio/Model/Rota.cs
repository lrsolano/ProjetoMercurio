using MercurioCore.Model.Exceptions;
using ProjetoMercurioCore.db.DataManipulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMercurioCore.Model
{
    public class Rota
    {
        public int Id { get; private set; }
        public Sensor SensorInicial { get; private set; }
        public Sensor SensorFinal { get; set; }
        public string Tracado { get; set; }

        public Rota(int id, Sensor sensorInicial, Sensor sensorFinal, string tracado)
        {
            Id = id;
            SensorInicial = sensorInicial;
            SensorFinal = sensorFinal;
            Tracado = tracado;
        }

        public Rota(Sensor sensorInicial, Sensor sensorFinal)
        {
            SensorInicial = sensorInicial;
            SensorFinal = sensorFinal;
            GerarRota();
        }

        public Rota(int id)
        {
            RotaManipulation<Rota> item = new RotaManipulation<Rota>();
            Rota i = item.FindByID(id);
            Id = id;
            SensorFinal = i.SensorFinal;
            SensorInicial = i.SensorInicial;
            Tracado = i.Tracado;
        }

        public void CreateItem()
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
                RotaManipulation<Rota> item = new RotaManipulation<Rota>();

                Rota novo = item.Create(this);

                Id = novo.Id;
                Console.WriteLine("Novo Id");
            }
            
        }
        public void UpdateItem()
        {
            RotaManipulation<Rota> item = new RotaManipulation<Rota>();

            item.Update(this);

        }
        public void DeleteItem()
        {
            RotaManipulation<Rota> item = new RotaManipulation<Rota>();

            item.Delete(this.Id);
        }
        public void ChangeItem(int id)
        {
            RotaManipulation<Rota> item = new RotaManipulation<Rota>();
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

            for(int i = 1; i < lista.Length; i++)
            {
                rotas += lista[i] + ";";
            }

            Tracado = rotas;
            return rotas;


        }

        public static List<Rota> FindAll()
        {
            RotaManipulation<Rota> item = new RotaManipulation<Rota>();
            return item.FindAll();
        }

        private int Exist()
        {
            RotaManipulation<Rota> item = new RotaManipulation<Rota>();
            return item.RotaExist(this);
        }
    }
}
