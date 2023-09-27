using MinistryTask.Serivices.Models.RequestModels.AuthorRequestModel;
using MinistryTask.Serivices.Models.RequestModels.ProductRequestModel;
using MinistryTask.Serivices.Models.ResposeModels.AuthorResponseModels;

namespace MinistryTask.Serivices.Abstraction
{
    public interface IAuthorService
    {
        Task<AddAuthorResponse> AddAuthor(AuthorDto entity);
        Task<DeleteAuthorResponse> DeleteAuthor(int productId);
        Task<UpdateAuthorResponse> UpdateAuthor(AuthorDto entity, int id);
        Task<GetAuthorInfoResponse> GetAuthorFullInfoById(int authorId);
        Task<AddProductToAuthorResponse> AddProductToAuthor(int authorId, ProductDto product);
        Task<DeleteProductToAuthorResponse> DeleteProductToAuthor(int authorId, int productId);
        Task<GetAuthorsWithFiltersResponse> GetAuthorsWithFilters(FilteringAuthorModel entity);
    }
}
