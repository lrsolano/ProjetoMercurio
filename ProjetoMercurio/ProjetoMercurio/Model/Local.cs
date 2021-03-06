using MercurioCore.Model.Exceptions;
using ProjetoMercurioCore.db.DataManipulation;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurioCore.Model
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
        public Local(int id)
        {
            LocalManipulation<Local> item = new LocalManipulation<Local>();
            Local i = item.FindByID(id);
            Id = id;
            Nome = i.Nome;
            DataCriacao = i.DataCriacao;
            Sensor = i.Sensor;
        }
        public static Local FindByName(string nome)
        {
            LocalManipulation<Local> item = new LocalManipulation<Local>();
            Local i = item.FindByName(nome);
            return i;
        }
        public static List<Local> FindAll()
        {
            LocalManipulation<Local> item = new LocalManipulation<Local>();
            List<Local> i = item.FindAll();
            return i;
        }
        public void CreateLocal()
        {
            if (Id != 0)
            {
                throw new MercurioCoreException("Objeto já criado no Banco de Dados");
            }
            LocalManipulation<Local> item = new LocalManipulation<Local>();

            Local novo = item.Create(this);

            Id = novo.Id;
        }
        public void UpdateLocal()
        {
            LocalManipulation<Local> item = new LocalManipulation<Local>();

            item.Update(this);

        }
        public void DeleteLocal()
        {
            LocalManipulation<Local> item = new LocalManipulation<Local>();

            item.Delete(this.Id);
        }
        public void ChangeLocal(int id)
        {
            LocalManipulation<Local> item = new LocalManipulation<Local>();
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
