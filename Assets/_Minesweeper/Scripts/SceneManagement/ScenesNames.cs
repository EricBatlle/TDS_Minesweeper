using System.Collections.Generic;

namespace Minesweeper
{
	public static class ScenesNames
	{
		public const string GAME_SCENE_NAME = "GameScene";
		public const string TRANSITION_SCENE_NAME = "TransitionScene";

		public static string[] GetAllScenesNames = new string[]
		{
			GAME_SCENE_NAME,
			TRANSITION_SCENE_NAME
		};

		public static readonly Dictionary<SceneType, string> GetAllScenesNamesByType = new Dictionary<SceneType, string>()
		{
			{SceneType.Game, GAME_SCENE_NAME },
			{SceneType.Transition, TRANSITION_SCENE_NAME },
		};
	}
}