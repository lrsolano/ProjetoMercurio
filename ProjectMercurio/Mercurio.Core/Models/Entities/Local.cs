using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    public class Local : CommonColumns, IColumnAtivo
    {
        public bool Ativo { get; set; }
        public string Nome { get; private set; }
        public Sensor Sensor { get; private set; }
        internal Local(int id, string nome, DateTime dataCriacao, Sensor sensor) : base("local", "IdLocal")
        {
            Id = id;
            Nome = nome;
            DataCriacao = dataCriacao;
            Sensor = sensor;
        }
        public Local(string nome, Sensor sensor) : base("local", "IdLocal")
        {
            Nome = nome;
            Sensor = sensor;
        }
        internal Local(int id) : base("local", "IdLocal")
        {
            if (base.Exists(id))
            {
                LocalManipulation item = new LocalManipulation();
                Local i = item.FindByID(id);
                Id = id;
                Nome = i.Nome;
                DataCriacao = i.DataCriacao;
                Sensor = i.Sensor;
            }
            
        }
        public static Local FindByName(string nome)
        {
            LocalManipulation item = new LocalManipulation();
            Local i = item.FindByName(nome);
            return i;
        }
        public static List<Local> FindAll()
        {
            LocalManipulation item = new LocalManipulation();
            List<Local> i = item.FindAll();
            return i;
        }
        public static Local FindById(long id)
        {
            LocalManipulation item = new LocalManipulation();
            Local i = item.FindByID(id);
            return i;
        }
        public void CreateLocal()
        {
            if (Id != 0)
            {
                throw new MercurioCoreException("Local já criado no Banco de Dados");
            }
            
            LocalManipulation item = new LocalManipulation();
            if (item.FindByName(Nome) != null)
            {
                throw new MercurioCoreException("Local já criado no Banco de Dados");
            }
            Local novo = item.Create(this);

            Id = novo.Id;
        }
        public void UpdateLocal()
        {
            LocalManipulation item = new LocalManipulation();

            item.Update(this);

        }
        public void DeleteLocal()
        {
            LocalManipulation item = new LocalManipulation();

            item.Delete(this.Id);
        }
        public void ChangeLocal(int id)
        {
            LocalManipulation item = new LocalManipulation();
            Local i = item.FindByID(id);
            Id = id;
            Nome = i.Nome;
            DataCriacao = i.DataCriacao;
            Sensor = i.Sensor;

        }
        public override bool Equals(object obj)
        {
            var item = obj as Local;

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
