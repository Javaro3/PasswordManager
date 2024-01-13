namespace Repository.Repositories {
    public interface IRepository<TEntity> {
        public void Add(TEntity entity);
        public void Remove(TEntity entity);
        public void Update(TEntity entity);
        public TEntity GetById(int id);
        public IEnumerable<TEntity> GetAll();
    }
}
