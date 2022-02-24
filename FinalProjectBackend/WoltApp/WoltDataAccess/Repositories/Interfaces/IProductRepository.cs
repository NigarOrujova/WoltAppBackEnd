using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoltEntity.Entities;

namespace WoltDataAccess.Repositories.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetAll(Expression<Func<Product, bool>> exp = null, params string[] includes);
        List<Product> GetAllPaginated(int page,int size,Expression<Func<Product, bool>> exp = null, params string[] includes);
        Task<Product> Get(Expression<Func<Product, bool>> exp = null, params string[] includes);
        int GetTotalCount(Expression<Func<Product, bool>> exp = null);
        bool IsProductExist(Expression<Func<Product, bool>> exp = null);
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        void Remove(Product product);
        Task SaveAsync();

    }
}
