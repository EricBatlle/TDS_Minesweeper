using System;

namespace Game
{
	public class TryFlagCellUseCase
	{
		public Action<Cell> CellFlagged;
		public Action<Cell> CellUnflagged;

		private readonly CellService cellService;

		public TryFlagCellUseCase(CellService cellService)
		{
			this.cellService = cellService;
		}

		public void Execute(Cell cell)
		{
			if (cellService.CanCellBeFlagged(cell))
			{
				FlagCell(cell);
				return;
			}

			if (cellService.CanCellBeUnflagged(cell))
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
	}
}