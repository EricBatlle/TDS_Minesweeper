using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace NavigationSystem
{
	public class ViewsFactory
	{
		private readonly LifetimeScope sceneScope;
		private readonly IObjectResolver resolver;

		public ViewsFactory(LifetimeScope sceneScope, IObjectResolver resolver)
		{
			this.sceneScope = sceneScope;
			this.resolver = resolver;
		}

		public GameObject Create(GameObject viewPrefab, Transform parentTransform)
		{
			if (viewPrefab.GetComponent<LifetimeScope>() != null)
			{
				return CreateWithScope(viewPrefab, parentTransform);
			}

			var instance = Object.Instantiate(viewPrefab, parentTransform, false);
			resolver.Inject(instance);
			return instance;
		}
		
		private GameObject CreateWithScope(GameObject viewPrefab, Transform parentTransform)
		{
			var instance = sceneScope.CreateChildFromPrefab(viewPrefab.GetComponent<LifetimeScope>());
			instance.transform.SetParent(parentTransform, false);
			return instance.gameObject;
		}
	}
}