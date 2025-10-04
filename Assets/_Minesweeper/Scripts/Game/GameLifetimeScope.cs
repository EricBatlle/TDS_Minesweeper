using Leaderboard;
using NavigationSystem;
using TimerModule;
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
        [Header("Navigation System")] 
        [SerializeField]
        private ViewsContainer viewsContainer;
        [SerializeField]
        private Transform rootCanvas;
        [Header("LevelConfig")] 
        [SerializeField]
        private LevelConfigData levelConfigData;
        [Header("Grid")]
        [SerializeField]
        private GridView gridView;
        [SerializeField]
        private GameObject cellPrefab;
        [Header("Stopwatch")] 
        [SerializeField]
        private ScoreView scoreView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(editorCheater);

            new NavigationSystemInstaller(rootCanvas, viewsContainer).Install(builder);
            new TimerSystemInstaller().Install(builder);
            new LeaderboardInstaller().Install(builder);
            new GameStateMachineInstaller().Install(builder);
            
            builder.Register<GamePresenter>(Lifetime.Singleton).AsSelf().As<IInitializable>();

            builder.Register<GameEndFlow>(Lifetime.Singleton);

            builder.Register<InMemoryCellViewsRepository>(Lifetime.Singleton).As<ICellViewsRepository>();
            builder.Register<InMemoryLevelConfigRepository>(Lifetime.Singleton).As<ILevelConfigRepository>();
            builder.Register<InMemoryLevelRepository>(Lifetime.Singleton).As<ILevelRepository>();
            builder.Register<InMemoryGameRepository>(Lifetime.Singleton).As<IGameRepository>();
            builder.Register<InMemoryScoreRepository>(Lifetime.Singleton).As<IScoreRepository>();
            
            builder.Register<LevelConfigProvider>(Lifetime.Singleton).WithParameter(levelConfigData);
            
            builder.Register<LevelService>(Lifetime.Singleton);
            builder.Register<GameService>(Lifetime.Singleton);
            builder.Register<ScoreService>(Lifetime.Singleton);
            builder.Register<ChallengeCellService>(Lifetime.Singleton);
            builder.Register<LevelDifficultyAdjusterService>(Lifetime.Singleton);
            
            builder.Register<SetLevelUseCase>(Lifetime.Singleton);
            builder.Register<TryFlagCellUseCase>(Lifetime.Singleton);
            builder.Register<SelectCellUseCase>(Lifetime.Singleton);
            
            builder.Register<InitializeGridUseCase>(Lifetime.Singleton);
            builder.Register<RevealAllLevelBombsUseCase>(Lifetime.Singleton);

            builder.RegisterInstance(gridView);
            builder.Register<CellViewFactory>(Lifetime.Singleton).WithParameter(cellPrefab);

            builder.RegisterInstance(scoreView);
            builder.Register<ScoreViewPresenter>(Lifetime.Singleton).As<IInitializable>();
            
            builder.RegisterBuildCallback(resolver =>
            {
                resolver.Inject(editorCheater);
            });
        }
    }
}
