using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Minesweeper
{
	public class SceneTransitioner
	{

		public async UniTask TransitionTo(string destinationSceneName)
		{
			await TransitionFromTo(SceneManager.GetActiveScene().name, destinationSceneName);
		}

		public async UniTask TransitionFromTo(string originSceneName, string destinationSceneName)
		{
			await LoadScene(ScenesNames.TRANSITION_SCENE_NAME, LoadSceneMode.Additive);
			await UnloadScene(originSceneName);

			await LoadScene(destinationSceneName, LoadSceneMode.Additive);
			await UnloadScene(ScenesNames.TRANSITION_SCENE_NAME);
		}

		private async UniTask UnloadScene(string sceneName)
		{
			Debug.Log($"Unload Scene {sceneName}");
			await SceneManager.UnloadSceneAsync(sceneName).ToUniTask();
		}

		private async UniTask LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
		{
			Debug.Log($"Load Scene {sceneName}");
			await SceneManager.LoadSceneAsync(sceneName, loadSceneMode).ToUniTask();
		}
	}
}