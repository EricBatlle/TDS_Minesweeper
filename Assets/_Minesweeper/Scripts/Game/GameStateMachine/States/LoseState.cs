using Cysharp.Threading.Tasks;

namespace Game
{
	public class LoseState : IGameState
	{
		public GameState Id => GameState.Lose;

		private readonly ScoreService scoreService;
		private readonly ChallengeCellService challengeCellService;

		public LoseState(ScoreService scoreService, ChallengeCellService challengeCellService)
		{
			this.scoreService = scoreService;
			this.challengeCellService = challengeCellService;
		}

		public UniTask Enter()
		{
			challengeCellService.PauseChallenge();
			return UniTask.CompletedTask;
		}

		public UniTask Exit()
		{
			scoreService.ResetScore();
			return UniTask.CompletedTask;
		}
	}
}