using MinistryTask.Serivices.Models.RequestModels.ProductRequestModel;
using MinistryTask.Serivices.Models.ResposeModels.ProductResponseModels;

namespace MinistryTask.Serivices.Abstraction
{
    public interface IProductService
    {
        Task<AddProductResponseModel> AddProductAsync(ProductDto entity);
        Task<ArchiveProductResponse> ArchiveProduct(int id);
        Task<PublishProductResponse> PublishProduct(int id);
        Task<GetProductStatusResponse> GetProductStatusAsync(int id);
    }
}
