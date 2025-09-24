namespace Leaderboard
{
	public class UserLeaderboardSubmission
	{
		public string Name { get; }
		public float Score { get; }

		public UserLeaderboardSubmission(string name, float score)
		{
			Name = name;
			Score = score;
		}
	}
}