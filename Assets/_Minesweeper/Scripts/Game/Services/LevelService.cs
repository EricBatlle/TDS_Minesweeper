using System.Linq;
using UnityEngine;
using Utils;

namespace Game
{
	public class LevelService
	{
		private readonly ILevelRepository levelRepository;
		private readonly IRandomProvider randomProvider;

		public LevelService(ILevelRepository levelRepository, IRandomProvider randomProvider)
		{
			this.levelRepository = levelRepository;
			this.randomProvider = randomProvider;
		}

		public void PopulateLevelGrid(Level level, LevelConfig levelConfig)
		{
			var rowsCount = levelConfig.RowsCount;
			var cellsCount = rowsCount * rowsCount;
			var cellsIndexWithBomb = randomProvider.GetUniqueRandomNumbers(levelConfig.MinesCount, 0, cellsCount - 1);
			
			for (var rowIndex = 0; rowIndex < rowsCount; rowIndex++)
			{
				for (var columnIndex = 0; columnIndex < rowsCount; columnIndex++)
				{
					var hasBomb = cellsIndexWithBomb.Contains(rowIndex + columnIndex * rowsCount);
					level.AddCell(rowIndex, columnIndex, new Cell(CellState.Unopen, hasBomb, new Vector2Int(rowIndex, columnIndex)));
				}	
			}
		}

		public Level GetCurrent()
		{
			return levelRepository.Get();
		}

		public void UpdateLevel(Level level)
		{
			levelRepository.Update(level);
		}

		public int GetNeighborsWithBombCount(Level level, Cell cell)
		{
			var cellNeighbors = level.GetNeighborsOf(cell);
			return cellNeighbors.Count(neighborCell => neighborCell.HasBomb);
		}
	}
}