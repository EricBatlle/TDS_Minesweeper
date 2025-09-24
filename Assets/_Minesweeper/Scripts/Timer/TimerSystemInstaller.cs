using VContainer;
using VContainer.Unity;

namespace TimerModule
{
	public class TimerSystemInstaller : IInstaller
	{
		public void Install(IContainerBuilder builder)
		{
			builder.Register<TimerSystem>(Lifetime.Singleton).As<ITickable>();
			builder.Register<TimerRepository>(Lifetime.Singleton);
			builder.Register<TimerService>(Lifetime.Singleton);
			builder.Register<DateTimeProvider>(Lifetime.Singleton).As<IDateTimeProvider>();
		}
	}
}