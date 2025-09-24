namespace Game
{
	public class CellService
	{
		public bool CanCellShowBombsAround(Cell cell)
		{
			return cell.State == CellState.Open;
		}
		
		public bool CanCellBeSelected(Cell cell)
		{
			return cell.State is CellState.Unopen;
		}

		public bool CanCellBeFlagged(Cell cell)
		{
			return cell.State is CellState.Unopen;
		}
		
		public bool CanCellBeUnflagged(Cell cell)
		{
			return cell.State is CellState.Flagged;
		}
	}
}