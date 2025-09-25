using Cysharp.Threading.Tasks;

namespace Game
{
	public class StartedState : IGameState
	{
		public GameState Id => GameState.Started;

		private readonly UserAliveStopwatchRepository userAliveStopwatchRepository;
		private readonly ChallengeCellService challengeCellService;

		public StartedState(UserAliveStopwatchRepository userAliveStopwatchRepository, ChallengeCellService challengeCellService)
		{
			this.userAliveStopwatchRepository = userAliveStopwatchRepository;
			this.challengeCellService = challengeCellService;
		}

		public UniTask Enter()
		{
			var userAliveStopwatch = userAliveStopwatchRepository.Get() ?? userAliveStopwatchRepository.Create();
			userAliveStopwatch.Start();
			challengeCellService.ScheduleNextChallenge();
			return UniTask.CompletedTask;
		}

		public UniTask Exit()
		{
			return UniTask.CompletedTask;
		}
	}
}