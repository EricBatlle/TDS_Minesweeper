using System.Collections.Generic;
using NavigationSystem;

namespace Leaderboard
{
	public class LeaderboardViewData : IViewData
	{
		public List<LeaderboardUser> UsersToShow { get; }

		public LeaderboardViewData(List<LeaderboardUser> usersToShow)
		{
			UsersToShow = usersToShow;
		}
	}
}