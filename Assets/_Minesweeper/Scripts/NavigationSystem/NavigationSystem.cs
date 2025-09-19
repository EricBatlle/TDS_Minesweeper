using UnityEngine;

namespace NavigationSystem
{
    public class NavigationSystem
    {
        private readonly ViewsContainer viewsContainer;
        private readonly ViewsFactory viewsFactory;
        private readonly Transform rootCanvas;

        public NavigationSystem(ViewsContainer viewsContainer, ViewsFactory viewsFactory, Transform rootCanvas)
        {
            this.viewsContainer = viewsContainer;
            this.viewsFactory = viewsFactory;
            this.rootCanvas = rootCanvas;
        }
        
        public void Open(ViewType viewType)
        {
            viewsFactory.Create(viewsContainer.GetViewPrefab(viewType), rootCanvas);
        }
        
        public void Open<TViewData>(ViewType viewType, TViewData viewData) where TViewData : IViewData
        {
            var viewGameObject = viewsFactory.Create(viewsContainer.GetViewPrefab(viewType), rootCanvas);
            var viewWithData = viewGameObject.GetComponent<IViewWithData<TViewData>>();
            if (viewWithData == null)
            {
                Debug.LogError($"Trying to inject ViewData of type {typeof(TViewData)} in View {viewGameObject.name} but this view do not accept ViewData");
                return;
            }
            viewWithData.SetIntent(viewData);
        }

        public void Close(GameObject gameObject)
        {
            Object.DestroyImmediate(gameObject);
        }
    }
}
