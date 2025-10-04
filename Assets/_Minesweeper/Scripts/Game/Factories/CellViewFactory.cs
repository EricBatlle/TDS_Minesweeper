using UnityEngine;
using Utils.VContainer;
using VContainer;

namespace Game
{
	public class CellViewFactory : PrefabFactory<CellView>
	{
		private readonly ChallengeCellService challengeCellService;

		public CellViewFactory(IObjectResolver resolver, GameObject prefab, ChallengeCellService challengeCellService) : base(resolver, prefab)
		{
			this.challengeCellService = challengeCellService;
		}

		public CellView Create(Cell cell, Transform parentTransform)
		{
			var cellView = Create(parentTransform);
			cellView.gameObject.name = $"Cell_{cell.Position.x}_{cell.Position.y}";
			cellView.SetUp(cell, challengeCellService.GetChallengeDuration());
			return cellView;
		}
	}
}