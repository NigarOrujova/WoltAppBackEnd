using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WoltDataAccess.DAL;
using WoltDataAccess.Repositories.Interfaces;
using WoltEntity.Entities;

namespace WoltDataAccess.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context=context;
        }
        public async Task<Product> Get(Expression<Func<Product, bool>> exp = null, params string[] includes)
        {
            var query = _context.Products.AsQueryable();
            if (includes != null)
            {
                foreach (var item in includes)
                {
                    query.Include(item);
                }
            };
            return exp is null
                ? await query.FirstOrDefaultAsync()
                : await query.Where(exp).FirstOrDefaultAsync();
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> exp = null, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllPaginated(int page, int size, Expression<Func<Product, bool>> exp = null, params string[] includes)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Expression<Func<Product, bool>> exp = null)
        {
            throw new NotImplementedException();
        }

        public bool IsProductExist(Expression<Func<Product, bool>> exp = null)
        {
            throw new NotImplementedException();
        }
        public Task UpdateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public void Remove(Product product)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
