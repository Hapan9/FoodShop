using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BLL.Dto;
using DAL.Models;

namespace BLL.Interfaces
{
    public interface IProductInfoService
    {
        Task<ProductInfo> Get(Guid id);
        Task<IEnumerable<ProductInfo>> GetAll();
        Task Create(ProductInfoDto item);
        Task Update(Guid id, ProductInfoDto item);
        Task Delete(Guid id);
    }
}