using Cysharp.Threading.Tasks;
using Leaderboard;
using NavigationSystem;

namespace Game
{
    public class GameEndFlow
    {
        private readonly RevealAllLevelBombsUseCase revealAllLevelBombsUseCase;
        private readonly NavigationSystem.NavigationSystem navigationSystem;
        private readonly LeaderboardService leaderboardService;
        private readonly SetLevelUseCase setLevelUseCase;
        private readonly ScoreService scoreService;

        private const int WaitingTimeBeforeLostFlowInMilliseconds = 1000;

        public GameEndFlow(RevealAllLevelBombsUseCase revealAllLevelBombsUseCase, NavigationSystem.NavigationSystem navigationSystem, LeaderboardService leaderboardService, SetLevelUseCase setLevelUseCase, ScoreService scoreService)
        {
            this.revealAllLevelBombsUseCase = revealAllLevelBombsUseCase;
            this.navigationSystem = navigationSystem;
            this.leaderboardService = leaderboardService;
            this.setLevelUseCase = setLevelUseCase;
            this.scoreService = scoreService;
        }

        public async UniTask ExecuteFlow(GameEndReason gameEndReason)
        {
            revealAllLevelBombsUseCase.Execute();
            if (gameEndReason == GameEndReason.Lose)
            {
                await UniTask.Delay(WaitingTimeBeforeLostFlowInMilliseconds);
                await GameLostFlow();
            }
            else
            {
                await GameWinFlow();
            }
        }

        private async UniTask GameLostFlow()
        {
            await navigationSystem.Open(ViewType.Lose)
                .WithData(new LoseViewData(scoreService.GetScore()))
                .AwaitClose();
            
            await navigationSystem.Open(ViewType.LeaderBoard)
                .WithData(new LeaderboardViewData(leaderboardService.GetAllLeaderboardEntries()))
                .AwaitClose();

            setLevelUseCase.FirstLevel();
        }

        private async UniTask GameWinFlow()
        {
            await navigationSystem.Open(ViewType.Win).AwaitClose();
            setLevelUseCase.NextLevel();
        }
    }
}
