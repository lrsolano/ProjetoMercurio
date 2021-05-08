using MercurioCore.Model;
using MercurioCore.Model.Exceptions;
using MercurioCore.Model.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Text;

namespace MercurioCore.db.DataManipulation
{
    public class PedidoManipulation<T> : IRepository<T> where T : Pedido
    {
        private DBConnection connection;

        public PedidoManipulation()
        {
            connection = new DBConnection();
        }

        public T Create(T item)
        {
            string sql = string.Format("INSERT INTO projetomercurio.pedido (IdPedinte, DataPedido) VALUES ({0}, NOW());", item.pedinte.Id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir pedido no banco.");
            }

            int novo = LastId();

            string pedidoXitem = "";
            foreach (Item i in item.Itens)
            {
                pedidoXitem = pedidoXitem + string.Format("INSERT INTO projetomercurio.pedidoxitem (IdPedido, IdItem) VALUES ({0},{1}); ",novo, i.Id);
            }

            if (!connection.SendCommand(pedidoXitem))
            {
                throw new DBConnectionException("Erro ao inserir pedidoxitem no banco.");
            }



            return FindLastId();
        }

        public void Delete(long id)
        {
            string sql = string.Format("DELETE FROM projetomercurio.pedidoxitem WHERE IdPedido={0};", id);
            sql = sql + string.Format("DELETE FROM projetomercurio.pedido WHERE IdPedido={0}; ", id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir item no banco.");
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
                throw new DBConnectionException("Nenhum registro encontrado");
            }

        }

        public T FindByID(long id)
        {
            List<Item> itens = new List<Item>();
            Pedinte pedinte = null;
            DateTime dataPedido = new DateTime();
            string sql = string.Format(@"SELECT IdPedinte, DataPedido
                                        FROM projetomercurio.pedido p
                                        WHERE p.IdPedido={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                pedinte = new Pedinte((int)result["IdPedinte"]);
                dataPedido = (DateTime)result["DataPedido"];
                result.Close();

            }
            else
            {
                throw new DBConnectionException("Nenhum registro encontrado");
            }

            sql = string.Format("SELECT IdItem, IdPedidoxItem FROM projetomercurio.pedidoxitem WHERE IdPedido = {0}", id);

            MySqlDataReader result2 = connection.SendQuery(sql);

            if (result2.HasRows)
            {
                while (result2.Read())
                {
                    Item item2 = new Item((int)result2["IdItem"]);
                    item2.IdPedidoXItem = (int)result2["IdPedidoxItem"];
                    itens.Add(item2);
                }
            }           
            result2.Close();
            Pedido pedido = new Pedido((int)id, pedinte, itens, dataPedido);
            return (T)pedido;
            
            
        }

        public T Update(T item)
        {
            string sql = string.Format("UPDATE projetomercurio.pedido SET IdPedinte = {0}, DataPedido = '{1}' WHERE IdPedido = {2}", item.pedinte.Id, item.DataPedido.ToString("yyyy-MM-dd HH:mm:ss"), item.Id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao atualizar pedido no banco.");
            }
            sql = "";
            foreach(Item i in item.Itens)
            {
                if(i.IdPedidoXItem != 0)
                {
                    sql = sql + string.Format("UPDATE projetomercurio.pedidoxitem SET IdItem = {0} WHERE IdPedidoxItem = {1}; ", i.Id, i.IdPedidoXItem);
                }
                else
                {
                    sql = sql + string.Format("INSERT INTO projetomercurio.pedidoxitem (IdPedido, IdItem) VALUES ({0},{1}); ", item.Id, i.Id);
                }
                
            }
            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir pedidoxitem no banco.");
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
                throw new DBConnectionException("Nenhum registro encontrado");
            }
        }

        public int LastId()
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
                throw new DBConnectionException("Nenhum registro encontrado");
            }
        }
    }
}
