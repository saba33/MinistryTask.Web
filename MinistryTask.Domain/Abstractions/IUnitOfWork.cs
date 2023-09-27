namespace MinistryTask.Domain.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository Products { get; }
        IAuthorRepository Authors { get; }
        IUserRepository Users { get; }

        int Save();
    }
}
