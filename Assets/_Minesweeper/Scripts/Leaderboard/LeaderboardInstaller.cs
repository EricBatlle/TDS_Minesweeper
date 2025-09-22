using VContainer;
using VContainer.Unity;

namespace Leaderboard
{
	public class LeaderboardInstaller : IInstaller
	{
		public void Install(IContainerBuilder builder)
		{
			builder.Register<LeaderboardUsersRepository>(Lifetime.Singleton);
			builder.Register<LeaderboardService>(Lifetime.Singleton);
		}
	}
}