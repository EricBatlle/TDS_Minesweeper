using System.Collections.Generic;
using System.Linq;
using Minesweeper;
using Utils;

namespace Leaderboard
{
	public class PlayerPrefsLeaderboardUsersRepository : BasePlayerPrefsRepository<List<LeaderboardUser>>, ILeaderboardUsersRepository
	{
		protected override string PlayerPrefsKey => "LeaderboardUsers";
		
		protected override string Serialize(List<LeaderboardUser> data) => JsonUtilityHelper.ToJson(data);
		protected override List<LeaderboardUser> Deserialize(string json) => JsonUtilityHelper.FromJson<LeaderboardUser>(json);

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