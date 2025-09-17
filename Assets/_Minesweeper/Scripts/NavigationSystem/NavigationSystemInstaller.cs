using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace NavigationSystem
{
	public class NavigationSystemInstaller : IInstaller
	{
		private readonly Transform rootCanvas;
		private readonly ViewsContainer viewsContainer;

		public NavigationSystemInstaller(Transform rootCanvas, ViewsContainer viewsContainer)
		{
			this.rootCanvas = rootCanvas;
			this.viewsContainer = viewsContainer;
		}

		public void Install(IContainerBuilder builder)
		{
			builder.Register<NavigationSystem>(Lifetime.Singleton).WithParameter(rootCanvas);
			builder.Register<ViewsFactory>(Lifetime.Singleton);
			builder.RegisterInstance(viewsContainer);
		}
	}
}