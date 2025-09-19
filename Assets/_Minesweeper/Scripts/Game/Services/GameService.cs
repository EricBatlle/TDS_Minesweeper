namespace Game
{
	public class GameService
	{
		private readonly GameRepository gameRepository;

		public GameService(GameRepository gameRepository)
		{
			this.gameRepository = gameRepository;
		}

		/// <summary>
		///  IMPORTANT: This must be called JUST from the StateMachines to ensure a single entry point!
		/// </summary>
		public void ChangeGameState(Game game, GameState newGameState)
		{
			game.State = newGameState;
		}

		public Game CreateGame()
		{
			return gameRepository.Create();
		}

		public Game GetCurrent()
		{
			return gameRepository.Get();
		}

		public void SetGameFinalSelectedCell(Cell cell)
		{
			gameRepository.Get().LoseGameCell = cell;
		}
		
		public Cell GetGameFinalSelectedCell()
		{
			return gameRepository.Get().LoseGameCell;
		}
	}
}