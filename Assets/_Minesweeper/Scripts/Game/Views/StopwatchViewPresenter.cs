using VContainer.Unity;

namespace Game
{
	public class StopwatchViewPresenter : ITickable
	{
		private readonly StopwatchView stopwatchView;
		private readonly UserAliveStopwatchService userAliveStopwatchService;

		public StopwatchViewPresenter(StopwatchView stopwatchView, UserAliveStopwatchService userAliveStopwatchService)
		{
			this.stopwatchView = stopwatchView;
			this.userAliveStopwatchService = userAliveStopwatchService;
		}

		public void Tick()
		{
			stopwatchView.UpdateView(userAliveStopwatchService.GetElapsedMilliseconds());
		}
	}
}