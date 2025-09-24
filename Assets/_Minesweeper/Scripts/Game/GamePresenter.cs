using System;
using System.Collections.Generic;
using TimerModule;
using UnityEngine;
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
		private readonly TimerService timerService;
		private readonly ChallengeCellService challengeCellService;

		private readonly GameEndFlow gameEndFlow;

		private readonly GameStateMachine gameStateMachine;

		public GamePresenter(
			ChallengeCellService challengeCellService,
			TimerService timerService,
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
			this.challengeCellService = challengeCellService;
			this.timerService = timerService;
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
			timerService.TimerStateChanged += OnTimerStateChanged;
			gameStateMachine.GameStateChanged += OnGameStateChanged;
			selectCellUseCase.CellsOpened += OnCellsOpened;
			tryFlagCellUseCase.CellFlagged += OnCellFlagged;
			tryFlagCellUseCase.CellUnflagged += OnCellUnflagged;
			challengeCellService.CellChallenged += OnCellChallenged;
			challengeCellService.ChallengeCellSucceed += OnCellChallengedSucceed;
			challengeCellService.ChallengeCellFailed += OnChallengeCellFailed;
			challengeCellService.CompleteChallengePaused += OnCompleteChallengePaused;

			gameService.CreateGame();
			setLevelUseCase.FirstLevel();
		}

		private void OnCompleteChallengePaused()
		{
			var challengedCell = levelService.GetCurrent().ChallengedCell;
			if (challengedCell == null)
			{
				return;
			}

			var cellView = cellViewsRepository.Get(challengedCell);
			cellView?.StopBlinking();
		}

		private void OnChallengeCellFailed(Cell cell)
		{
			var cellView = cellViewsRepository.Get(cell);
			cellView?.StopBlinking();
		}

		private void OnCellChallengedSucceed(Cell cell)
		{
			var cellView = cellViewsRepository.Get(cell);
			cellView?.StopBlinking();
			challengeCellService.StartChallengeWaiting();
		}

		// ToDo: Is this state for Game, or for Level?
		private async void OnGameStateChanged(GameState gameState)
		{
			var level = levelService.GetCurrent();

			switch (gameState)
			{
				case GameState.Default:
					break;
				case GameState.Initializing:
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
		
		private void OnTimerStateChanged(Timer timer)
		{
			if (timer.Id == TimerIds.CompleteChallengeTimerId && timer.State == TimerState.Stopped)
			{
				challengeCellService.ForceChallengeCellFailed();
				
			}
			if (timer.Id == TimerIds.ChallengeCellTimerId && timer.State == TimerState.Stopped)
			{
				challengeCellService.ChallengeCell();
				Debug.LogWarning("ChallengeTriggered");
			}
		}


		private void OnCellUnflagged(Cell cell)
		{
			UpdateCellView(cell);
		}

		private void OnCellFlagged(Cell cell)
		{
			if (challengeCellService.IsCellChallenged(cell))
			{
				challengeCellService.CheckChallenge(cell);
			}
			UpdateCellView(cell);
		}

		private void OnCellsOpened(HashSet<Cell> cells)
		{
			foreach (var cell in cells)
			{
				if (challengeCellService.IsCellChallenged(cell))
				{
					challengeCellService.CheckChallenge(cell);
				}
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

		private void OnCellChallenged(Cell cell)
		{
			var cellView = cellViewsRepository.Get(cell);
			cellView?.StartBlinking();
		}
	}
}