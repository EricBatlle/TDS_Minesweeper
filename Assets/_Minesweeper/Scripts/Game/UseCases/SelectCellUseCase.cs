using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
	public class SelectCellUseCase
	{
		public event Action<HashSet<Cell>> CellsOpened;
		public event Action LevelCompleted;
		public event Action<Cell> BombSelected;

		private readonly LevelService levelService;
		private readonly GameService gameService;

		public SelectCellUseCase(
			LevelService levelService,
			GameService gameService)
		{
			this.gameService = gameService;
			this.levelService = levelService;
		}

		public void Execute(Cell selectedCell)
		{
			if (!CanCellBeSelected(selectedCell))
			{
				return;
			}

			selectedCell.State = CellState.Unopen;
			if (selectedCell.HasBomb)
			{
				gameService.SetGameFinalSelectedCell(selectedCell);
				BombSelected?.Invoke(selectedCell);
			}
			else
			{
				var level = levelService.GetCurrent();
				var openedCells = new HashSet<Cell>();
				OpenCellsRecursively(selectedCell, level, openedCells);
				if (openedCells.Count > 0)
				{
					CellsOpened?.Invoke(openedCells);
				}

				if (level.CellsWithoutBomb.Count(CanCellBeSelected) == 0)
				{
					LevelCompleted?.Invoke();
				}
			}
			
		}

		private static void OpenCellsRecursively(Cell selectedCell, Level level, HashSet<Cell> openedCells)
		{
			openedCells.Add(selectedCell);
			selectedCell.State = CellState.Open;
			var cellNeighbors = level.GetNeighborsOf(selectedCell);
			
			foreach (var cell in cellNeighbors.Where(cell=>!cell.HasBomb && cell.State == CellState.Unopen))
			{
				cellNeighbors = level.GetNeighborsOf(cell);
				if (cellNeighbors.Count(c => c.HasBomb) == 0)
				{
					OpenCellsRecursively(cell, level, openedCells);
				}
				else
				{
					openedCells.Add(cell);
					cell.State = CellState.Open;
				}
			}
		}
		
		private bool CanCellBeSelected(Cell cell)
		{
			return cell.State is CellState.Unopen;
		}
	}
}