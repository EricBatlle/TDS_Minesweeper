using Cysharp.Threading.Tasks;

namespace Game
{
	public class InitializingState : IGameState
	{
		public GameState Id => GameState.Initializing;
		
		private readonly SetLevelUseCase setLevelUseCase;
		private readonly InitializeGridUseCase initializeGridUseCase;
		private readonly LevelConfigRepository levelConfigRepository;

		public InitializingState(
			LevelConfigRepository levelConfigRepository,
			InitializeGridUseCase initializeGridUseCase,
			SetLevelUseCase setLevelUseCase)
		{
			this.levelConfigRepository = levelConfigRepository;
			this.initializeGridUseCase = initializeGridUseCase;
			this.setLevelUseCase = setLevelUseCase;
		}

		public UniTask Enter()
		{
			// ToDo: Move to GamePresenter
			var levelConfig = levelConfigRepository.Get();
			var level = setLevelUseCase.Execute(levelConfig);
			initializeGridUseCase.Execute(level, levelConfig);
			return UniTask.CompletedTask;
		}

		public UniTask Exit()
		{
			return UniTask.CompletedTask;
		}
	}
}