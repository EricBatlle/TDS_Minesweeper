namespace Utils
{
	public abstract class BaseInMemoryRepository<T> : IRepository<T> where T : new()
	{
		private T inMemoryData;
		
		public virtual T Get()
		{
			return inMemoryData ?? Create();
		}

		public virtual T Create()
		{
			return new T();
		}

		public virtual T Update(T data)
		{
			inMemoryData = data;
			return inMemoryData;
		}
	}
}