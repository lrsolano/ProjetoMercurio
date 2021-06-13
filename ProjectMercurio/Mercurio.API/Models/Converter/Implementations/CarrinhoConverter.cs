using Mercurio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    public class CarrinhoConverter : IParser<CarrinhoV, Carrinho>, IParser<Carrinho, CarrinhoV>
    {
        public CarrinhoV Parser(Carrinho origin)
        {
            PedidoConverter pedidoConverter = new PedidoConverter();
            SensorConverter sensorConverter = new SensorConverter();
            if (origin == null) return null;
            long idPedido = 0;
            long idUltimoSensor = 0;
            if(origin.Pedido != null)
            {
                idPedido = origin.Pedido.Id;
            }
            if(origin.UltimoSensor != null)
            {
                idUltimoSensor = origin.UltimoSensor.Id;
            }
            CarrinhoV item = new CarrinhoV
            {
                Id = origin.Id,
                Nome = origin.Nome,
                IdUltimoSensor = idUltimoSensor,
                Disponivel = origin.Disponivel,
                IdPedido = idPedido,
                HashRota = origin.HashRota
                
            };
            return item;
        }
        public Carrinho Parser(CarrinhoV origin)
        {
            if (origin == null) return null;
            Carrinho item = null;
            if (origin.Id != 0)
            {
                item = Carrinho.FindById(origin.Id);
            }
            else
            {
                item = new Carrinho();
                if(origin.Nome != "" || string.IsNullOrWhiteSpace(origin.Nome))
                {
                    item.ChangeName(origin.Nome);
                }
                if(origin.IdPedido != 0)
                {
                    item.ChangePedido(Pedido.FindById(origin.IdPedido));
                }
                if (origin.IdUltimoSensor != 0)
                {
                    item.ChangeUltimoSensor(Sensor.FindById(origin.IdUltimoSensor));
                }

            }
            return item;
        }

        public List<CarrinhoV> Parser(List<Carrinho> origin)
        {
            if (origin.Count == 0) return null;
            List<CarrinhoV> items = new List<CarrinhoV>();
            foreach (Carrinho u in origin)
            {
                items.Add(Parser(u));
            }
            return items;
        }

        public List<Carrinho> Parser(List<CarrinhoV> origin)
        {
            if (origin.Count == 0) return null;
            List<Carrinho> items = new List<Carrinho>();
            foreach (CarrinhoV u in origin)
            {
                items.Add(Parser(u));
            }
            return items;
        }
    }
}
