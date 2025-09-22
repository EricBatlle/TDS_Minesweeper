using Cysharp.Threading.Tasks;
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
        
        public ViewHandle Open(ViewType viewType)
        {
            var go = viewsFactory.Create(viewsContainer.GetViewPrefab(viewType), rootCanvas);
            var view = go.GetComponent<IView>();
            if (view == null)
            {
                Debug.LogError($"Prefab {go.name} does not implement IView.");
            }
            return new ViewHandle(go, view);
        }

        public async UniTask Close(IView view)
        {
            view.Close();
            await view.AwaitCloseComplete;
            if (view is Component component && component != null)
            {
                Object.Destroy(component.gameObject);
            }
            else
            {
                Debug.LogError($"IView is not a Component, can't destroy its GameObject automatically");
            }
        }
    }
}
