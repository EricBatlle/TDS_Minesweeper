using Cysharp.Threading.Tasks;

namespace Game
{
	public class StartedState : IGameState
	{
		public GameState Id => GameState.Started;

		private readonly ChallengeCellService challengeCellService;

		public StartedState(ChallengeCellService challengeCellService)
		{
			this.challengeCellService = challengeCellService;
		}

		public UniTask Enter()
		{
			challengeCellService.ScheduleNextChallenge();
			return UniTask.CompletedTask;
		}

		public UniTask Exit()
		{
			return UniTask.CompletedTask;
		}
	}
}