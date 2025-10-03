using Cysharp.Threading.Tasks;

namespace Game
{
	public class StartedState : IGameState
	{
		public GameState Id => GameState.Started;

		private readonly UserAliveStopwatchService userAliveStopwatchService;
		private readonly ChallengeCellService challengeCellService;

		public StartedState(UserAliveStopwatchService userAliveStopwatchService, ChallengeCellService challengeCellService)
		{
			this.userAliveStopwatchService = userAliveStopwatchService;
			this.challengeCellService = challengeCellService;
		}

		public UniTask Enter()
		{
			userAliveStopwatchService.Start();
			challengeCellService.ScheduleNextChallenge();
			return UniTask.CompletedTask;
		}

		public UniTask Exit()
		{
			return UniTask.CompletedTask;
		}
	}
}