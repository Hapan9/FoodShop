using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Dto;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface IProductService
    {
        Task<Product> Get(Guid id);
        Task<IEnumerable<Product>> GetAll();
        Task Create(ProductDto item);
        Task Update(Guid id, ProductDto item);
        Task Delete(Guid id);
    }
}