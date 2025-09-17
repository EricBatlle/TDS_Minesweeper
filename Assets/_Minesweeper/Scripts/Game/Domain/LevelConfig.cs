using System;

namespace Game
{
	public class LevelConfig
	{
		public int MinesCount { get; }
		public int RowsCount { get; }

		public LevelConfig(int minesCount, int rowsCount)
		{
			if (minesCount >= rowsCount * rowsCount)
			{
				throw new ArgumentException("Too many mines for this grid size");
			}

			MinesCount = minesCount;
			RowsCount = rowsCount;
		}
	}
}