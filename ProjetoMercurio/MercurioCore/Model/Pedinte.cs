using MercurioCore.db.DataManipulation;
using MercurioCore.Model.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace MercurioCore.Model
{
    public class Pedinte
    {
        public int Id { get; private set; }
        public int Idade { get; private set; }
        public string Nome { get; set; }
        public DateTime DataEntrada { get; private set; }

        public Pedinte(int idade, string nome)
        {
            Idade = idade;
            Nome = nome;
        }
        public Pedinte(int id)
        {
            PedinteManipulation<Pedinte> item = new PedinteManipulation<Pedinte>();
            Pedinte i = item.FindByID(id);
            Id = id;
            Nome = i.Nome;
            Idade = i.Idade;
            DataEntrada = i.DataEntrada;
        }

        public Pedinte(int id, int idade, string nome, DateTime dataEntrada)
        {
            Id = id;
            Idade = idade;
            Nome = nome;
            DataEntrada = dataEntrada;
        }

        public void CreatePedinte()
        {
            if (Id != 0)
            {
                throw new MercurioCoreException("Objeto já criado no Banco de Dados");
            }
            PedinteManipulation<Pedinte> item = new PedinteManipulation<Pedinte>();

            Pedinte novo = item.Create(this);

            Id = novo.Id;
        }
        public void UpdatePedinte()
        {
            PedinteManipulation<Pedinte> item = new PedinteManipulation<Pedinte>();

            item.Update(this);

        }
        public void DeletePedinte()
        {
            PedinteManipulation<Pedinte> item = new PedinteManipulation<Pedinte>();

            item.Delete(this.Id);
        }

        public void ChangePedinte(int id)
        {
            PedinteManipulation<Pedinte> item = new PedinteManipulation<Pedinte>();
            Pedinte i = item.FindByID(id);
            Id = id;
            Nome = i.Nome;
            Idade = i.Idade;
            DataEntrada = i.DataEntrada;
        }
        public override string ToString()
        {
            return string.Format("{0} - {1} - {2} - {3}", this.Id, this.Nome, this.Idade, this.DataEntrada);
        }
    }
}
