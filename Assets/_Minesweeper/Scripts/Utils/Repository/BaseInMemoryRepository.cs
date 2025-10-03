namespace Utils
{
	public abstract class BaseInMemoryRepository<T> : IRepository<T> where T : new()
	{
		protected T InMemoryData;
		
		// ToDo: This should return null if non existent
		public virtual T Get()
		{
			return InMemoryData ?? Create();
		}

		public virtual T Create()
		{
			InMemoryData = new T();
			return InMemoryData;
		}

		public virtual T Update(T data)
		{
			InMemoryData = data;
			return InMemoryData;
		}

		public virtual void Delete()
		{
			InMemoryData = default;
		}
	}
}