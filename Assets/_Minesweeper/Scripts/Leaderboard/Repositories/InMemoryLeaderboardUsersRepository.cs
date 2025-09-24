using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Utils;

namespace Leaderboard
{
	public class InMemoryLeaderboardUsersRepository : BaseInMemoryRepository<List<LeaderboardUser>>, ILeaderboardUsersRepository
	{
		public LeaderboardUser Create(string name, float score)
		{
			var allUsers = Get();
			var lastId = 0;
			if (allUsers.Any())
			{
				lastId = allUsers.OrderByDescending(user => user.Id).First().Id;
			}
			var newUser = new LeaderboardUser(lastId + 1, name, score);
			Update(newUser);
			return newUser;
		}

		[CanBeNull]
		public LeaderboardUser Get(LeaderboardUser user)
		{
			return Get().FirstOrDefault(u => u.Id == user.Id);
		}
		
		public void Update(LeaderboardUser user)
		{
			var inMemoryData = Get();
			inMemoryData.Add(user);
			Update(inMemoryData);
		}
	}
}