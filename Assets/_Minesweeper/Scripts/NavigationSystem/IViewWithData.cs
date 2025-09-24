namespace NavigationSystem
{
	public interface IViewWithData<TViewData> : IView where TViewData : IViewData
	{
		public void SetData(TViewData viewData);
	}
}