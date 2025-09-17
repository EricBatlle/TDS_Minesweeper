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
    }
}
