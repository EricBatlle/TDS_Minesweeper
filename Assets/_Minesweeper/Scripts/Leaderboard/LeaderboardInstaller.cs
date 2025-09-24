using VContainer;
using VContainer.Unity;

namespace Leaderboard
{
	public class LeaderboardInstaller : IInstaller
	{
		public void Install(IContainerBuilder builder)
		{
			builder.Register<PlayerPrefsLeaderboardUsersRepository>(Lifetime.Singleton).As<ILeaderboardUsersRepository>();
			builder.Register<LeaderboardService>(Lifetime.Singleton);
		}
	}
}