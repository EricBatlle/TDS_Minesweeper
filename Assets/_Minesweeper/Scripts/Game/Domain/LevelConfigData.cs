using UnityEngine;

namespace Game
{
	[CreateAssetMenu(fileName = "LevelConfigData", menuName = "Scriptable Objects/LevelConfigData")]
	public class LevelConfigData : ScriptableObject
	{
		[SerializeField] private int minesCount;
		[SerializeField] private int rowsCount;
		[SerializeField] private int challengeCellFrequencyInSeconds;
		[SerializeField] private int timeToCompleteChallengeInSeconds;
		public int MinesCount => minesCount;
		public int RowsCount => rowsCount;
		public int ChallengeCellFrequencyInSeconds => challengeCellFrequencyInSeconds;
		public int TimeToCompleteChallengeInSeconds => timeToCompleteChallengeInSeconds;
	}
}