using Cysharp.Threading.Tasks;

namespace Game
{
	public class WinState : IGameState
	{
		public GameState Id => GameState.Win;

		private readonly UserAliveStopwatchRepository userAliveStopwatchRepository;
		private readonly ChallengeCellService challengeCellService;

		public WinState(UserAliveStopwatchRepository userAliveStopwatchRepository, ChallengeCellService challengeCellService)
		{
			this.userAliveStopwatchRepository = userAliveStopwatchRepository;
			this.challengeCellService = challengeCellService;
		}

		public UniTask Enter()
		{
			userAliveStopwatchRepository.Get()?.Stop();
			challengeCellService.PauseChallenge();
			return UniTask.CompletedTask;
		}

		public UniTask Exit()
		{
			return UniTask.CompletedTask;
		}
	}
}