using Cysharp.Threading.Tasks;

namespace Game
{
	public class LoseState : IGameState
	{
		public GameState Id => GameState.Lose;

		public UniTask Enter()
		{
			return UniTask.CompletedTask;
		}

		public UniTask Exit()
		{
			return UniTask.CompletedTask;
		}
	}
}