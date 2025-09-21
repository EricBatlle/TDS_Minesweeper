using System;

namespace Game
{
	public class SetLevelUseCase
	{
		public event Action NewLevelSet;

		private readonly LevelService levelService;
		private readonly LevelRepository levelRepository;

		public SetLevelUseCase(LevelService levelService, LevelRepository levelRepository)
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
			NewLevelSet?.Invoke();
			return level;
		}
	}
}