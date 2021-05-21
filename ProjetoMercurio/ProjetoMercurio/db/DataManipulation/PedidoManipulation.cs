using MercurioCore.db;
using MercurioCore.Model.Exceptions;
using MercurioCore.Model.Interfaces;
using MySqlConnector;
using ProjetoMercurioCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoMercurioCore.db.DataManipulation
{
    class PedidoManipulation<T> : IRepository<T> where T : Pedido
    {
        private DBConnection connection;
        public PedidoManipulation()
        {
            connection = new DBConnection();
        }
        public T Create(T item)
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
        public bool Exists(long id)
        {
            string sql = string.Format("SELECT IdPedido FROM  projetomercurio.pedido WHERE IdPedido={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Close();
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<T> FindAll()
        {
            List<T> items = new List<T>();
            string sql = string.Format("SELECT IdPedido FROM projetomercurio.pedido");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Pedido pedido = new Pedido((int)result["IdPedido"]);
                    items.Add((T)pedido);

                }
                result.Close();

                return (items);
            }
            else
            {
                throw new DBConnectionException("Nenhum Pedido encontrado");
            }
        }
        public T FindByID(long id)
        {
            List<Item> itens = new List<Item>();
            Usuario pedinte = null;
            Rota rota = null;
            DateTime dataPedido = new DateTime();
            string sql = string.Format(@"SELECT IdUsuario, DataCriacao, IdRota
                                        FROM projetomercurio.pedido p
                                        WHERE p.IdPedido={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                pedinte = new Usuario((int)result["IdUsuario"]);
                dataPedido = (DateTime)result["DataCriacao"];
                rota = new Rota((int)result["IdRota"]);
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
            Pedido pedido = new Pedido(Convert.ToInt32(id),pedinte, dataPedido, rota, itens);
            return (T)pedido;
        }
        public T Update(T item)
        {
            string sql = string.Format("UPDATE projetomercurio.pedido SET IdUsuario = {0}, IdRota = {1} WHERE IdPedido = {2}", item.Usuario.Id,item.Rota.Id , item.Id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao atualizar pedido no banco.");
            }
            sql = "";
            foreach (Item i in item.Items)
            {
                if (i.IdPedidoxItem != 0)
                {
                    sql = sql + string.Format("UPDATE projetomercurio.PedidoXItem SET IdItem = {0}, Quantidade  = {1} WHERE IdPedidoxItem = {2}; ", i.Id, i.Quantidade, i.IdPedidoxItem);
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
        public T FindLastId()
        {
            string sql = string.Format("SELECT IdPedido FROM projetomercurio.pedido order by IdPedido desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Pedido pedido = new Pedido((int)result["IdPedido"]);
                result.Close();
                return (T)pedido;
            }
            else
            {
                throw new DBConnectionException("Nenhum Pedido encontrado");
            }
        }
        private int LastId()
        {
            string sql = string.Format("SELECT IdPedido FROM projetomercurio.pedido order by IdPedido desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                int pedido = (int)result["IdPedido"];
                result.Close();
                return pedido;
            }
            else
            {
                throw new DBConnectionException("Nenhum Pedido encontrado");
            }
        }
    }
}
