using NavigationSystem;

namespace Game
{
	public class LoseViewData : IViewData
	{
		public float Score { get; set; }

		public LoseViewData(float score)
		{
			Score = score;
		}
	}
}