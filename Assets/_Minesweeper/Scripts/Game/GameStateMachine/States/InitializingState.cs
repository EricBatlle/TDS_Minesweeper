using Cysharp.Threading.Tasks;

namespace Game
{
	public class InitializingState : IGameState
	{
		public GameState Id => GameState.Initializing;
		
		private readonly CreateLevelUseCase createLevelUseCase;
		private readonly InitializeGridUseCase initializeGridUseCase;
		private readonly LevelConfigRepository levelConfigRepository;

		public InitializingState(
			LevelConfigRepository levelConfigRepository,
			InitializeGridUseCase initializeGridUseCase,
			CreateLevelUseCase createLevelUseCase)
		{
			this.levelConfigRepository = levelConfigRepository;
			this.initializeGridUseCase = initializeGridUseCase;
			this.createLevelUseCase = createLevelUseCase;
		}

		public UniTask Enter()
		{
			var levelConfig = levelConfigRepository.Get();
			var level = createLevelUseCase.Execute(levelConfig);
			initializeGridUseCase.Execute(level, levelConfig);
			return UniTask.CompletedTask;
		}

		public UniTask Exit()
		{
			return UniTask.CompletedTask;
		}
	}
}