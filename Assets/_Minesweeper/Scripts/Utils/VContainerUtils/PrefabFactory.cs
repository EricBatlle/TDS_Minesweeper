using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Utils.VContainer
{
	public abstract class PrefabFactory<T> where T : class
	{
		private readonly GameObject prefab;
		private readonly IObjectResolver resolver;

		protected PrefabFactory(IObjectResolver resolver, GameObject prefab)
		{
			this.resolver = resolver;
			this.prefab = prefab;
		}

		public T Create(Transform parentTransform)
		{
			return Create(prefab, parentTransform);
		}
		
		public T Create(GameObject viewPrefab, Transform parentTransform) 
		{
			var instance = Object.Instantiate(viewPrefab, parentTransform, false);
			resolver.InjectGameObject(instance);
			return instance.GetComponent<T>();
		}
	}
}