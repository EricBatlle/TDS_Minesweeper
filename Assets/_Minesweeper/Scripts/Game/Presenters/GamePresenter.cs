using System.Collections.Generic;
using VContainer.Unity;

namespace Game
{
	public class GamePresenter : IInitializable
	{
		private readonly SelectCellUseCase selectCellUseCase;
		private readonly CellViewsRepository cellViewsRepository;
		private readonly CellService cellService;
		private readonly GameService gameService;
		private readonly GameStateMachine gameStateMachine;

		public GamePresenter(
			GameStateMachine gameStateMachine,
			GameService gameService,
			CellViewsRepository cellViewsRepository, 
			SelectCellUseCase selectCellUseCase,
			CellService cellService)
		{
			this.gameStateMachine = gameStateMachine;
			this.gameService = gameService;
			this.cellViewsRepository = cellViewsRepository;
			this.selectCellUseCase = selectCellUseCase;
			this.cellService = cellService;
		}

		public void Initialize()
		{
			selectCellUseCase.CellsOpened += OnCellsOpened;

			gameService.CreateGame();
			gameStateMachine.Initialize();
		}

		private void OnCellsOpened(HashSet<Cell> cells)
		{
			foreach (var cell in cells)
			{
				var cellView = cellViewsRepository.Get(cell);
				cellView?.UpdateView(new CellViewData
				{
					Cell = cell,
					CanShowBombsAround = cellService.CanCellShowBombsAround(cell),
					BombsAroundCount = cellService.GetNeighborsWithBombCount(cell)
				});
			}
		}

		
	}
}