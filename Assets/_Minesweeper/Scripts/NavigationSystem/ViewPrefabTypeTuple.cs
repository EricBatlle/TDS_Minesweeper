using System;
using UnityEngine;

namespace NavigationSystem
{
	[Serializable]
	public class ViewPrefabTypeTuple
	{
		[SerializeField]
		private ViewType type;
		[SerializeField]
		private GameObject prefab;

		public ViewType Type => type;
		public GameObject Prefab => prefab;
	}
}