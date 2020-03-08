using Donations.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Donations.Data.Interfaces
{
    public interface IDonationsRepository
    {
        IEnumerable<TEntity> Get<TEntity>() where TEntity : BaseEntity;
        Task<TEntity> Get<TEntity>(Guid id) where TEntity : BaseEntity;
        Task Add<TEntity>(TEntity entity) where TEntity : BaseEntity;
        void Remove<TEntity>(TEntity entity) where TEntity : BaseEntity;
        Task SaveChanges();
    }
}
