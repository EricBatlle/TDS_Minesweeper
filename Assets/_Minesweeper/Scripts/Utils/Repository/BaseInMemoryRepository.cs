namespace Utils
{
	// ToDo: Create base class for collections to avoid the ugliness like the one in UsersRepository
	public abstract class BaseInMemoryRepository<T> : IRepository<T> where T : new()
	{
		private T inMemoryData;
		
		public virtual T Get()
		{
			return inMemoryData ?? Create();
		}

		public virtual T Create()
		{
			inMemoryData = new T();
			return inMemoryData;
		}

		public virtual T Update(T data)
		{
			inMemoryData = data;
			return inMemoryData;
		}
	}
}