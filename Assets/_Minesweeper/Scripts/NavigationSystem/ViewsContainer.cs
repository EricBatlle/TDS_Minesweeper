using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace NavigationSystem
{
    [CreateAssetMenu(fileName = "ViewsContainer", menuName = "Scriptable Objects/ViewsContainer")]
    public class ViewsContainer : ScriptableObject
    {
        [SerializeField] 
        private List<ViewPrefabTypeTuple> views;

        [CanBeNull]
        public GameObject GetViewPrefab(ViewType viewType)
        {
            return views.FirstOrDefault(tuple => tuple.Type == viewType)?.Prefab;
        }
    }
}
