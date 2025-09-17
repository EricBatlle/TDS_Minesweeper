using UnityEngine;
using Utils.VContainer;
using VContainer;

namespace Game
{
	public class CellViewFactory : PrefabFactory<CellView>
	{
		public CellViewFactory(IObjectResolver resolver, GameObject prefab) : base(resolver, prefab)
		{
		}

		public CellView Create(Cell cell, Transform parentTransform)
		{
			var cellView = Create(parentTransform);
			cellView.gameObject.name = $"Cell_{cell.Position.x}_{cell.Position.y}";
			cellView.SetUp(cell);
			return cellView;
		}
	}
}