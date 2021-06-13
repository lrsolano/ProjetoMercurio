using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    class CarrinhoManipulation : IRepository<Carrinho>
    {
        private DBConnection connection;
        public CarrinhoManipulation()
        {
            connection = new DBConnection();
        }
        public Carrinho Create(Carrinho item)
        {
            string sql = string.Empty;
            if (item.Pedido == null & item.UltimoSensor == null)
            {
                sql = string.Format("INSERT INTO projetomercurio.carrinho (Nome, Disponivel,HashRota,DataCriacao) VALUES ('{0}',{1},'{2}', NOW())", item.Nome, item.Disponivel, item.HashRota);
            }
            else if (item.Pedido != null & item.UltimoSensor == null)
            {
                sql = string.Format("INSERT INTO projetomercurio.carrinho (Nome, IdPedido,Disponivel,HashRota,DataCriacao) VALUES ('{0}',{1},{2},'{3}', NOW())", item.Nome, item.Pedido.Id, item.Disponivel, item.HashRota);
            }
            else if (item.Pedido == null & item.UltimoSensor != null)
            {
                sql = string.Format("INSERT INTO projetomercurio.carrinho (Nome,Disponivel,IdUltimoSensor,HashRota,DataCriacao) VALUES ('{0}',{1},{2},'{3},' NOW())", item.Nome,  item.Disponivel, item.UltimoSensor.Id, item.HashRota);
            }
            else
            {
                sql = string.Format("INSERT INTO projetomercurio.carrinho (Nome, IdPedido,Disponivel,IdUltimoSensor,HashRota,DataCriacao) VALUES ('{0}',{1},{2},{3},'{4}', NOW())", item.Nome, item.Pedido.Id, item.Disponivel, item.UltimoSensor.Id, item.HashRota);
            }

            

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir carrinho no banco.");
            }

            return FindLastId();
        }

        public void Delete(long id)
        {
            string sql = string.Format("DELETE FROM projetomercurio.carrinho WHERE IdCarrinho={0}", id);
            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao remover carrinho no banco.");
            }
        }

        public List<Carrinho> FindAll()
        {
            List<Carrinho> items = new List<Carrinho>();
            string sql = string.Format("SELECT IdCarrinho,Nome, IdPedido,Disponivel,IdUltimoSensor,HashRota,DataCriacao FROM projetomercurio.carrinho");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Pedido pedido = null;
                    Sensor sensor = null;
                    if (result["IdPedido"].GetType() != typeof(System.DBNull))
                    {
                        pedido = new Pedido((int)result["IdPedido"]);
                    }
                    if (result["IdUltimoSensor"].GetType() != typeof(System.DBNull))
                    {
                        sensor = new Sensor((int)result["IdUltimoSensor"]);
                    }
                    Carrinho item = new Carrinho((int)result["IdCarrinho"], result["Nome"].ToString(), pedido,(bool)result["Disponivel"],sensor, result["HashRota"].ToString(),(DateTime)result["DataCriacao"]);
                    items.Add(item);
                }
                
            }
            result.Close();
            return items;
        }

        public Carrinho FindByID(long id)
        {
            Carrinho item = null;
            string sql = string.Format("SELECT IdCarrinho,Nome, IdPedido,Disponivel,IdUltimoSensor,HashRota,DataCriacao FROM projetomercurio.carrinho WHERE IdCarrinho={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Pedido pedido = null;
                Sensor sensor = null;
                if (result["IdPedido"].GetType() != typeof(System.DBNull))
                {
                    pedido = new Pedido((int)result["IdPedido"]);
                }
                if (result["IdUltimoSensor"].GetType() != typeof(System.DBNull))
                {
                    sensor = new Sensor((int)result["IdUltimoSensor"]);
                }
                item = new Carrinho((int)result["IdCarrinho"], result["Nome"].ToString(), pedido, (bool)result["Disponivel"], sensor, result["HashRota"].ToString(), (DateTime)result["DataCriacao"]);

                

            }
            result.Close();
            return item;
        }

        public Carrinho FindLastId()
        {
            string sql = string.Format("SELECT IdCarrinho,Nome, IdPedido,Disponivel,IdUltimoSensor,HashRota,DataCriacao FROM projetomercurio.carrinho order by IdCarrinho desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            Carrinho item = null;
            if (result.HasRows)
            {
                result.Read();
                Pedido pedido = null;
                Sensor sensor = null;
                if (result["IdPedido"].GetType() != typeof(System.DBNull))
                {
                    pedido = new Pedido((int)result["IdPedido"]);
                }
                if (result["IdUltimoSensor"].GetType() != typeof(System.DBNull))
                {
                    sensor = new Sensor((int)result["IdUltimoSensor"]);
                }
                item = new Carrinho((int)result["IdCarrinho"], result["Nome"].ToString(), pedido, (bool)result["Disponivel"], sensor, result["HashRota"].ToString(), (DateTime)result["DataCriacao"]);



            }
            result.Close();
            return item;
        }

        public Carrinho Update(Carrinho item)
        {
            string sql = string.Empty;
            if (item.Pedido == null & item.UltimoSensor == null)
            {
                sql = string.Format("UPDATE projetomercurio.carrinho SET  Nome = '{0}', Disponivel = {1}, HashRota = '{2}'  WHERE IdCarrinho = {3}", item.Nome, item.Disponivel, item.HashRota, item.Id);
            }
            else if (item.Pedido != null & item.UltimoSensor == null)
            {
                sql = string.Format("UPDATE projetomercurio.carrinho SET  Nome = '{0}', IdPedido = {1}, Disponivel = {2}, HashRota = '{3}'  WHERE IdCarrinho = {4}", item.Nome, item.Pedido.Id, item.Disponivel, item.HashRota, item.Id);
            }
            else if (item.Pedido == null & item.UltimoSensor != null)
            {
                sql = string.Format("UPDATE projetomercurio.carrinho SET  Nome = '{0}',  Disponivel = {1}, IdUltimoSensor = {2}, HashRota = '{3}'  WHERE IdCarrinho = {4}", item.Nome, item.Disponivel, item.UltimoSensor.Id, item.HashRota, item.Id);
            }
            else
            {
                sql = string.Format("UPDATE projetomercurio.carrinho SET  Nome = '{0}', IdPedido = {1}, Disponivel = {2}, IdUltimoSensor = {3}, HashRota = '{4}'  WHERE IdCarrinho = {5}", item.Nome, item.Pedido.Id, item.Disponivel, item.UltimoSensor.Id, item.HashRota, item.Id);
            }
           

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao atualizar carrinho no banco.");
            }

            return FindByID(item.Id);
        }

        public Carrinho FindByName(string nome)
        {
            Carrinho item = null;
            string sql = string.Format("SELECT IdCarrinho,Nome, IdPedido,Disponivel,IdUltimoSensor,HashRota,DataCriacao FROM projetomercurio.carrinho WHERE Nome='{0}' ", nome);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                Pedido pedido = null;
                Sensor sensor = null;
                if(result["IdPedido"].GetType() != typeof(System.DBNull))
                {
                    pedido = new Pedido((int)result["IdPedido"]);
                }
                if (result["IdUltimoSensor"].GetType() != typeof(System.DBNull))
                {
                    sensor = new Sensor((int)result["IdUltimoSensor"]);
                }
                item = new Carrinho((int)result["IdCarrinho"], result["Nome"].ToString(), pedido, (bool)result["Disponivel"], sensor, result["HashRota"].ToString(), (DateTime)result["DataCriacao"]);

            }
            result.Close();
            return item;

        }
        public bool CanDelete(long id)
        {
            bool resultado;

            string sql = string.Format(@"SELECT 1 FROM projetomercurio.carrinho
                                            where IdCarrinho = {0} and Disponivel = true;", id);
            MySqlDataReader result = connection.SendQuery(sql);

            if (result.HasRows)
            {
                result.Close();
                resultado = false;
            }
            else
            {
                result.Close();
                resultado = true;
            }

            return resultado;
        }
        public bool FinalizarPedido(Carrinho item)
        {
            bool retorno = false;
            string sql = string.Format("UPDATE projetomercurio.carrinho SET  Disponivel = true, HashRota = '', IdUltimoSensor = null, IdPedido = null   WHERE IdCarrinho = {0};",  item.Id);
            sql += string.Format("UPDATE projetomercurio.pedido SET PedidoEntregue = true WHERE IdPedido = {0};", item.Pedido.Id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao atualizar carrinho no banco.");
            }
            retorno = true;
            return retorno;
        }
    }
}
