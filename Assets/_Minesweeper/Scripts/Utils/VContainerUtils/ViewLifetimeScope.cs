using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Utils
{
	public abstract class ViewLifetimeScope<TView, TPresenter> : LifetimeScope
	{
		[SerializeField]
		private TView view;

		protected override void Configure(IContainerBuilder builder)
		{
			builder.Register<TPresenter>(Lifetime.Singleton).As<IInitializable>();
			builder.RegisterInstance(view);
		}
	}
}