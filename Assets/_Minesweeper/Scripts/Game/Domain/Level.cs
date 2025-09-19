using System.Collections.Generic;
using System.Linq;

namespace Game
{
	public class Level
	{
		private LevelConfig Config { get; }
		private Grid<Cell> Grid { get; }

		public List<Cell> Cells => Grid.ToList();
		public List<Cell> CellsWithBomb => Grid.Where(cell => cell.HasBomb).ToList();

		public Level()
		{
			
		}
		public Level(LevelConfig config)
		{
			Config = config;
			Grid = new Grid<Cell>(Config.RowsCount, Config.RowsCount);
		}

		public void AddCell(int x, int y, Cell cell) => Grid.Set(x, y, cell);
		public List<Cell> GetNeighborsOf(Cell cell) => Grid.GetNeighborsAt(cell.Position.x, cell.Position.y);
	}
}