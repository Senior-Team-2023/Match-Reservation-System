namespace MatchReservationSystem.Ops
{
    public interface IBasicOps<T> where T : class
    {
        public T? GetbyId(int id);
        public Task<T?> GetbyIdAsync(int id);
        public IEnumerable<T> GetAll();
        public Task<IEnumerable<T>> GetAllAsync();
        public IQueryable<T> GetAllQueryable();
        public void Create(T entity);
        public void EditRecursive(T entity);
        public void EditNonRecursive(T entity);
        public void Delete(int id);
        public void SaveChanges();
    }
}
