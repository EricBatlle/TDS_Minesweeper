namespace NavigationSystem
{
	public interface IViewWithData<TViewData> : IView where TViewData : IViewData
	{
		public void SetIntent(TViewData viewData);
	}
}