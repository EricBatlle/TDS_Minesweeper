using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Utils;

namespace TimerModule
{
	public class TimerRepository : BaseInMemoryRepository<List<Timer>>
	{
		public void Update(Timer timer)
		{
			var inMemoryData = Get();
			inMemoryData.Add(timer);
			Update(inMemoryData);
		}

		[CanBeNull]
		public Timer Get(string timerId)
		{
			return InMemoryData?.FirstOrDefault(timer => timer.Id == timerId);
		}
	}
}