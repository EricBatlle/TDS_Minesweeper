using System.Collections.Generic;
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
		[Inject]
		private LevelConfigRepository levelConfigRepository;

		[Button]
		public void RefreshLevel()
		{
			var levelConfig = new LevelConfig(levelConfigData);
			levelConfigRepository.Update(levelConfig);
			setLevelUseCase.Execute(levelConfig);
		}

		[Button]
		public void OpenView(ViewType viewType)
		{
			navigationSystem.Open(viewType);
		}
		
		[Button]
		public void OpenViewLeaderboard()
		{
			var users = new List<User>
			{
				new User(0, "Eric", 10),
				new User(1, "Eric1", 10),
				new User(2, "Eric2", 10),
				new User(3, "Eric3", 10),
				new User(4, "Eric4", 10),
				new User(5, "Eric5", 10),
				new User(6, "Eric6", 10),
			};
			var data = new LeaderboardViewData(users);
			navigationSystem.Open(ViewType.LeaderBoard).WithData(data);
		}
	}
}