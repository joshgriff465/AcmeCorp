using AcmeCorp.Data;
using AcmeCorp.Data.Models;
using AcmeCorp.Repository.interfaces;
using Microsoft.EntityFrameworkCore;

namespace AcmeCorp.Repository
{
    public class NewsRepository : INewsRepository
    {
        private readonly ApplicationDbContext _context;
        private bool disposedValue;

        public NewsRepository(ApplicationDbContext context)
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

        public async Task<List<News>> Get()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<News> GetById(Guid id)
        {
            return await _context.News.FirstOrDefaultAsync(o => o.NewsId == id);
        }

        public async Task<News> Create(News model)
        {
            await _context.News.AddAsync(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<News> Update(News model)
        {
            _context.News.Attach(model);
            _context.Entry(model).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task Delete(Guid id)
        {
            News news = await GetById(id);

            _context.News.Remove(news);
            await _context.SaveChangesAsync();
        }

    }
}
