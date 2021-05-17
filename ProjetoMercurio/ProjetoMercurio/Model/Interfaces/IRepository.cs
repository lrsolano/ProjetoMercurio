using ProjetoMercurioCore.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MercurioCore.Model.Interfaces
{
    public interface IRepository<T>
    {
        T Create(T item);
        T FindByID(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long id);
        bool Exists(long id);
        T FindLastId();
    }
}
