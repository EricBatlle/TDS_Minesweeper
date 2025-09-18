using Cysharp.Threading.Tasks;

namespace Game
{
	public class StartedState : IGameState
	{
		public GameState Id => GameState.Started;

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