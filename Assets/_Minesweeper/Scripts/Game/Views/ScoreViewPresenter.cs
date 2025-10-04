using VContainer.Unity;

namespace Game
{
	public class ScoreViewPresenter : IInitializable
	{
		private readonly ScoreView scoreView;
		private readonly ScoreService scoreService;

		public ScoreViewPresenter(ScoreView scoreView, ScoreService scoreService)
		{
			this.scoreView = scoreView;
			this.scoreService = scoreService;
		}

		public void Initialize()
		{
			scoreService.ScoreUpdated += OnScoreUpdated;
			
			scoreView.UpdateView(scoreService.GetScore());
		}

		private void OnScoreUpdated()
		{
			scoreView.UpdateView(scoreService.GetScore());
		}
	}
}