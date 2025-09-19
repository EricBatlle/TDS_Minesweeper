namespace Game
{
	public class RevealAllLevelBombsUseCase
	{
		private readonly LevelRepository levelRepository;
		private readonly GameService gameService;
		private readonly CellViewsRepository cellViewsRepository;

		public RevealAllLevelBombsUseCase(LevelRepository levelRepository, GameService gameService, CellViewsRepository cellViewsRepository)
		{
			this.levelRepository = levelRepository;
			this.gameService = gameService;
			this.cellViewsRepository = cellViewsRepository;
		}

		public void Execute()
		{
			var level = levelRepository.Get();
			var selectedBomb = gameService.GetGameFinalSelectedCell();

			foreach (var cell in level.CellsWithBomb)
			{
				var cellView = cellViewsRepository.Get(cell);
				cellView?.RevealBomb(cell == selectedBomb);
			}
		}
	}
}