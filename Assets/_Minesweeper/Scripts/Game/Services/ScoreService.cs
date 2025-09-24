namespace Game
{
	public class ScoreService
	{
		private readonly UserAliveStopwatchRepository userAliveStopwatchRepository;

		public ScoreService(UserAliveStopwatchRepository userAliveStopwatchRepository)
		{
			this.userAliveStopwatchRepository = userAliveStopwatchRepository;
		}

		public int GetScore()
		{
			var userAliveStopwatch = userAliveStopwatchRepository.Get();
			if (userAliveStopwatch != null)
			{
				return (int)userAliveStopwatch.ElapsedMilliseconds;
			}

			return 0;
		}
	}
}