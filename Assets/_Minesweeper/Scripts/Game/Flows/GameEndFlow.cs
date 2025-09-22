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

        public GameEndFlow(RevealAllLevelBombsUseCase revealAllLevelBombsUseCase, NavigationSystem.NavigationSystem navigationSystem, LeaderboardService leaderboardService, SetLevelUseCase setLevelUseCase)
        {
            this.revealAllLevelBombsUseCase = revealAllLevelBombsUseCase;
            this.navigationSystem = navigationSystem;
            this.leaderboardService = leaderboardService;
            this.setLevelUseCase = setLevelUseCase;
        }

        public async UniTask ExecuteFlow(GameEndReason gameEndReason)
        {
            if (gameEndReason == GameEndReason.Lose)
            {
                await GameLostFlow();
            }
            else
            {
                await GameWinFlow();
            }
        }

        private async UniTask GameLostFlow()
        {
            revealAllLevelBombsUseCase.Execute();

            await navigationSystem.Open(ViewType.Lose)
                .WithData(new LoseViewData(10))
                .AwaitClose();
            
            await navigationSystem.Open(ViewType.LeaderBoard)
                .WithData(new LeaderboardViewData(leaderboardService.GetAllLeaderboardEntries()))
                .AwaitClose();

            setLevelUseCase.Execute();
        }

        private async UniTask GameWinFlow()
        {
            await navigationSystem.Open(ViewType.Win)
                .AwaitClose();
            setLevelUseCase.NextLevel();
        }
    }
}
