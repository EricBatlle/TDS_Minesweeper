using Cysharp.Threading.Tasks;

namespace NavigationSystem
{
	public interface IView
	{
		UniTask AwaitCloseComplete { get; }
		void Close();
	}
}