using Cysharp.Threading.Tasks;

namespace Game
{
	public interface IGameState
	{
		GameState Id { get; }
		UniTask Enter();
		UniTask Exit();
	}
}