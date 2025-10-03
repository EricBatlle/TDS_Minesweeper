using System.Collections.Generic;
using Leaderboard;
using NavigationSystem;
using TriInspector;
using UnityEngine;
using VContainer;

namespace Game
{
	[CreateAssetMenu(fileName = "GameEditorCheater", menuName = "Scriptable Objects/GameEditorCheater")]
	public class GameEditorCheater : ScriptableObject
	{
		[SerializeField]
		private LevelConfigData levelConfigData;

		[Inject]
		private NavigationSystem.NavigationSystem navigationSystem;
		[Inject]
		private SetLevelUseCase setLevelUseCase;

		[Button]
		public void RefreshLevel()
		{
			var levelConfig = new LevelConfig(levelConfigData);
			setLevelUseCase.Execute(levelConfig);
		}

		[Button]
		public void OpenView(ViewType viewType)
		{
			navigationSystem.Open(viewType);
		}
		
		[Button]
		public void OpenViewLeaderboardWithPlaceholders()
		{
			var users = new List<LeaderboardUser>
			{
				new LeaderboardUser(0, "Eric", 10),
				new LeaderboardUser(1, "Eric1", 10),
				new LeaderboardUser(2, "Eric2", 10),
				new LeaderboardUser(3, "Eric3", 10),
				new LeaderboardUser(4, "Eric4", 10),
				new LeaderboardUser(5, "Eric5", 10),
				new LeaderboardUser(6, "Eric6", 10),
			};
			var data = new LeaderboardViewData(users);
			navigationSystem.Open(ViewType.LeaderBoard).WithData(data);
		}
	}
}