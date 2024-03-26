namespace SellIt.Core.Repository
{
    using System;
    using System.Linq;

    using SellIt.Infrastructure.Data;
    using Microsoft.EntityFrameworkCore;
    using System.Threading.Tasks;


    public class EfRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {

        public EfRepository(ApplicationDbContext context)
        {
            this.Context = context ?? throw new ArgumentNullException(nameof(context));
            this.DbSet = this.Context.Set<TEntity>();
        }

        protected DbSet<TEntity> DbSet { get; set; }

        protected ApplicationDbContext Context { get; set; }

        public virtual IQueryable<TEntity> All()
        {
            return this.DbSet;
        }

        public virtual IQueryable<TEntity> AllAsNoTracking()
        {
            return this.DbSet.AsNoTracking();
        }

        public virtual Task AddAsync(TEntity entity)
        {
            return this.DbSet.AddAsync(entity).AsTask();
        }

        public virtual void Update(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            this.DbSet.Remove(entity);
        }

        public Task<int> SaveChangesAsync()
        {
            return this.Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Context?.Dispose();
            }
        }

    }
}
