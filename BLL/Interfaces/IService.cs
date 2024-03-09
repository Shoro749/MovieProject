using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IService<T>
    {
        IEnumerable<T> GetAll();
        void Add(T item);
        void Remove(int id);
        void Update(int id, T item);
        string GetById(int id);
    }
}
