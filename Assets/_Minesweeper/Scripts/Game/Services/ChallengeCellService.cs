using System;
using TimerModule;
using Utils;

namespace Game
{
	public class ChallengeCellService
	{
		public event Action<Cell> CellChallenged;
		public event Action<Cell> ChallengeCellFailed;
		public event Action<Cell> ChallengeCellSucceed;
		public event Action ChallengePaused;

		private readonly GameService gameService;
		private readonly LevelService levelService;
		private readonly IRandomProvider randomProvider;
		private readonly TimerService timerService;
		private readonly TimerRepository timerRepository;

		public ChallengeCellService(GameService gameService, LevelService levelService, IRandomProvider randomProvider, TimerService timerService, TimerRepository timerRepository)
		{
			this.gameService = gameService;
			this.levelService = levelService;
			this.randomProvider = randomProvider;
			this.timerService = timerService;
			this.timerRepository = timerRepository;

			timerService.TimerStateChanged += OnTimerStateChanged;
		}

		private void OnTimerStateChanged(Timer timer)
		{
			if (timer.Id == TimerIds.CompleteChallengeTimerId && timer.State == TimerState.Stopped)
			{
				TimeoutChallenge();
			}

			if (timer.Id == TimerIds.ChallengeCellTimerId && timer.State == TimerState.Stopped)
			{
				StartChallengeCell();
			}
		}
		
		public void ResolveChallengeFor(Cell cell)
		{
			if (!cell.IsChallenged)
			{
				return;
			}

			PauseCompleteChallengeTimer();
			cell.StopChallenge();
			var success = cell.HasBomb && cell.State == CellState.Flagged || (!cell.HasBomb && cell.State == CellState.Open);
			if (success)
			{
				ChallengeCellSucceed?.Invoke(cell);
				return;
			}

			gameService.SetGameFinalSelectedCell(cell);
			ChallengeCellFailed?.Invoke(cell);
		}

		public void TimeoutChallenge()
		{
			var cell = levelService.GetCurrent().ChallengedCell;
			gameService.SetGameFinalSelectedCell(cell);
			ChallengeCellFailed?.Invoke(cell);
		}

		public void StartChallengeCell()
		{
			var unopenCells = levelService.GetCurrent().CellsUnopen;
			var randomUnopenCell = randomProvider.PickRandomOrDefault(unopenCells);
			if (randomUnopenCell == null)
			{
				PauseChallenge();
				return;
			}

			randomUnopenCell.StartChallenge();

			PauseChallengeCellTimer();
			StartCompleteChallengeTimer();
			CellChallenged?.Invoke(randomUnopenCell);
		}

		public void ScheduleNextChallenge()
		{
			CreateChallengeTimers();
			PauseCompleteChallengeTimer();
			StartChallengeCellTimer();
		}

		public void PauseChallenge()
		{
			PauseChallengeCellTimer();
			PauseCompleteChallengeTimer();
			ChallengePaused?.Invoke();
		}

		private void CreateChallengeTimers()
		{
			if (timerRepository.Get(TimerIds.CompleteChallengeTimerId) == null || timerRepository.Get(TimerIds.ChallengeCellTimerId) == null)
			{
				var level = levelService.GetCurrent();
				timerService.CreateTimer(TimerIds.ChallengeCellTimerId, level.Config.ChallengeCellFrequencyInSeconds);
				timerService.CreateTimer(TimerIds.CompleteChallengeTimerId, level.Config.TimeToCompleteChallengeInSeconds);
			}
		}

		private void PauseCompleteChallengeTimer()
		{
			var completeChallengeTimer = timerRepository.Get(TimerIds.CompleteChallengeTimerId);
			timerService.PauseTimer(completeChallengeTimer);
		}

		private void StartCompleteChallengeTimer()
		{
			var completeChallengeTimer = timerRepository.Get(TimerIds.CompleteChallengeTimerId);
			timerService.StartTimer(completeChallengeTimer);
		}
		
		private void StartChallengeCellTimer()
		{
			var challengeCellTimer = timerRepository.Get(TimerIds.ChallengeCellTimerId);
			timerService.StartTimer(challengeCellTimer);
		}

		private void PauseChallengeCellTimer()
		{
			var challengeCellTimer = timerRepository.Get(TimerIds.ChallengeCellTimerId);
			timerService.PauseTimer(challengeCellTimer);
		}
	}
}