using System.Linq;

namespace Game
{
	public class CellService
	{
		private readonly LevelRepository levelRepository;

		public CellService(LevelRepository levelRepository)
		{
			this.levelRepository = levelRepository;
		}

		public bool CanCellShowBombsAround(Cell cell)
		{
			return cell.State == CellState.Open;
		}

		public int GetNeighborsWithBombCount(Cell cell)
		{
			var level = levelRepository.Get();
			var cellNeighbors = level.GetNeighborsOf(cell);
			return cellNeighbors.Count(neighborCell => neighborCell.HasBomb);
		}
	}
}