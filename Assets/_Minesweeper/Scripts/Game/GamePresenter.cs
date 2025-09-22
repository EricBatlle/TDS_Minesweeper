using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Game
{
	public class GamePresenter : IInitializable
	{
		private readonly InitializeGridUseCase initializeGridUseCase;
		private readonly TryFlagCellUseCase tryFlagCellUseCase;
		private readonly SelectCellUseCase selectCellUseCase;
		private readonly SetLevelUseCase setLevelUseCase;
		private readonly CellViewsRepository cellViewsRepository;
		
		private readonly LevelService levelService;
		private readonly CellService cellService;
		private readonly GameService gameService;

		private readonly GameEndFlow gameEndFlow;

		private readonly GameStateMachine gameStateMachine;

		public GamePresenter(
			LevelService levelService,
			GameEndFlow gameEndFlow,
			SetLevelUseCase setLevelUseCase,
			InitializeGridUseCase initializeGridUseCase,
			TryFlagCellUseCase tryFlagCellUseCase,
			GameStateMachine gameStateMachine,
			GameService gameService,
			CellViewsRepository cellViewsRepository, 
			SelectCellUseCase selectCellUseCase,
			CellService cellService)
		{
			this.levelService = levelService;
			this.gameEndFlow = gameEndFlow;
			this.setLevelUseCase = setLevelUseCase;
			this.initializeGridUseCase = initializeGridUseCase;
			this.tryFlagCellUseCase = tryFlagCellUseCase;
			this.gameStateMachine = gameStateMachine;
			this.gameService = gameService;
			this.cellViewsRepository = cellViewsRepository;
			this.selectCellUseCase = selectCellUseCase;
			this.cellService = cellService;
		}

		public void Initialize()
		{
			gameStateMachine.GameStateChanged += OnGameStateChanged;
			selectCellUseCase.CellsOpened += OnCellsOpened;
			tryFlagCellUseCase.CellFlagged += OnCellFlagged;
			tryFlagCellUseCase.CellUnflagged += OnCellUnflagged;

			gameService.CreateGame();
			setLevelUseCase.FirstLevel();
		}

		private async void OnGameStateChanged(GameState gameState)
		{
			switch (gameState)
			{
				case GameState.Default:
					break;
				case GameState.Initializing:
					var level = levelService.GetCurrent();
					initializeGridUseCase.Execute(level, level.Config);
					break;
				case GameState.Started:
					break;
				case GameState.Lose:
					await gameEndFlow.ExecuteFlow(GameEndReason.Lose);
					break;
				case GameState.Win:
					await gameEndFlow.ExecuteFlow(GameEndReason.Win);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(gameState), gameState, null);
			}
		}

		private void OnCellUnflagged(Cell cell)
		{
			UpdateCellView(cell);
		}

		private void OnCellFlagged(Cell cell)
		{
			UpdateCellView(cell);
		}

		private void OnCellsOpened(HashSet<Cell> cells)
		{
			foreach (var cell in cells)
			{
				UpdateCellView(cell);
			}
		}

		private void UpdateCellView(Cell cell)
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