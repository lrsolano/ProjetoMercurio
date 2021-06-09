using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mercurio.Core
{
    class ItemManipulation : IRepository<Item>
    {
        private DBConnection connection;
        public ItemManipulation()
        {
            connection = new DBConnection();
        }
        public Item Create(Item item)
        {
            string sql = string.Format("INSERT INTO projetomercurio.item (Nome, DataCriacao) VALUES ('{0}', NOW())", item.Nome);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao inserir item no banco.");
            }

            return FindLastId();
        }

        public void Delete(long id)
        {
            string sql = string.Format("DELETE FROM projetomercurio.item WHERE IdItem={0}", id);
            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao remover item no banco.");
            }
        }

        public List<Item> FindAll()
        {
            List<Item> items = new List<Item>();
            string sql = string.Format("SELECT IdItem, Nome, DataCriacao FROM projetomercurio.item");
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                while (result.Read())
                {
                    Item item = new Item((int)result["IdItem"], result["Nome"].ToString(), (DateTime)result["DataCriacao"]);
                    items.Add(item);
                }
                
            }
            result.Close();
            return items;
        }

        public Item FindByID(long id)
        {
            Item item = null;
            string sql = string.Format("SELECT IdItem, Nome, DataCriacao FROM  projetomercurio.item WHERE IdItem={0} ", id);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                item = new Item((int)result["IdItem"], result["Nome"].ToString(), (DateTime)result["DataCriacao"]);
                

            }
            result.Close();
            return item;
        }

        public Item FindLastId()
        {
            string sql = string.Format("SELECT IdItem, Nome, DataCriacao FROM projetomercurio.item order by IdItem desc limit 1 ");
            MySqlDataReader result = connection.SendQuery(sql);
            Item item = null;
            if (result.HasRows)
            {
                result.Read();
                item = new Item((int)result["IdItem"], result["Nome"].ToString(), (DateTime)result["DataCriacao"]);
                

            }
            result.Close();
            return item;
        }

        public Item Update(Item item)
        {
            string sql = string.Format("UPDATE projetomercurio.item SET  Nome = '{0}' WHERE IdItem = {1}", item.Nome, item.Id);

            if (!connection.SendCommand(sql))
            {
                throw new DBConnectionException("Erro ao atualizar item no banco.");
            }

            return FindByID(item.Id);
        }

        public Item FindByName(string nome)
        {
            Item item = null;
            string sql = string.Format("SELECT IdItem, Nome, DataCriacao FROM  projetomercurio.item WHERE Nome='{0}' ", nome);
            MySqlDataReader result = connection.SendQuery(sql);
            if (result.HasRows)
            {
                result.Read();
                item = new Item((int)result["IdItem"], result["Nome"].ToString(), (DateTime)result["DataCriacao"]);
            }
            result.Close();
            return item;

        }
        public bool CanDelete(long id)
        {
            bool resultado;

            string sql = string.Format(@"SELECT IdPedidoxItem FROM projetomercurio.PedidoXItem
                                            where IdItem = {0};", id);
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
    }
}
