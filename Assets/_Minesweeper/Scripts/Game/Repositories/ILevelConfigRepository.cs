namespace Game
{
	public interface ILevelConfigRepository
	{
		LevelConfig Get();
		LevelConfig Update(LevelConfig data);
	}
}