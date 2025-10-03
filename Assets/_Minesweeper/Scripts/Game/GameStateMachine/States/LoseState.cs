using Cysharp.Threading.Tasks;

namespace Game
{
	public class LoseState : IGameState
	{
		public GameState Id => GameState.Lose;

		private readonly UserAliveStopwatchService userAliveStopwatchService;
		private readonly ChallengeCellService challengeCellService;

		public LoseState(UserAliveStopwatchService userAliveStopwatchService, ChallengeCellService challengeCellService)
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
			userAliveStopwatchService.Delete();
			return UniTask.CompletedTask;
		}
	}
}