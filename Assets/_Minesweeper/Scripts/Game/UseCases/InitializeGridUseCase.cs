using System;

namespace Game
{
	// ToDo: Revisit, I don't like the fact that this useCase can call another UseCase :/
	public class InitializeGridUseCase
	{
		public event Action GridInitialized;

		private readonly GridView gridView;
		private readonly CellViewFactory cellViewFactory;
		private readonly SelectCellUseCase selectCellUseCase;
		private readonly TryFlagCellUseCase tryFlagCellUseCase;
		private readonly CellViewsRepository cellViewsRepository;

		public InitializeGridUseCase(
			GridView gridView,
			CellViewFactory cellViewFactory,
			SelectCellUseCase selectCellUseCase,
			TryFlagCellUseCase tryFlagCellUseCase,
			CellViewsRepository cellViewsRepository)
		{
			this.tryFlagCellUseCase = tryFlagCellUseCase;
			this.gridView = gridView;
			this.cellViewFactory = cellViewFactory;
			this.selectCellUseCase = selectCellUseCase;
			this.cellViewsRepository = cellViewsRepository;
		}

		public void Execute(Level level, LevelConfig levelConfig)
		{
			gridView.ClearGrid();
			gridView.SetGridRows(levelConfig.RowsCount);
			foreach (var cell in level.Cells)
			{
				var cellView = cellViewFactory.Create(cell, gridView.CellsSpawnTransform);
				cellViewsRepository.Update(cell, cellView);
				cellView.CellLeftClicked += OnCellLeftClicked;
				cellView.CellRightClicked += OnCellRightClicked;
			}

			GridInitialized?.Invoke();
		}
		
		private void OnCellLeftClicked(Cell cell)
		{
			selectCellUseCase.Execute(cell);
		}
		
		private void OnCellRightClicked(Cell cell)
		{
			tryFlagCellUseCase.Execute(cell);
		}
	}
}