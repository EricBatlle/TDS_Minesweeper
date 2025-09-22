using System.Collections.Generic;
using UnityEngine;

namespace Game
{
	// ToDo: Move leaderboard data to it's own system package
	public class LeaderboardService
	{
		private readonly UsersRepository usersRepository;

		public LeaderboardService(UsersRepository usersRepository)
		{
			this.usersRepository = usersRepository;
		}

		public void SaveLeaderboardEntry(UserLeaderboardSubmission userLeaderboardSubmission)
		{
			var user = usersRepository.Create(userLeaderboardSubmission.Name, userLeaderboardSubmission.Score);
			Debug.LogWarning("New Leaderboard entry: "+user);
			Debug.LogWarning("All Leaderboard entries: "+string.Join("\n",usersRepository.Get()));
		}

		public List<User> GetAllLeaderboardEntries()
		{
			return usersRepository.Get();
		}
	}
}