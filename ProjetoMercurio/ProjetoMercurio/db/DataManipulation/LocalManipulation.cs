using MercurioCore.db;
using MercurioCore.Model.Interfaces;
using ProjetoMercurioCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjetoMercurioCore.db.DataManipulation
{
    class LocalManipulation<T> : IRepository<T> where T : Local
    {
        private DBConnection connection;

        public LocalManipulation()
        {
            connection = new DBConnection();
        }
        public T Create(T item)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(long id)
        {
            throw new NotImplementedException();
        }

        public List<T> FindAll()
        {
            throw new NotImplementedException();
        }

        public T FindByID(long id)
        {
            throw new NotImplementedException();
        }

        public T FindLastId()
        {
            throw new NotImplementedException();
        }

        public T Update(T item)
        {
            throw new NotImplementedException();
        }
    }
}
