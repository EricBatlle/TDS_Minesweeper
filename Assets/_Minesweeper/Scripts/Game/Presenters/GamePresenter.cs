using System.Collections.Generic;
using VContainer.Unity;

namespace Game
{
	public class GamePresenter : IInitializable
	{
		private readonly CreateLevelUseCase createLevelUseCase;
		private readonly SelectCellUseCase selectCellUseCase;
		private readonly GridView gridView;
		private readonly CellViewFactory cellViewFactory;
		private readonly CellViewsRepository cellViewsRepository;
		private readonly CellService cellService;

		public GamePresenter(
			CellViewsRepository cellViewsRepository,
			CreateLevelUseCase createLevelUseCase, 
			SelectCellUseCase selectCellUseCase, 
			GridView gridView, 
			CellViewFactory cellViewFactory,
			CellService cellService)
		{
			this.cellViewsRepository = cellViewsRepository;
			this.createLevelUseCase = createLevelUseCase;
			this.selectCellUseCase = selectCellUseCase;
			this.gridView = gridView;
			this.cellViewFactory = cellViewFactory;
			this.cellService = cellService;
		}

		public void Initialize()
		{
			selectCellUseCase.CellsOpened += OnCellsOpened;

			var levelConfig = new LevelConfig(5, 5);
			var level = createLevelUseCase.Execute(levelConfig);

			InitializeGrid(level, levelConfig);
		}

		public void InitializeGrid(Level level, LevelConfig levelConfig)
		{
			gridView.ClearGrid();
			gridView.SetGridRows(levelConfig.RowsCount);
			foreach (var cell in level.Cells)
			{
				var cellView = cellViewFactory.Create(cell, gridView.CellsSpawnTransform);
				cellViewsRepository.Update(cell, cellView);
				cellView.CellClicked += OnCellClicked;
			}
		}

		private void OnCellsOpened(List<Cell> cells)
		{
			foreach (var cell in cells)
			{
				var cellView = cellViewsRepository.Get(cell);
				cellView?.UpdateView(new CellViewData
				{
					Cell = cell,
					CanShowBombsAround = cellService.CanCellShowBombsAround(cell),
					BombsAroundCount = cellService.GetNeighborsWithBombCount(cell)
				});
			}
		}

		private void OnCellClicked(Cell cell)
		{
			selectCellUseCase.Execute(cell);
		}
	}
}