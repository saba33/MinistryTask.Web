using MinistryTask.Domain.Models;
using System.Linq.Expressions;

namespace MinistryTask.Domain.Abstractions
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<IEnumerable<User>> FindUserAsync(Expression<Func<User, bool>> predicate);
    }
}
