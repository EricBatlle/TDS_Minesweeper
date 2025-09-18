using System;

namespace Game
{
	public class LevelConfig
	{
		public int MinesCount { get; }
		public int RowsCount { get; }

		public LevelConfig()
		{
			MinesCount = 5;
			RowsCount = 5;
		}
		
		public LevelConfig(int minesCount, int rowsCount)
		{
			if (minesCount >= rowsCount * rowsCount)
			{
				throw new ArgumentException("Too many mines for this grid size");
			}

			MinesCount = minesCount;
			RowsCount = rowsCount;
		}
		
		public LevelConfig(LevelConfigData data)
			: this((data ?? throw new ArgumentNullException(nameof(data))).MinesCount, data.RowsCount)
		{
		}
	}
}