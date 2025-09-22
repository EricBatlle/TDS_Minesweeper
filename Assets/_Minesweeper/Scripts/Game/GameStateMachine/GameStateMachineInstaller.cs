using VContainer;
using VContainer.Unity;

namespace Game
{
	public class GameStateMachineInstaller : IInstaller
	{
		public void Install(IContainerBuilder builder)
		{
			builder.Register<DefaultState>(Lifetime.Singleton).As<IGameState>();
			builder.Register<InitializingState>(Lifetime.Singleton).As<IGameState>();
			builder.Register<StartedState>(Lifetime.Singleton).As<IGameState>();
			builder.Register<LoseState>(Lifetime.Singleton).As<IGameState>();
			builder.Register<WinState>(Lifetime.Singleton).As<IGameState>();

			builder.Register<GameStateMachine>(Lifetime.Singleton);
		}
	}
}