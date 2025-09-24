using VContainer.Unity;

namespace Game
{
	public class StopwatchViewPresenter : ITickable
	{
		private readonly StopwatchView stopwatchView;
		private readonly UserAliveStopwatchRepository userAliveStopwatchRepository;

		public StopwatchViewPresenter(StopwatchView stopwatchView, UserAliveStopwatchRepository userAliveStopwatchRepository)
		{
			this.stopwatchView = stopwatchView;
			this.userAliveStopwatchRepository = userAliveStopwatchRepository;
		}

		public void Tick()
		{
			var elapsedMilliseconds = userAliveStopwatchRepository.Get()?.ElapsedMilliseconds;
			if (elapsedMilliseconds != null)
			{
				stopwatchView.UpdateView((int)elapsedMilliseconds);
			}
		}
	}
}