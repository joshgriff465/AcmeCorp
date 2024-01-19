using AcmeCorp.Data.Models;

namespace AcmeCorp.Repository.interfaces
{
    public interface INewsRepository : IDisposable
    {
        Task<List<News>> Get();
        Task<News> GetById(Guid id);
        Task<News> Create(News model);
        Task<News> Update(News model);
        Task Delete(Guid id);
    }
}
