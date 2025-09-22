namespace Game
{
	public class LevelConfigProvider
	{
		private readonly LevelConfigRepository levelConfigRepository;

		public LevelConfigProvider(LevelConfigRepository levelConfigRepository)
		{
			this.levelConfigRepository = levelConfigRepository;
		}

		public LevelConfig GetFirst()
		{
			var levelConfig = new LevelConfig();
			levelConfigRepository.Update(levelConfig);
			return levelConfig;
		}
		
		public LevelConfig GetCurrent()
		{
			return levelConfigRepository.Get();
		}
		
		public LevelConfig GetNext()
		{
			var currentConfig = levelConfigRepository.Get();
			var newConfig = new LevelConfig(currentConfig.RowsCount + 2, currentConfig.MinesCount + 2);
			levelConfigRepository.Update(newConfig);
			return newConfig;
		}
	}
}