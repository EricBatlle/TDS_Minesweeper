using System.Collections.Generic;

namespace Leaderboard
{
	public interface ILeaderboardUsersRepository
	{
		LeaderboardUser Create(string name, float score);
		LeaderboardUser Get(LeaderboardUser user);
		public List<LeaderboardUser> Get();
		void Update(LeaderboardUser user);
	}
}