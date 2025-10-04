using Cysharp.Threading.Tasks;

namespace Game
{
	public class WinState : IGameState
	{
		public GameState Id => GameState.Win;

		private readonly ChallengeCellService challengeCellService;

		public WinState(ChallengeCellService challengeCellService)
		{
			this.challengeCellService = challengeCellService;
		}

		public UniTask Enter()
		{
			challengeCellService.PauseChallenge();
			return UniTask.CompletedTask;
		}

		public UniTask Exit()
		{
			return UniTask.CompletedTask;
		}
	}
}