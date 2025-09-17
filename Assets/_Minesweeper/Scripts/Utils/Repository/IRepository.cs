namespace Utils
{
	public interface IRepository<T>
	{
		public T Get();
		public T Create();
		public T Update(T data);
	}
}