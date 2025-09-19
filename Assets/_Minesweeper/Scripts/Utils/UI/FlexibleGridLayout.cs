using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
	public class FlexibleGridLayout : LayoutGroup
	{
		private enum FitType
		{
			Uniform,
			Width,
			Height,
			FixedRows,
			FixedColumns
		}
		[SerializeField]
		private FitType fitType;
		[Space]
		[SerializeField]
		private int rows;
		[SerializeField]
		private int columns;
		[SerializeField]
		private Vector2 cellSize;
		[SerializeField]
		private Vector2 spacing;

		[SerializeField]
		private bool fitX;
		[SerializeField]
		private bool fitY;

		public void ResizeByRows(int rowsCount)
		{
			rows = rowsCount;
			CalculateLayoutInputHorizontal();
		}

		public void ClearLayoutElements()
		{
			gameObject.DestroyAllChilds();
		}
		
		public override void CalculateLayoutInputHorizontal()
		{
			base.CalculateLayoutInputHorizontal();

			if (fitType is FitType.Width or FitType.Uniform)
			{
				fitX = true;
				fitY = true;

				var sqrRt = Mathf.Sqrt(transform.childCount);
				rows = Mathf.CeilToInt(sqrRt);
				columns = Mathf.CeilToInt(sqrRt);
			}

			switch (fitType)
			{
				case FitType.Width:
				case FitType.FixedColumns:
					rows = Mathf.CeilToInt(transform.childCount / (float)columns);
					break;
				case FitType.Height:
				case FitType.FixedRows:
					columns = Mathf.CeilToInt(transform.childCount / (float)rows);
					break;
			}

			var parentWidth = rectTransform.rect.width;
			var parentHeight = rectTransform.rect.height;

			var cellWidth = (parentWidth / columns) - (spacing.x / columns * (columns - 1)) - (padding.left / (float)columns) - (padding.right / (float)columns);
			var cellHeight = (parentHeight / rows) - (spacing.y / rows * (rows - 1)) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

			cellSize.x = fitX ? cellWidth : cellSize.x;
			cellSize.y = fitY ? cellHeight : cellSize.y;

			for (var i = 0; i < rectChildren.Count; i++)
			{
				var rowCount = i / columns;
				var columnCount = i % columns;

				var item = rectChildren[i];

				var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
				var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

				SetChildAlongAxis(item, 0, xPos, cellSize.x);
				SetChildAlongAxis(item, 1, yPos, cellSize.y);
			}
		}

		public override void CalculateLayoutInputVertical()
		{
		}

		public override void SetLayoutHorizontal()
		{
		}

		public override void SetLayoutVertical()
		{
		}
	}	
}