using UnityEngine;

namespace Game
{
	[CreateAssetMenu(fileName = "LevelConfigData", menuName = "Scriptable Objects/LevelConfigData")]
	public class LevelConfigData : ScriptableObject
	{
		[SerializeField] private int minesCount;
		[SerializeField] private int rowsCount;
		public int MinesCount => minesCount;
		public int RowsCount => rowsCount;
	}
}