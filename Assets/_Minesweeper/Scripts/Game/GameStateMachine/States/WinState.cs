using Cysharp.Threading.Tasks;

namespace Game
{
	public class WinState : IGameState
	{
		public GameState Id => GameState.Win;

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