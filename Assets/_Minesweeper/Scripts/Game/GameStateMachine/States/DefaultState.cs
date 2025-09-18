using Cysharp.Threading.Tasks;

namespace Game
{
	public class DefaultState : IGameState
	{
		public GameState Id => GameState.Default;

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