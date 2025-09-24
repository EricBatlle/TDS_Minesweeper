using System;

namespace Game
{
	public class LevelConfig
	{
		public int MinesCount { get; }
		public int RowsCount { get; }
		public int ChallengeCellFrequencyInSeconds { get; }
		public int TimeToCompleteChallengeInSeconds { get; }

		public LevelConfig()
		{
			MinesCount = 3;
			RowsCount = 2;
			ChallengeCellFrequencyInSeconds = 3;
			TimeToCompleteChallengeInSeconds = 3;
		}
		
		public LevelConfig(int minesCount, int rowsCount, int challengeCellFrequencyInSeconds = 3, int timeToCompleteChallengeInSeconds = 3)
		{
			if (minesCount >= rowsCount * rowsCount)
			{
				throw new ArgumentException("Too many mines for this grid size");
			}

			MinesCount = minesCount;
			RowsCount = rowsCount;
			ChallengeCellFrequencyInSeconds = challengeCellFrequencyInSeconds;
			TimeToCompleteChallengeInSeconds = timeToCompleteChallengeInSeconds;
		}

		public LevelConfig(LevelConfigData data)
			: this((data ?? throw new ArgumentNullException(nameof(data))).MinesCount, data.RowsCount, data.ChallengeCellFrequencyInSeconds, data.TimeToCompleteChallengeInSeconds)
		{
		}
	}
}