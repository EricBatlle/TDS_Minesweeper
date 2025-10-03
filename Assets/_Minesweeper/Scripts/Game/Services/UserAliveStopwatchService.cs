namespace Game
{
	public class UserAliveStopwatchService
	{
		private readonly IUserAliveStopwatchRepository userAliveStopwatchRepository;

		public UserAliveStopwatchService(IUserAliveStopwatchRepository userAliveStopwatchRepository)
		{
			this.userAliveStopwatchRepository = userAliveStopwatchRepository;
		}

		public int GetElapsedMilliseconds()
		{
			var userAliveStopwatch = userAliveStopwatchRepository.Get();
			if (userAliveStopwatch != null)
			{
				return (int)userAliveStopwatch.ElapsedMilliseconds;
			}

			return 0;
		}

		public void Stop()
		{
			userAliveStopwatchRepository.Get()?.Stop();
		}
		
		public void Delete()
		{
			userAliveStopwatchRepository.Delete();
		}

		public void Start()
		{
			var userAliveStopwatch = userAliveStopwatchRepository.Get() ?? userAliveStopwatchRepository.Create();
			userAliveStopwatch.Start();
		}
	}
}