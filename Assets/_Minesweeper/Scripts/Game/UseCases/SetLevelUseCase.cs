using System;

namespace Game
{
	public class SetLevelUseCase
	{
		public event Action NewLevelSet;

		private readonly LevelService levelService;
		private readonly LevelConfigProvider levelConfigProvider;

		public SetLevelUseCase(LevelService levelService, LevelConfigProvider levelConfigProvider)
		{
			this.levelService = levelService;
			this.levelConfigProvider = levelConfigProvider;
		}

		public Level Execute(LevelConfig levelConfig)
		{
			var level = new Level(levelConfig);
			levelService.PopulateLevelGrid(level, levelConfig);
			levelService.UpdateLevel(level);
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