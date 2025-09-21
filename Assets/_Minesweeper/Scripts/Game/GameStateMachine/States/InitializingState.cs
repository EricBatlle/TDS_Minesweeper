using Cysharp.Threading.Tasks;

namespace Game
{
	public class InitializingState : IGameState
	{
		public GameState Id => GameState.Initializing;

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