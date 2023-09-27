namespace MinistryTask.Domain.Abstractions
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetByNameAync(string name);
        Task<IEnumerable<Product>> GetByISBNAsync(string name);
    }
}
