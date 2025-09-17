namespace Utils
{
	public abstract class BaseInMemoryRepository<T> : IRepository<T> where T : new()
	{
		private T inMemoryData;
		
		public T Get()
		{
			return inMemoryData ?? Create();
		}

		public T Create()
		{
			return new T();
		}

		public T Update(T data)
		{
			inMemoryData = data;
			return inMemoryData;
		}
	}
}