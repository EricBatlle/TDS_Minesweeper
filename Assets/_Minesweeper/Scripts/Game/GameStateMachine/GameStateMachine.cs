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
            RefreshLevelUseCase refreshLevelUseCase,
            InitializeGridUseCase initializeGridUseCase,
            GameService gameService,
            IEnumerable<IGameState> states)
        {
            this.gameService = gameService;

            initializeGridUseCase.GridInitialized += OnGridInitialized;
            refreshLevelUseCase.RefreshLevel += OnRefreshLevel;

            this.states = states.ToDictionary(state => state.Id);
            validTransitions = new Dictionary<GameState, HashSet<GameState>> {
                { GameState.Default, new() { GameState.Initializing } },
                { GameState.Initializing, new() { GameState.Started } },
                { GameState.Started, new() { GameState.Initializing } },
            };
            
            currentState = this.states[GameState.Default];
        }

        public void Initialize()
        {
            TryChangeState(GameState.Initializing).Forget();
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
        
        private void OnGridInitialized() => TryChangeState(GameState.Started).Forget();
        private void OnRefreshLevel() => TryChangeState(GameState.Initializing).Forget();
    }
}
