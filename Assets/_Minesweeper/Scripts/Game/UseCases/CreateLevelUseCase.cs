namespace Game
{
	public class CreateLevelUseCase
	{
		private readonly LevelService levelService;
		private readonly LevelRepository levelRepository;

		public CreateLevelUseCase(LevelService levelService, LevelRepository levelRepository)
		{
			this.levelService = levelService;
			this.levelRepository = levelRepository;
		}
		
		public Level Execute(LevelConfig levelConfig)
		{
			// Create Level
			var level = new Level(levelConfig);
			levelService.PopulateLevelGrid(level, levelConfig);
			levelRepository.Update(level);
			return level;
		}
	}
}