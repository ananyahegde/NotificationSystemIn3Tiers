namespace DataAccessLayer.Interfaces
{
    internal interface IRepository<T, K> where T : class
    {
        public T Create(T item);
        public List<T>? ReadAll();
        public T? Read(K key);
        public T? Update(T item, K key);
        public T? Delete(K key);
    }
}
