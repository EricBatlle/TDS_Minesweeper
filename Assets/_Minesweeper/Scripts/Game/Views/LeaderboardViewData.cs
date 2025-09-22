using System.Collections.Generic;
using NavigationSystem;

namespace Game
{
	public class LeaderboardViewData : IViewData
	{
		public List<User> UsersToShow { get; }

		public LeaderboardViewData(List<User> usersToShow)
		{
			UsersToShow = usersToShow;
		}
	}
}