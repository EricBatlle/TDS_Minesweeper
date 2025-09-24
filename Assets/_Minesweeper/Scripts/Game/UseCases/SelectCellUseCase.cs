using System;
using System.Collections.Generic;
using System.Linq;

namespace Game
{
	public class SelectCellUseCase
	{
		public Action<HashSet<Cell>> CellsOpened;
		public Action LevelCompleted;
		public Action<Cell> BombSelected;

		private readonly LevelRepository levelRepository;
		private readonly CellService cellService;
		private readonly GameService gameService;

		public SelectCellUseCase(
			GameService gameService,
			CellService cellService, 
			LevelRepository levelRepository)
		{
			this.gameService = gameService;
			this.cellService = cellService;
			this.levelRepository = levelRepository;
		}

		public void Execute(Cell selectedCell)
		{
			if (!cellService.CanCellBeSelected(selectedCell))
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
				var level = levelRepository.Get();
				var openedCells = new HashSet<Cell>();
				OpenCellsRecursively(selectedCell, level, openedCells);
				if (openedCells.Count > 0)
				{
					CellsOpened?.Invoke(openedCells);
				}

				if (level.CellsWithoutBomb.Count(cell => cellService.CanCellBeSelected(cell)) == 0)
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
	}
}