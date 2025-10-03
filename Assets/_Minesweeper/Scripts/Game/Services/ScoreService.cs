namespace Game
{
	public class ScoreService
	{
		private readonly UserAliveStopwatchService userAliveStopwatchService;

		public ScoreService(UserAliveStopwatchService userAliveStopwatchService)
		{
			this.userAliveStopwatchService = userAliveStopwatchService;
		}

		public int GetScore()
		{
			return userAliveStopwatchService.GetElapsedMilliseconds();
		}
	}
}