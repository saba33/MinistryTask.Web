using Microsoft.EntityFrameworkCore;
using MinistryTask.Domain.Abstractions;
using MinistryTask.Domain.Models;
using MinistryTask.Repository.DatabaseContext;

namespace MinistryTask.Repository.Repositories
{
    public class AuthorRepository : GenericRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(DataContext dbContext) : base(dbContext)
        {

        }

        public async Task<IEnumerable<Author>> GetByName(string name)
        {
            var authors = _context.Author.Where(a => a.Name.Contains(name));
            return await Task.FromResult(authors);
        }

        public async Task<Author?> GetByPrivateNumber(string IdNumber)
        {
            var authors = _context.Author.FirstOrDefault(a => a.PrivateNumber == IdNumber);
            if (authors != null)
            {
                return await Task.FromResult(authors);
            }
            else
                return null;
        }

        public async Task<Author?> GetFullInfoById(int id)
        {
            var author = this._context.Set<Author>()
                  .Include(a => a.Products)
                  .Where(a => a.Id == id)
                  .FirstOrDefault();
            return await Task.FromResult(author);
        }

        public async Task<FilteredDataResponseModel> GetFilteredData(FilteringModel entity)
        {
            var query = _context.Author.Include(a => a.Products).AsQueryable();
            List<Author> Authors = new List<Author>();
            Authors = query.ToList();
            if (!string.IsNullOrWhiteSpace(entity.NameFilter))
            {
                query = query.Where(a => a.Name.Contains(entity.NameFilter));
            }

            if (!string.IsNullOrWhiteSpace(entity.LastNameFilter))
            {
                query = query.Where(a => a.LastName.Contains(entity.LastNameFilter));
            }

            if (!string.IsNullOrWhiteSpace(entity.GenderFilter))
            {
                query = query.Where(a => a.Gender.ToString().Contains(entity.GenderFilter));
            }

            if (!string.IsNullOrWhiteSpace(entity.PrivateNumber))
            {
                query = query.Where(a => a.PrivateNumber.Contains(entity.PrivateNumber));
            }

            var totalCount = query.Count();
            var skip = (entity.Page - 1) * entity.PageSize;
            var take = entity.PageSize;

            query = query.Skip(skip).Take(take);

            var totalPages = (int)Math.Ceiling((double)totalCount / entity.PageSize);
            var hasPreviousPage = entity.Page > 1;
            var hasNextPage = entity.Page < totalPages;


            var response = new FilteredDataResponseModel
            {
                Authors = Authors,
                TotalPages = totalPages,
                CurrentPage = entity.Page,
                HasPreviousPage = hasPreviousPage,
                HasNextPage = hasNextPage
            };
            return await Task.FromResult(response);
        }
    }
}
