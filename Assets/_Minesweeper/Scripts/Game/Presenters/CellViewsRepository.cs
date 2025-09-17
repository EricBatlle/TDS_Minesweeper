using System.Collections.Generic;
using JetBrains.Annotations;
using Utils;

namespace Game
{
	public class CellViewsRepository : BaseInMemoryRepository<Dictionary<Cell, CellView>>
	{
		[CanBeNull]
		public CellView Get(Cell cell)
		{
			return Get().GetValueOrDefault(cell);
		}
		
		public void Update(Cell cell, CellView cellView)
		{
			var inMemoryData = Get();
			inMemoryData.Add(cell, cellView);
			Update(inMemoryData);
		}
	}
}