using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    class PedidoManipulation : IRepository<Pedido>
    {
        private DBConnection connection;
        public PedidoManipulation()
        {
            connection = new DBConnection();
        }

        public Pedido Create(Pedido item)
        {
            string sql = string.Format("INSERT INTO projetomercurio.pedido (IdUsuario, DataCriacao, IdRota) VALUES ({0}, NOW(), {1});", item.Usuario.Id, item.Rota.Id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir Pedido no banco.");
            }

            int novo = LastId();

            string pedidoXitem = "";
            foreach (Item i in item.Items)
            {
                pedidoXitem = pedidoXitem + string.Format("INSERT INTO projetomercurio.PedidoXItem (IdPedido, IdItem, Quantidade) VALUES ({0},{1}, {2}); ", novo, i.Id, i.Quantidade);
            }

            if (!connection.SendCommand(pedidoXitem))
            {
                throw new DBConnectionException("Erro ao inserir PedidoXItem no banco.");
            }



            return FindLastId();
        }

        public void Delete(long id)
        {
            string sql = string.Format("DELETE FROM projetomercurio.PedidoXItem WHERE IdPedido={0};", id);
            sql = sql + string.Format("DELETE FROM projetomercurio.pedido WHERE IdPedido={0}; ", id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao Remover Pedido no banco.");
            }
        }

        public List<Pedido> FindAll()
        {
            List<Pedido> items = new List<Pedido>();
            string sql = string.Format("SELECT IdPedido FROM projetomercurio.pedido");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Pedido pedido = new Pedido((int)result["IdPedido"]);
                    items.Add(pedido);

                }
                

                
            }
            result.Close();
            return (items);
        }

        public Pedido FindByID(long id)
        {
            List<Item> itens = new List<Item>();
            Usuario pedinte = null;
            Rota rota = null;
            bool pedidoEntregue = false;
            DateTime dataPedido = new DateTime();
            string sql = string.Format(@"SELECT IdUsuario, DataCriacao, IdRota, PedidoEntregue
                                        FROM projetomercurio.pedido p
                                        WHERE p.IdPedido={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                pedinte = new Usuario((int)result["IdUsuario"]);
                dataPedido = (DateTime)result["DataCriacao"];
                rota = new Rota((int)result["IdRota"]);
                pedidoEntregue = (bool)result["PedidoEntregue"];
                result.Close();

            }
            else
            {
                throw new DBConnectionException("Nenhum Pedido encontrado");
            }

            sql = string.Format("SELECT IdItem, IdPedidoxItem, Quantidade FROM projetomercurio.PedidoXItem WHERE IdPedido = {0}", id);

            MySqlDataReader result2 = connection.SendQuery(sql);

            if (result2.HasRows)
            {
                while (result2.Read())
                {
                    Item item2 = new Item((int)result2["IdItem"]);

                    item2.IdPedidoxItem = (int)result2["IdPedidoxItem"];
                    item2.Quantidade = (int)result2["Quantidade"];
                    itens.Add(item2);
                }
            }
            result2.Close();
            Pedido pedido = new Pedido(Convert.ToInt32(id), pedinte, dataPedido, rota, itens, pedidoEntregue);
            return pedido;
        }

        public Pedido FindLastId()
        {
            string sql = string.Format("SELECT IdPedido FROM projetomercurio.pedido order by IdPedido desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            Pedido pedido = null;
            if (result.HasRows)
            {
                result.Read();
                pedido = new Pedido((int)result["IdPedido"]);
                
                
            }
            result.Close();
            return pedido;
        }

        public Pedido Update(Pedido item)
        {
            string sql = string.Format("UPDATE projetomercurio.pedido SET IdUsuario = {0}, IdRota = {1} WHERE IdPedido = {2}", item.Usuario.Id, item.Rota.Id, item.Id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao atualizar pedido no banco.");
            }
            sql = "";
            foreach (Item i in item.Items)
            {
                if (i.IdPedidoxItem != 0)
                {
                    if (i.RemoveItem)
                    {
                        sql = sql + string.Format("DELETE FROM projetomercurio.PedidoXItem WHERE IdPedidoxItem = {0}; ", i.IdPedidoxItem);
                    }
                    else
                    {
                        sql = sql + string.Format("UPDATE projetomercurio.PedidoXItem SET IdItem = {0}, Quantidade  = {1} WHERE IdPedidoxItem = {2}; ", i.Id, i.Quantidade, i.IdPedidoxItem);
                    }
                    
                }
                else
                {
                    sql = sql + string.Format("INSERT INTO projetomercurio.PedidoXItem (IdPedido, IdItem, Quantidade) VALUES ({0},{1},{2}); ", item.Id, i.Id, i.Quantidade);
                }

            }
            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao Atualizar pedidoxitem no banco.");
            }

            return FindByID(item.Id);
        }
        private int LastId()
        {
            int pedido = 1;
            string sql = string.Format("SELECT IdPedido FROM projetomercurio.pedido order by IdPedido desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                pedido = (int)result["IdPedido"];
                
                
            }
            result.Close();
            return pedido;
        }
    }
}
