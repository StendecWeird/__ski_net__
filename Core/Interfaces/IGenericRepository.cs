using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : EntityBase
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
    }
}