namespace Game
{
	public class LevelDifficultyAdjusterService
	{
		public LevelConfig IncreaseDifficulty(LevelConfig levelConfig)
		{
			return new LevelConfig(levelConfig.RowsCount + 2, levelConfig.MinesCount + 2);;
		}
	}
}