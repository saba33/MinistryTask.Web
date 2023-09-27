using MinistryTask.Domain;
using MinistryTask.Domain.Abstractions;
using MinistryTask.Repository.DatabaseContext;

namespace MinistryTask.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Product>?> GetByISBNAsync(string ISBN)
        {
            var product = base._context.Products.Where(p => p.Name == ISBN);
            if (product != null)
            {
                return await Task.FromResult(product);
            }
            return null;
        }

        public async Task<IEnumerable<Product>?> GetByNameAync(string name)
        {
            var product = base._context.Products.Where(p => p.Name == name);
            if (product != null)
            {
                return await Task.FromResult(product);
            }
            return null;
        }
    }
}
