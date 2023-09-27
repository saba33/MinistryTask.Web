using MinistryTask.Domain.Abstractions;
using MinistryTask.Repository.DatabaseContext;

namespace MinistryTask.Repository.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        public IProductRepository Products { get; }
        public IAuthorRepository Authors { get; }
        public IUserRepository Users { get; set; }
        public UnitOfWork(DataContext dataContext,
                            IProductRepository products,
                                    IAuthorRepository authors,
                                    IUserRepository userRepo)
        {
            _dataContext = dataContext;
            Products = products;
            Authors = authors;
            Users = userRepo;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Save()
        {
            return _dataContext.SaveChanges();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dataContext.Dispose();
            }
        }
    }
}
