using System.Diagnostics;
using JetBrains.Annotations;
using Utils;

namespace Game
{
	public class UserAliveStopwatchRepository : BaseInMemoryRepository<Stopwatch>
	{
		[CanBeNull]
		public override Stopwatch Get()
		{
			return InMemoryData;
		}

		public void Delete()
		{
			InMemoryData = null;
		}
	}
}