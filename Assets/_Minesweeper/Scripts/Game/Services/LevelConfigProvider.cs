namespace Game
{
	public class LevelConfigProvider
	{
		private readonly ILevelConfigRepository levelConfigRepository;
		private readonly LevelConfigData initialLevelConfig;
		private readonly LevelDifficultyAdjusterService levelDifficultyAdjusterService;

		public LevelConfigProvider(ILevelConfigRepository levelConfigRepository, LevelConfigData initialLevelConfig, LevelDifficultyAdjusterService levelDifficultyAdjusterService)
		{
			this.levelConfigRepository = levelConfigRepository;
			this.initialLevelConfig = initialLevelConfig;
			this.levelDifficultyAdjusterService = levelDifficultyAdjusterService;
		}

		public LevelConfig GetFirst()
		{
			var levelConfig = new LevelConfig(initialLevelConfig);
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
			var newConfig = levelDifficultyAdjusterService.IncreaseDifficulty(currentConfig);
			levelConfigRepository.Update(newConfig);
			return newConfig;
		}
	}
}