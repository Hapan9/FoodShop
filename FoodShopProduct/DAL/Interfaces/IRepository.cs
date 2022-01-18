using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> Get(Guid id);

        Task<IEnumerable<T>> GetAll();

        Task Create(T item);

        Task Update(T item);

        Task Delete(Guid id);

        Task Save();
    }
}