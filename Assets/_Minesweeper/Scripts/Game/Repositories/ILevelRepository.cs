namespace Game
{
	public interface ILevelRepository
	{
		Level Get();
		Level Update(Level data);
	}
}