using Cysharp.Threading.Tasks;

namespace Game
{
	public class WinState : IGameState
	{
		public GameState Id => GameState.Win;

		private readonly UserAliveStopwatchService userAliveStopwatchService;
		private readonly ChallengeCellService challengeCellService;

		public WinState(UserAliveStopwatchService userAliveStopwatchService, ChallengeCellService challengeCellService)
		{
			this.userAliveStopwatchService = userAliveStopwatchService;
			this.challengeCellService = challengeCellService;
		}

		public UniTask Enter()
		{
			userAliveStopwatchService.Stop();
			challengeCellService.PauseChallenge();
			return UniTask.CompletedTask;
		}

		public UniTask Exit()
		{
			return UniTask.CompletedTask;
		}
	}
}