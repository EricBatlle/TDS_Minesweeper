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
		public event Action CompleteChallengePaused;

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
		}

		public bool IsCellChallenged(Cell cell)
		{
			return cell.IsChallenged;
		}

		public void CheckChallenge(Cell cell)
		{
			cell.StopChallenge();
			if (cell.HasBomb && cell.State == CellState.Flagged || (!cell.HasBomb && cell.State == CellState.Open))
			{
				ChallengeCellSucceed?.Invoke(cell);
				return;
			}

			gameService.SetGameFinalSelectedCell(cell);
			ChallengeCellFailed?.Invoke(cell);
		}

		public void ForceChallengeCellFailed()
		{
			var cell = levelService.GetCurrent().ChallengedCell;
			gameService.SetGameFinalSelectedCell(cell);
			ChallengeCellFailed?.Invoke(cell);
		}

		public void ChallengeCell()
		{
			var unopenCells = levelService.GetCurrent().CellsUnopen;
			var randomUnopenCell = randomProvider.PickRandomOrDefault(unopenCells);
			randomUnopenCell?.StartChallenge();

			PauseChallengeCellTimer();
			StartCompleteChallengeTimer();
			CellChallenged?.Invoke(randomUnopenCell);
		}

		public void StartChallengeWaiting()
		{
			CreateChallengeTimers();
			PauseCompleteChallengeTimer();
			StartChallengeCellTimer();
		}

		public void PauseChallenge()
		{
			PauseChallengeCellTimer();
			PauseCompleteChallengeTimer();
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
			CompleteChallengePaused?.Invoke();
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