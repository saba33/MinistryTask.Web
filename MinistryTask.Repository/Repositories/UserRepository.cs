using MinistryTask.Domain.Abstractions;
using MinistryTask.Domain.Models;
using MinistryTask.Repository.DatabaseContext;
using System.Linq.Expressions;

namespace MinistryTask.Repository.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {

        }

        public Task<IEnumerable<User>> FindUserAsync(Expression<Func<User, bool>> predicate)
        {
            return this.FindAsync(predicate);
        }
    }
}
