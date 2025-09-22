namespace Leaderboard
{
	public class LeaderboardUserLabelViewData
	{
		public string UserName { get; set; }
		public float UserScore { get; set; }

		public LeaderboardUserLabelViewData(string userName, float userScore)
		{
			UserName = userName;
			UserScore = userScore;
		}
	}
}