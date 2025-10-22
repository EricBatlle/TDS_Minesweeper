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
			var instanceScope = sceneScope.CreateChildFromPrefab(viewPrefab.GetComponent<LifetimeScope>());
			var instanceGameObject = instanceScope.gameObject;

			var instanceRectTransform   = instanceGameObject.transform as RectTransform;
			var parentRectTransform = parentTransform as RectTransform;
			var prefabRectTransform = viewPrefab.transform as RectTransform;

			if (instanceRectTransform != null && parentRectTransform != null)
			{
				instanceRectTransform.SetParent(parentRectTransform, false);
				if (prefabRectTransform != null)
				{
					ApplyPrefabRectTransform(instanceRectTransform, prefabRectTransform);
				}
			}
			else
			{
				instanceGameObject.transform.SetParent(parentTransform, false);
			}

			return instanceGameObject;
		}
		
		private static void ApplyPrefabRectTransform(RectTransform instance, RectTransform prefab)
		{
			instance.anchorMin = prefab.anchorMin;
			instance.anchorMax = prefab.anchorMax;
			instance.pivot     = prefab.pivot;
			instance.sizeDelta = prefab.sizeDelta;
			instance.anchoredPosition3D = prefab.anchoredPosition3D;
			instance.localRotation = prefab.localRotation;
			instance.localScale    = prefab.localScale;
		}
	}
}