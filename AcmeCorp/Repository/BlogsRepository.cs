using AcmeCorp.Data;
using AcmeCorp.Data.Models;
using AcmeCorp.Repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace AcmeCorp.Repository
{
    public class BlogsRepository : IBlogsRepository
    {
        private readonly ApplicationDbContext _context;
        private bool disposedValue;

        public BlogsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public async Task<List<Blogs>> Get(bool active)
        {
            if(active)
                return await _context.Blogs.Where(b => b.Active == true).OrderByDescending(b => b.CreationDate).ToListAsync();

            return await _context.Blogs.OrderByDescending(b => b.CreationDate).ToListAsync();
        }

        public async Task<Blogs> GetById(Guid id)
        {
            return await _context.Blogs.FirstOrDefaultAsync(o => o.BlogsId == id);
        }

        public async Task<Blogs> Create(Blogs model)
        {
            await _context.Blogs.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<Blogs> Update(Blogs model)
        {
            _context.Blogs.Attach(model);
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task Delete(Guid id)
        {
            Blogs Blogs = await GetById(id);

            _context.Blogs.Remove(Blogs);
            await _context.SaveChangesAsync();
        }
        public bool releaseDateChecker(bool active, DateTime? blogReleaseDate)
        {
            if (active == true && blogReleaseDate == null)
                return true;
            return false;
            
        }
    }
}
