using System.Diagnostics;

namespace Game
{
	public interface IUserAliveStopwatchRepository
	{
		Stopwatch Get();
		void Delete();
		Stopwatch Create();
	}
}