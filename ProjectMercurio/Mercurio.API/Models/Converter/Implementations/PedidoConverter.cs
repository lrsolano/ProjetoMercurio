using Mercurio.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mercurio.API
{
    public class PedidoConverter : IParser<PedidoV, Pedido>, IParser<Pedido, PedidoV>
    {
        public PedidoV Parser(Pedido origin)
        {
            ItemConverter itemConverter = new ItemConverter();

            if (origin == null) return null;
            PedidoV pedido = new PedidoV { 
                IdUsuario = origin.Usuario.Id,
                Items = itemConverter.Parser(origin.Items),
                IdSensorInicial = origin.Rota.SensorInicial.Id,
                IdSensorFinal = origin.Rota.SensorFinal.Id,
                NomeUsuario = origin.Usuario.Nome,
                IdRota = origin.Rota.Id,
                Id = origin.Id
            };
            return pedido;
        }

        public Pedido Parser(PedidoV origin)
        {
            ItemConverter itemConverter = new ItemConverter();
            if (origin == null) return null;
            Pedido pedido = null;
            if (origin.Id == 0)
            {
                pedido = new Pedido(Usuario.FindById(origin.IdUsuario));
                foreach(ItemV item in origin.Items)
                {
                    Item i = itemConverter.Parser(item);
                    pedido.AddItem(i, i.Quantidade);
                }
                pedido.SetRota(Sensor.FindById(origin.IdSensorInicial), Sensor.FindById(origin.IdSensorFinal));
            }
            else
            {
                pedido = Pedido.FindById(origin.Id);
            }
            

            return pedido;
        }

        public List<Pedido> Parser(List<PedidoV> origin)
        {
            if (origin.Count == 0) return null;
            List<Pedido> usuarios = new List<Pedido>();
            foreach(PedidoV u in origin)
            {
                usuarios.Add(Parser(u));
            }
            return usuarios;
        }

        public List<PedidoV> Parser(List<Pedido> origin)
        {
            if (origin.Count == 0) return null;
            List<PedidoV> usuarios = new List<PedidoV>();
            foreach (Pedido u in origin)
            {
                usuarios.Add(Parser(u));
            }
            return usuarios;
        }
    }
}
