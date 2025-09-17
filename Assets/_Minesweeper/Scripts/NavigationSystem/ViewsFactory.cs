using UnityEngine;
using VContainer;

namespace NavigationSystem
{
	public class ViewsFactory
	{
		private readonly IObjectResolver resolver;

		public ViewsFactory(IObjectResolver resolver)
		{
			this.resolver = resolver;
		}

		public GameObject Create(GameObject viewPrefab, Transform parentTransform)
		{
			var instance = Object.Instantiate(viewPrefab, parentTransform, false);
			resolver.Inject(instance);
			return instance;
		}
	}
}