using System.Collections.Generic;
using UnityEngine;

namespace Leaderboard
{
	public class LeaderboardService
	{
		private readonly ILeaderboardUsersRepository leaderboardUsersRepository;

		public LeaderboardService(ILeaderboardUsersRepository leaderboardUsersRepository)
		{
			this.leaderboardUsersRepository = leaderboardUsersRepository;
		}

		public void SaveLeaderboardEntry(UserLeaderboardSubmission userLeaderboardSubmission)
		{
			var user = leaderboardUsersRepository.Create(userLeaderboardSubmission.Name, userLeaderboardSubmission.Score);
			Debug.LogWarning("New Leaderboard entry: "+user);
			Debug.LogWarning("All Leaderboard entries: "+string.Join("\n",leaderboardUsersRepository.Get()));
		}

		public List<LeaderboardUser> GetAllLeaderboardEntries()
		{
			return leaderboardUsersRepository.Get();
		}
	}
}