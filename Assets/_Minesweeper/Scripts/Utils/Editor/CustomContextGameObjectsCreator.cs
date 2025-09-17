using UnityEditor;
using UnityEngine;

namespace Utils.Editor
{
	public class CustomContextGameObjectsCreator : MonoBehaviour
	{
		
		private const string SettingsAssetPath = "Assets/_CroppedGame/Scripts/ScriptableObjects/CustomContextGameObjectsCreator/CustomContextSettings.asset"; 
		private static CustomContextSettings settings;

		private static CustomContextSettings Settings
		{
			get
			{
				if (settings == null)
					settings = AssetDatabase.LoadAssetAtPath<CustomContextSettings>(SettingsAssetPath);
				return settings;
			}
		}
		
		[MenuItem("GameObject/Eric/Base Text", false, 10)]
		private static void CreateBaseText(MenuCommand menuCommand)
		{
			CreateFromPrefab(Settings?.baseTextPrefab, "Base Text", menuCommand);
		}

		[MenuItem("GameObject/Eric/Base Button", false, 10)]
		private static void CreateBaseButton(MenuCommand menuCommand)
		{
			CreateFromPrefab(Settings?.baseButtonPrefab, "Base Button", menuCommand);
		}

		private static void CreateFromPrefab(Object prefab, string fallbackName, MenuCommand menuCommand)
		{
			if (prefab == null)
			{
				Debug.LogError($"[CustomContext] Falta asignar el prefab de {fallbackName} en {SettingsAssetPath}");
				return;
			}

			var go = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
			GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
			Undo.RegisterCreatedObjectUndo(go, "Create " + (go != null ? go.name : fallbackName));
			Selection.activeObject = go;
		}
	}
}