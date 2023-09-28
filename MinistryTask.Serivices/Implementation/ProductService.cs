using AutoMapper;
using Microsoft.AspNetCore.Http;
using MinistryTask.Domain;
using MinistryTask.Domain.Abstractions;
using MinistryTask.Domain.Enums;
using MinistryTask.Domain.Models;
using MinistryTask.Serivices.Abstraction;
using MinistryTask.Serivices.Models.RequestModels.ProductRequestModel;
using MinistryTask.Serivices.Models.ResposeModels.ProductResponseModels;

namespace MinistryTask.Serivices.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork init, IMapper mapper)
        {
            _unit = init;
            _mapper = mapper;
        }

        public async Task<AddProductResponseModel> AddProductAsync(ProductDto entity)
        {
            var ProductToInsert = _mapper.Map<Product>(entity);

            var productStatus = new ProductStatus
            {
                Status = StatusOfProduct.Archived
            };

            ProductToInsert.ProductStatus = productStatus;

            await _unit.Products.AddProductAsync(ProductToInsert);
            _unit.Save();
            return new AddProductResponseModel
            {
                Message = "პროდუქტი წამრატებით დაემატა სისტემაში",
                StatusCode = StatusCodes.Status200OK,
            };
        }

        public async Task<ArchiveProductResponse> ArchiveProduct(int id)
        {
            var result = await _unit.Products.ArchiveProduct(id);
            _unit.Save();
            return new ArchiveProductResponse
            {
                Status = result.ToString(),
                Message = "პროდუქტი დაარქივებულია.",
                ProductId = id,
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<PublishProductResponse> PublishProduct(int id)
        {
            var result = await _unit.Products.PublishProduct(id);
            _unit.Save();
            return new PublishProductResponse
            {
                Status = result.ToString(),
                Message = "პროდუქტი დაარქივებულია.",
                ProductId = id,
                StatusCode = StatusCodes.Status200OK
            };
        }

        public async Task<GetProductStatusResponse> GetProductStatusAsync(int id)
        {
            var result = await _unit.Products.GetStatusOfProduct(id);
            return new GetProductStatusResponse
            {
                Message = $"პროდუქტის სტატუსი {result} წარმატებით დაბრუნდა",
                ProductStatus = result.ToString(),
                StatusCode = StatusCodes.Status200OK,
            };
        }
    }
}
