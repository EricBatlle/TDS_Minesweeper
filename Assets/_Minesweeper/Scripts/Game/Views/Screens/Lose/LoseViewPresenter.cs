using Cysharp.Threading.Tasks;
using Leaderboard;
using VContainer.Unity;

namespace Game
{
	public class LoseViewPresenter : IInitializable
	{
		private readonly LoseView view;
		private readonly NavigationSystem.NavigationSystem navigationSystem;
		private readonly LeaderboardService leaderboardService;

		public LoseViewPresenter(LoseView view, NavigationSystem.NavigationSystem navigationSystem, LeaderboardService leaderboardService)
		{
			this.view = view;
			this.navigationSystem = navigationSystem;
			this.leaderboardService = leaderboardService;
		}

		public void Initialize()
		{
			view.UserLeaderboardSubmissionConfirmed += OnUserLeaderboardSubmissionConfirmed;
		}

		private void OnUserLeaderboardSubmissionConfirmed(UserLeaderboardSubmission userLeaderboardSubmission)
		{
			leaderboardService.SaveLeaderboardEntry(userLeaderboardSubmission);
			navigationSystem.Close(view).Forget();
		}
	}
}