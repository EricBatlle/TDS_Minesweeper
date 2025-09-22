using Cysharp.Threading.Tasks;

namespace NavigationSystem
{
	public interface IResultView<TResult> : IView
	{
		UniTask<TResult> Result { get; }
	}
}