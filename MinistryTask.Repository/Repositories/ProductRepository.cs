using MinistryTask.Domain;
using MinistryTask.Domain.Abstractions;
using MinistryTask.Domain.Enums;
using MinistryTask.Domain.Models;
using MinistryTask.Repository.DatabaseContext;

namespace MinistryTask.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context)
        {

        }

        public async Task<StatusOfProduct> AddProductAsync(Product Product)
        {
            _context.Products.Add(Product);
            return await Task.FromResult(StatusOfProduct.Archived);
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

        public async Task<StatusOfProduct> GetStatusOfProduct(int productId)
        {
            var productStatus = _context.Products
                .Where(p => p.Id == productId)
                .Select(p => p.ProductStatus.Status)
                .FirstOrDefault();

            if (productStatus.ToString() == null)
            {
                throw new Exception("პროდუქტი ვერ მოიძებნა");
            }

            return await Task.FromResult(productStatus);
        }

        public async Task<StatusOfProduct> ArchiveProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception("პროდუქტი ვერ მოიძებნა!");
            }

            var archivedStatus = new ProductStatus
            {
                Status = StatusOfProduct.Archived
            };
            _context.ProductStatus.Update(archivedStatus);
            product.ProductStatus = archivedStatus;

            return StatusOfProduct.Archived;
        }

        public async Task<StatusOfProduct> PublishProduct(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception("პროდუქტი ვერ მოიძებნა!");
            }

            var PublishedStatus = new ProductStatus
            {
                Status = StatusOfProduct.Published
            };

            _context.ProductStatus.Update(PublishedStatus);
            product.ProductStatus = PublishedStatus;

            return StatusOfProduct.Published;
        }


    }
}
