using System.Collections.Generic;

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
			leaderboardUsersRepository.Create(userLeaderboardSubmission.Name, userLeaderboardSubmission.Score);
		}

		public List<LeaderboardUser> GetAllLeaderboardEntries()
		{
			return leaderboardUsersRepository.Get();
		}
	}
}