using Cysharp.Threading.Tasks;
using NavigationSystem;

namespace Game
{
	public class LoseState : IGameState
	{
		public GameState Id => GameState.Lose;

		private readonly RevealAllLevelBombsUseCase revealAllLevelBombsUseCase;
		private readonly NavigationSystem.NavigationSystem navigationSystem;

		public LoseState(RevealAllLevelBombsUseCase revealAllLevelBombsUseCase, NavigationSystem.NavigationSystem navigationSystem)
		{
			this.revealAllLevelBombsUseCase = revealAllLevelBombsUseCase;
			this.navigationSystem = navigationSystem;
		}

		public UniTask Enter()
		{
			// reveal all bombs in board
			revealAllLevelBombsUseCase.Execute();
			navigationSystem.Open(ViewType.Lose, new LoseViewData(10));
			return UniTask.CompletedTask;
		}

		public UniTask Exit()
		{
			return UniTask.CompletedTask;
		}
	}
}