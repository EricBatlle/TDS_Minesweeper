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
			return (int)userAliveStopwatchRepository.Get().ElapsedMilliseconds;
		}
	}
}