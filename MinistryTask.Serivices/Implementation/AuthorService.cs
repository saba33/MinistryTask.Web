using AutoMapper;
using Microsoft.AspNetCore.Http;
using MinistryTask.Domain;
using MinistryTask.Domain.Abstractions;
using MinistryTask.Domain.Models;
using MinistryTask.Serivices.Abstraction;
using MinistryTask.Serivices.Infrastructure.Exceptions;
using MinistryTask.Serivices.Models.RequestModels.AuthorRequestModel;
using MinistryTask.Serivices.Models.RequestModels.ProductRequestModel;
using MinistryTask.Serivices.Models.ResposeModels.AuthorResponseModels;

namespace MinistryTask.Serivices.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public AuthorService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<AddAuthorResponse> AddAuthor(AuthorDto entity)
        {
            var AuthorToInsert = _mapper.Map<Author>(entity);
            await _unit.Authors.Add(AuthorToInsert);
            _unit.Save();
            return new AddAuthorResponse
            {
                ActionDate = DateTime.UtcNow,
                StatusCode = StatusCodes.Status200OK,
                Message = "ავტორი წარმატებით დაემატა სისტემაში."
            };
        }

        public async Task<AddProductToAuthorResponse> AddProductToAuthor(int authorId, ProductDto product)
        {
            var author = await _unit.Authors.GetById(authorId);
            var productToInsert = _mapper.Map<Product>(product);
            if (author == null)
                throw new AuthorNotFoundException($"ავტორი აიდით {authorId} ვერ მოიძებნა სისტემაში");

            if (author.Products == null)
            {
                author.Products = new List<Product>();
            }
            if (productToInsert.Authors == null)
            {
                productToInsert.Authors = new List<Author>();
            }
            author.Products.Add(productToInsert);
            productToInsert.Authors.Add(author);
            await _unit.Products.Add(productToInsert);

            _unit.Save();

            return new AddProductToAuthorResponse
            {
                ActionDate = DateTime.UtcNow,
                AuthorId = authorId,
                StatusCode = StatusCodes.Status200OK,
                Message = "პროდუქტი წარმატებით დაემატა ავტორს სისტემაში."
            };
        }

        public async Task<DeleteAuthorResponse> DeleteAuthor(int productId)
        {
            var result = await _unit.Authors.GetById(productId);
            var authorToDelete = _mapper.Map<Author>(result);
            _unit.Authors.Delete(authorToDelete);
            _unit.Save();
            return new DeleteAuthorResponse
            {
                AuthorId = authorToDelete.Id,
                ActionDate = DateTime.UtcNow,
                Message = $"ავტორი აიდით {authorToDelete.Id} წარმატებით წაიშალა ბაზიდან."
            };
        }

        public async Task<DeleteProductToAuthorResponse> DeleteProductToAuthor(int authorId, int productId)
        {
            var author = await _unit.Authors.GetFullInfoById(authorId);
            var productToDelete = await _unit.Products.GetById(productId);
            if (author == null)
                throw new AuthorNotFoundException($"ავტორი აიდით {authorId} ვერ მოიძებნა სისტემაში");
            if (productToDelete == null)
            {
                productToDelete.Authors = new List<Author>();
            }
            author.Products.Remove(productToDelete);
            _unit.Save();
            return new DeleteProductToAuthorResponse
            {
                ActionDate = DateTime.UtcNow,
                StatusCode = StatusCodes.Status200OK,
                Message = $"პროდუქტი წარმატებით წაიშალა"
            };
        }

        public async Task<GetAuthorInfoResponse> GetAuthorFullInfoById(int authorId)
        {
            var author = await _unit.Authors.GetFullInfoById(authorId);
            if (author == null)
                throw new AuthorNotFoundException($"ავტორი აიდით {authorId} ვერ მოიძებნა სისტემაში");
            var authorToDisplay = _mapper.Map<AuthorDisplayModel>(author);
            return new GetAuthorInfoResponse
            {
                ActionDate = DateTime.UtcNow,
                Author = authorToDisplay,
                StatusCode = StatusCodes.Status200OK,
                Message = "ავტორი წარმატებით მოიძებნა."
            };
        }

        public async Task<GetAuthorsWithFiltersResponse> GetAuthorsWithFilters(FilteringAuthorModel entity)
        {
            var modelToPass = _mapper.Map<FilteringModel>(entity);
            var result = await _unit.Authors.GetFilteredData(modelToPass);
            var returnModel = _mapper.Map<GetAuthorsWithFiltersResponse>(result);
            return returnModel;
        }

        public async Task<UpdateAuthorResponse> UpdateAuthor(AuthorDto entity, int id)
        {
            var AuthorToUpdate = await _unit.Authors.GetFullInfoById(id);
            _mapper.Map(entity, AuthorToUpdate);
            _unit.Authors.Update(AuthorToUpdate);
            _unit.Save();
            return new UpdateAuthorResponse
            {
                ActionDate = DateTime.UtcNow,
                AuthorId = id,
                Message = "ავტორი წარმატებით დარედაქტირდა.",
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
}
