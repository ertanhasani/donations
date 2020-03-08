using Donations.Data.Interfaces;
using Donations.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Donations.Data
{
    public class DonationsRepository : IDonationsRepository
    {
        private DonationsDbContext _context;

        public DonationsRepository(DonationsDbContext context)
        {
            _context = context;
        }

        public async Task Add<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            await _context.AddAsync(entity);
        }

        public IEnumerable<TEntity> Get<TEntity>() where TEntity : BaseEntity
        {
            return _context.Set<TEntity>();
        }

        public async Task<TEntity> Get<TEntity>(Guid id) where TEntity : BaseEntity
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(p => p.Id == id);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            entity.IsDeleted = true;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
