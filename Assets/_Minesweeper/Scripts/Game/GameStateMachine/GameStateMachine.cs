using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;

namespace Game
{
    public class GameStateMachine
    {
        public event Action<GameState> GameStateChanged;

        private readonly GameService gameService;
        
        private readonly Dictionary<GameState, IGameState> states;
        private readonly Dictionary<GameState, HashSet<GameState>> validTransitions;
        private IGameState currentState;

        public GameStateMachine(
            ChallengeCellService challengeCellService,
            SelectCellUseCase selectCellUseCase,
            SetLevelUseCase setLevelUseCase,
            InitializeGridUseCase initializeGridUseCase,
            GameService gameService,
            IEnumerable<IGameState> states)
        {
            this.gameService = gameService;

            initializeGridUseCase.GridInitialized += OnGridInitialized;
            setLevelUseCase.NewLevelSet += OnNewLevelSet;
            selectCellUseCase.BombSelected += OnBombSelected;
            selectCellUseCase.LevelCompleted += OnLevelCompleted;
            challengeCellService.ChallengeCellFailed += OnChallengeCellFailed;

            this.states = states.ToDictionary(state => state.Id);
            validTransitions = new Dictionary<GameState, HashSet<GameState>> {
                { GameState.Default, new() { GameState.Initializing } },
                { GameState.Initializing, new() { GameState.Started } },
                { GameState.Started, new() { GameState.Initializing, GameState.Lose, GameState.Win } },
                { GameState.Lose, new() { GameState.Initializing } },
                { GameState.Win, new() { GameState.Initializing } },
            };
            
            currentState = this.states[GameState.Default];
        }

        private async UniTask TryChangeState(GameState newState)
        {
            if (!validTransitions[currentState.Id].Contains(newState)) {
                throw new InvalidOperationException($"Can't change state from {currentState.Id} to {newState}");
            }

            if (currentState != null) {
                await currentState.Exit();
            }
            currentState = states[newState];
            
            // That way we ensure that inside Enter state the model is correctly updated
            gameService.ChangeGameState(gameService.GetCurrent(), newState);

            await currentState.Enter();
            GameStateChanged?.Invoke(newState);
        }
        
        private void OnNewLevelSet() => TryChangeState(GameState.Initializing).Forget();
        private void OnGridInitialized() => TryChangeState(GameState.Started).Forget();
        private void OnBombSelected(Cell cell) => TryChangeState(GameState.Lose).Forget();
        private void OnChallengeCellFailed(Cell cell) => TryChangeState(GameState.Lose).Forget();
        private void OnLevelCompleted() => TryChangeState(GameState.Win).Forget();
    }
}
