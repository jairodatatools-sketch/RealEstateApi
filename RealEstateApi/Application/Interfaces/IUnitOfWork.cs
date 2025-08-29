namespace RealEstateApi.Application.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitAsync();
        Task BeginTransactionAsync();
        Task RollbackAsync();

    }
}
