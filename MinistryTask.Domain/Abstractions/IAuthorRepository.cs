using MinistryTask.Domain.Models;

namespace MinistryTask.Domain.Abstractions
{
    public interface IAuthorRepository : IGenericRepository<Author>
    {
        Task<Author> GetByPrivateNumber(string IdNumber);
        Task<IEnumerable<Author>> GetByName(string Name);
        Task<Author> GetFullInfoById(int id);
        Task<FilteredDataResponseModel> GetFilteredData(FilteringModel entity);
    }
}
