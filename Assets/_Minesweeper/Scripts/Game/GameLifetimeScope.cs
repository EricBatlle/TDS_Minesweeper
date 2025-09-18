using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Game
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private GameEditorCheater editorCheater;
        [Space]
        [SerializeField]
        private GridView gridView;
        [SerializeField]
        private GameObject cellPrefab;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(editorCheater);

            builder.Register<GamePresenter>(Lifetime.Singleton).AsSelf().As<IInitializable>();

            builder.Register<LevelRepository>(Lifetime.Singleton);
            builder.Register<CellViewsRepository>(Lifetime.Singleton);
            
            builder.Register<CellService>(Lifetime.Singleton);
            builder.Register<LevelService>(Lifetime.Singleton);
            
            builder.Register<CreateLevelUseCase>(Lifetime.Singleton);
            builder.Register<SelectCellUseCase>(Lifetime.Singleton);

            builder.RegisterInstance(gridView);
            builder.Register<CellViewFactory>(Lifetime.Singleton).WithParameter(cellPrefab);
            
            builder.RegisterBuildCallback(resolver =>
            {
                resolver.Inject(editorCheater);
            });
        }
    }
}
