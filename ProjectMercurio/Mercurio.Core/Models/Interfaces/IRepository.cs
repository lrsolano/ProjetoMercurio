using System.Collections.Generic;

namespace Mercurio.Core
{
    public interface IRepository<T> where T : CommonColumns
    {
        T Create(T item);
        T FindByID(long id);
        List<T> FindAll();
        T Update(T item);
        void Delete(long id);
        T FindLastId();
    }
}
