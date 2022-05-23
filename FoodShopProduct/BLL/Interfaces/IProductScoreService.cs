using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Dto;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface IProductScoreService
    {
        Task<ProductScore> Get(Guid id);
        Task<IEnumerable<ProductScore>> GetAll();
        Task<IEnumerable<ProductScore>> GetAll(Guid id);
        Task Create(ProductScoreDto item);
        Task Update(Guid id, ProductScoreDto item);
        Task Delete(Guid id);
    }
}