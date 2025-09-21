using System;

namespace Game
{
	public class SetLevelUseCase
	{
		public event Action NewLevelSet;

		private readonly LevelService levelService;
		private readonly LevelRepository levelRepository;
		private readonly LevelConfigRepository levelConfigRepository;

		public SetLevelUseCase(LevelService levelService, LevelRepository levelRepository, LevelConfigRepository levelConfigRepository)
		{
			this.levelService = levelService;
			this.levelRepository = levelRepository;
			this.levelConfigRepository = levelConfigRepository;
		}

		public Level Execute(LevelConfig levelConfig)
		{
			var level = new Level(levelConfig);
			levelService.PopulateLevelGrid(level, levelConfig);
			levelRepository.Update(level);
			NewLevelSet?.Invoke();
			return level;
		}

		public Level Execute()
		{
			var levelConfig = levelConfigRepository.Get();
			return Execute(levelConfig);
		}
	}
}