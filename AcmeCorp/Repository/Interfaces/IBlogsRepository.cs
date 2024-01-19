using AcmeCorp.Data.Models;

namespace AcmeCorp.Repository.interfaces
{
    public interface IBlogsRepository : IDisposable
    {
        Task<List<Blogs>> Get(bool active);
        Task<Blogs> GetById(Guid id);
        Task<Blogs> Create(Blogs model);
        Task<Blogs> Update(Blogs model);
        Task Delete(Guid id);
    }
}
