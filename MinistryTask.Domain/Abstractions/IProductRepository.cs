using MinistryTask.Domain.Enums;

namespace MinistryTask.Domain.Abstractions
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<StatusOfProduct> AddProductAsync(Product entity);
        Task<IEnumerable<Product>> GetByNameAync(string name);
        Task<IEnumerable<Product>> GetByISBNAsync(string name);
        Task<StatusOfProduct> ArchiveProduct(int productId);
        Task<StatusOfProduct> PublishProduct(int productId);
        Task<StatusOfProduct> GetStatusOfProduct(int productId);
    }
}
