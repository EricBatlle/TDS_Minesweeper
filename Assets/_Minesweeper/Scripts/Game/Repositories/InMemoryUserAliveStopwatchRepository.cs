using System.Diagnostics;
using JetBrains.Annotations;
using Utils;

namespace Game
{
	public class InMemoryUserAliveStopwatchRepository : BaseInMemoryRepository<Stopwatch>, IUserAliveStopwatchRepository
	{
		[CanBeNull]
		public override Stopwatch Get()
		{
			return InMemoryData;
		}
	}
}