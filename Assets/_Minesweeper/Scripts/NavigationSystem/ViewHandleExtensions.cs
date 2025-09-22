using Cysharp.Threading.Tasks;

namespace NavigationSystem
{
	public static class ViewHandleExtensions
	{
		// ViewData
		public static ViewHandle WithData<TViewData>(this ViewHandle screen, TViewData data)
			where TViewData : IViewData
		{
			screen.Require<IViewWithData<TViewData>>().SetData(data);
			return screen;
		}

		// Result
		public static UniTask<TResult> Expect<TResult>(this ViewHandle screen)
		{
			return screen.Require<IResultView<TResult>>().Result;
		}
	}
}