using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace NavigationSystem
{
	public sealed class ViewHandle
	{
		public GameObject GameObject { get; }
		public IView View { get; }
		public bool IsValid => GameObject && View != null;

		public ViewHandle(GameObject go, IView view)
		{
			GameObject = go;
			View = view ?? throw new ArgumentNullException(nameof(view));
		}

		public bool TryGet<TCapability>(out TCapability cap) where TCapability : class
		{
			return GameObject.TryGetComponent(out cap);
		}

		public TCapability Require<TCapability>() where TCapability : class
		{
			if (TryGet<TCapability>(out var cap))
			{
				return cap;
			}

			throw new InvalidOperationException($"View '{GameObject.name}' does not implement {typeof(TCapability).Name}.");
		}

		public UniTask AwaitClose() => View.AwaitCloseComplete;
	}
}