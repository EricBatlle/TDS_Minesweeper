namespace Game
{
	public interface ICellViewsRepository
	{
		CellView Get(Cell cell);
		void Update(Cell cell, CellView cellView);
	}
}