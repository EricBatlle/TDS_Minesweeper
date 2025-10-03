namespace Game
{
	public class RevealAllLevelBombsUseCase
	{
		private readonly LevelService levelService;
		private readonly GameService gameService;
		private readonly ICellViewsRepository cellViewsRepository;

		public RevealAllLevelBombsUseCase(LevelService levelService, GameService gameService, ICellViewsRepository cellViewsRepository)
		{
			this.levelService = levelService;
			this.gameService = gameService;
			this.cellViewsRepository = cellViewsRepository;
		}

		public void Execute()
		{
			var level = levelService.GetCurrent();
			var selectedBomb = gameService.GetGameFinalSelectedCell();

			foreach (var cell in level.CellsWithBomb)
			{
				var cellView = cellViewsRepository.Get(cell);
				cellView?.RevealBomb(cell == selectedBomb);
			}
		}
	}
}