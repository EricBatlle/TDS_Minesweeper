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
	}
}