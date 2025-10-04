namespace Game
{
	public interface IScoreRepository
	{
		int Get();
		int Update(int data);
		void Delete();
	}
}