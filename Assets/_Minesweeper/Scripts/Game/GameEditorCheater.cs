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
		private GamePresenter gamePresenter;
		[Inject]
		private CreateLevelUseCase createLevelUseCase;

		[Button]
		public void RefreshLevel()
		{
			var levelConfig = new LevelConfig(levelConfigData);
			var level = createLevelUseCase.Execute(levelConfig);
			gamePresenter.InitializeGrid(level, levelConfig);
		}
	}
}