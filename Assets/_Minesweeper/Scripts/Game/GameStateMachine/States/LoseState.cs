using Cysharp.Threading.Tasks;

namespace Game
{
	public class LoseState : IGameState
	{
		public GameState Id => GameState.Lose;

		private readonly UserAliveStopwatchRepository userAliveStopwatchRepository;
		private readonly ChallengeCellService challengeCellService;

		public LoseState(UserAliveStopwatchRepository userAliveStopwatchRepository, ChallengeCellService challengeCellService)
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
			userAliveStopwatchRepository.Delete();
			return UniTask.CompletedTask;
		}
	}
}