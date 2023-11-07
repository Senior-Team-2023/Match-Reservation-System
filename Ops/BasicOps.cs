using Microsoft.EntityFrameworkCore;

namespace MatchReservationSystem.Ops
{
    public abstract class BasicOps<T> : IBasicOps<T> where T : class
    {
        public BasicOps(DbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<T>();
        }
        public virtual T? GetbyId(int id)
        {
            return DbSet.Find(id);
        }
        public virtual async Task<T?> GetbyIdAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);
            return entity;
        }
        public virtual IEnumerable<T> GetAll()
        {
            return DbSet.ToList();
        }
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = await DbSet.ToListAsync();
            return entities;
        }
        public virtual async Task<List<T>> GetAllAsListAsync()
        {
            var entities = await DbSet.ToListAsync();
            return entities;
        }
        public virtual IQueryable<T> GetAllQueryable()
        {
            return DbSet.AsQueryable();
        }
        public virtual void Create(T entity)
        {
            DbSet.Add(entity);
        }
        public virtual void Edit(T entity)
        {
            EditRecursive(entity);
        }
        public virtual void EditRecursive(T entity)
        {
            var attach = DbSet.Update(entity);
            attach.State = EntityState.Modified;
        }
        public virtual void EditNonRecursive(T entity)
        {
            var entry = DbSet.Entry(entity);
            entry.State = EntityState.Modified;
        }
        public virtual void Delete(int id)
        {
            T? entity = GetbyId(id);
            if(entity != null)
            {
                DbSet.Remove(entity);
            }
        }
        public virtual bool Exists(int id)
        {
            return DbSet.Find(id) != null;
        }
        public virtual async Task<bool> ExistsAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);
            return entity != null;
        }
        public virtual void SaveChanges()
        {
            DbContext.SaveChanges();
        }
        public virtual async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        protected DbContext DbContext { get; set; }
        protected DbSet<T> DbSet { get; set; }
    }
}
