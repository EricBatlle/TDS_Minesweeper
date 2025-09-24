using Cysharp.Threading.Tasks;
using VContainer.Unity;

namespace Leaderboard
{
	public class LeaderboardViewPresenter : IInitializable
	{
		private readonly LeaderboardView view;
		private readonly NavigationSystem.NavigationSystem navigationSystem;

		public LeaderboardViewPresenter(LeaderboardView view, NavigationSystem.NavigationSystem navigationSystem, LeaderboardService leaderboardService)
		{
			this.view = view;
			this.navigationSystem = navigationSystem;
		}

		public void Initialize()
		{
			view.PlayAgainClicked += OnPlayAgainClicked;
		}

		private void OnPlayAgainClicked()
		{
			navigationSystem.Close(view).Forget();
		}
	}
}