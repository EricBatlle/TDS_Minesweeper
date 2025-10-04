using System;

namespace Game
{
	public class TryFlagCellUseCase
	{
		public event Action<Cell> CellFlagged;
		public event Action<Cell> CellUnflagged;

		public void Execute(Cell cell)
		{
			if (CanCellBeFlagged(cell))
			{
				FlagCell(cell);
				return;
			}

			if (CanCellBeUnflagged(cell))
			{
				UnflagCell(cell);
			}
		}

		private void FlagCell(Cell cellToFlag)
		{
			cellToFlag.State = CellState.Flagged;
			CellFlagged?.Invoke(cellToFlag);
		}

		private void UnflagCell(Cell cellToUnflag)
		{
			cellToUnflag.State = CellState.Unopen;
			CellUnflagged?.Invoke(cellToUnflag);
		}
		
		private bool CanCellBeFlagged(Cell cell)
		{
			return cell.State is CellState.Unopen;
		}
		
		private bool CanCellBeUnflagged(Cell cell)
		{
			return cell.State is CellState.Flagged;
		}
	}
}