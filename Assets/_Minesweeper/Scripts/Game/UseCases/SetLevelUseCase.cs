using System;

namespace Game
{
	public class SetLevelUseCase
	{
		public event Action NewLevelSet;

		private readonly LevelService levelService;
		private readonly LevelRepository levelRepository;
		private readonly LevelConfigProvider levelConfigProvider;

		public SetLevelUseCase(LevelService levelService, LevelRepository levelRepository, LevelConfigProvider levelConfigProvider)
		{
			this.levelService = levelService;
			this.levelRepository = levelRepository;
			this.levelConfigProvider = levelConfigProvider;
		}

		public Level Execute(LevelConfig levelConfig)
		{
			var level = new Level(levelConfig);
			levelService.PopulateLevelGrid(level, levelConfig);
			levelRepository.Update(level);
			NewLevelSet?.Invoke();
			return level;
		}
		
		public Level NextLevel()
		{
			var levelConfig = levelConfigProvider.GetNext();
			return Execute(levelConfig);
		}

		public Level FirstLevel()
		{
			var levelConfig = levelConfigProvider.GetFirst();
			return Execute(levelConfig);
		}
	}
}