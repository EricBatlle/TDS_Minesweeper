using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
	public class SelectCellUseCase
	{
		public Action<List<Cell>> CellsOpened;
		public Action BombSelected;

		private readonly LevelRepository levelRepository;

		public SelectCellUseCase(LevelRepository levelRepository)
		{
			this.levelRepository = levelRepository;
		}

		public void Execute(Cell selectedCell)
		{
			selectedCell.State = CellState.Unopen;
			if (selectedCell.HasBomb)
			{
				BombSelected?.Invoke();
			}
			else
			{
				var level = levelRepository.Get();
				var openedCells = new List<Cell>();
				OpenCellsRecursively(selectedCell, level, openedCells);
				CellsOpened?.Invoke(openedCells);
			}
		}

		private static void OpenCellsRecursively(Cell selectedCell, Level level, List<Cell> openedCells)
		{
			openedCells.Add(selectedCell);
			selectedCell.State = CellState.Open;
			var cellNeighbors = level.GetNeighborsOf(selectedCell);
			foreach (var cell in cellNeighbors.Where(cell=>!cell.HasBomb && cell.State == CellState.Unopen))
			{
				OpenCellsRecursively(cell, level, openedCells);
			}
		}
	}
}