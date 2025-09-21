using System.Collections.Generic;
using System.Linq;

namespace Game
{
	public class Level
	{
		public LevelConfig Config { get; }

		public List<Cell> Cells => grid.ToList();
		public List<Cell> CellsWithBomb => grid.Where(cell => cell.HasBomb).ToList();
		
		private readonly Grid<Cell> grid;

		public Level()
		{
			
		}
		public Level(LevelConfig config)
		{
			Config = config;
			grid = new Grid<Cell>(Config.RowsCount, Config.RowsCount);
		}

		public void AddCell(int x, int y, Cell cell) => grid.Set(x, y, cell);
		public List<Cell> GetNeighborsOf(Cell cell) => grid.GetNeighborsAt(cell.Position.x, cell.Position.y);
	}
}