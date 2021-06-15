using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    public class Carrinho : CommonColumns
    {
        public string Nome { get; private set; }
        public Pedido  Pedido { get; private set; }
        public bool Disponivel { get; private set; }
        public Sensor UltimoSensor { get; private set; }
        public string HashRota { get; private set; }
        internal Carrinho(int id) : base("item", "IdCarrinho")
        {
            Id = id;
        }
        internal Carrinho(int id,string nome, Pedido pedido, bool disponivel, Sensor ultimoSensor, string hashRota, DateTime dataCriacao) : base("item", "IdCarrinho")
        {
            Id = id;
            Nome = nome;
            Pedido = pedido;
            Disponivel = disponivel;
            UltimoSensor = ultimoSensor;
            HashRota = hashRota;
            DataCriacao = dataCriacao;
        }
        public Carrinho() : base("item", "IdCarrinho")
        {
            Disponivel = true;
        }
        public static Carrinho FindByName(string nome)
        {
            CarrinhoManipulation item = new CarrinhoManipulation();
            Carrinho i = item.FindByName(nome);
            return i;
        }
        public static List<Carrinho> FindAll()
        {
            CarrinhoManipulation item = new CarrinhoManipulation();
            List<Carrinho> i = item.FindAll();
            return i;
        }
        public static Carrinho FindById(long id)
        {
            CarrinhoManipulation item = new CarrinhoManipulation();
            Carrinho i = item.FindByID(id);
            return i;
        }
        public void CreateCarrinho()
        {
            if(Nome == null)
            {
                throw new MercurioCoreException("Carrinho sem nome");
            }
            if (Id != 0)
            {
                throw new MercurioCoreException("Carrinho já criado no Banco de Dados");
            }
            CarrinhoManipulation item = new CarrinhoManipulation();
            if (item.FindByName(Nome) != null)
            {
                throw new MercurioCoreException("Carrinho já criado no Banco de Dados");
            }
            Carrinho novo = item.Create(this);

            Id = novo.Id;
        }
        public void UpdateCarrinho()
        {
            CarrinhoManipulation item = new CarrinhoManipulation();
            item.Update(this);

        }
        public void DeleteCarrinho()
        {
            CarrinhoManipulation item = new CarrinhoManipulation();
            if (item.CanDelete(Id))
            {
                item.Delete(this.Id);
            }
            else
            {
                throw new MercurioCoreException("Carrinho em uso.");
            }

        }
        public void ChangeName(string nome)
        {
            CarrinhoManipulation item = new CarrinhoManipulation();
            if (item.FindByName(nome) != null)
            {
                throw new MercurioCoreException("Carrinho já criado no Banco de Dados");
            }
            Nome = nome;
        }
        public void ChangePedido(Pedido pedido)
        {
            if(pedido.Id == 0)
            {
                throw new MercurioCoreException("Pedido não criado no banco de dados");
            }
            Pedido = pedido;
            Disponivel = false;
            GenerateHash();
        }
        public void ChangeUltimoSensor(Sensor sensor)
        {
            if(sensor.Id == 0)
            {
                throw new MercurioCoreException("Sensor não criado no banco de dados");
            }
            if(UltimoSensor != null)
            {
                if (UltimoSensor.Equals(sensor))
                {
                    throw new MercurioCoreException("Sensores iguais");
                }
            }
            
            UltimoSensor = sensor;
        }
        public void FinalizarCorrida()
        {
            CarrinhoManipulation item = new CarrinhoManipulation();
            if(Pedido.Id == 0)
            {
                throw new MercurioCoreException("Carrinho não está com pedido");
            }
            item.FinalizarPedido(this);
            Disponivel = true;
            Pedido.PedidoEntregue = true;
        }
        public void ChangeCarrinho(int id)
        {
            CarrinhoManipulation item = new CarrinhoManipulation();
            Carrinho i = item.FindByID(id);
            Id = id;
            Nome = i.Nome;
            DataCriacao = i.DataCriacao;
        }
        public override string ToString()
        {
            return string.Format("{0} - {1} - {2}", this.Id, this.Nome, this.DataCriacao);
        }
        public override bool Equals(object obj)
        {
            var item = obj as Carrinho;

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
        internal void GenerateHash()
        {
            HashRota = "";
            Rota rota = Pedido.Rota;
            string[] tracado = rota.Tracado.Split(';');
            foreach (string s in tracado)
            {
                if (!string.IsNullOrWhiteSpace(s))
                {
                    Sensor sensor = Sensor.FindById(Convert.ToInt32(s.Split('-')[0]));
                    HashRota += sensor.Hash+";";
                }
                
            }
        }
    }
}
