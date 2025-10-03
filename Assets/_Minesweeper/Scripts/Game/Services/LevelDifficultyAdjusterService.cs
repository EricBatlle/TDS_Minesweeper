namespace Game
{
	public class LevelDifficultyAdjusterService
	{
		public LevelConfig IncreaseDifficulty(LevelConfig levelConfig)
		{
			return new LevelConfig(levelConfig.MinesCount + 2, levelConfig.RowsCount + 2);
		}
	}
}