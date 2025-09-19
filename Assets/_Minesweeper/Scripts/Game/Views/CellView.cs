using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Game
{
	public class CellView : MonoBehaviour
	{
		[Serializable]
		public class CellColorAndStateTuple
		{
			public CellState state;
			public Color color;
		}

		public Action<Cell> CellLeftClicked;
		public Action<Cell> CellRightClicked;

        [SerializeField]
        private CustomButton button;
        [SerializeField]
        private Image image;
        [SerializeField]
        private TextMeshProUGUI text;
        [SerializeField]
        private GameObject flagGameObject;

        [SerializeField] 
        private List<CellColorAndStateTuple> cellColorAndStateTuples;

        [SerializeField]
        [ReadOnly]
        private Cell cell;

        private void Awake()
        {
	        button.onLeftClick.AddListener(OnCellLeftClicked);
	        button.onRightClick.AddListener(OnCellRightClicked);
        }

        public void SetUp(Cell newCell)
        {
	        cell = newCell;
        }

        public void UpdateView(CellViewData data)
        {
	        cell = data.Cell;
	        SetCellBackground(cell.State);
	        if (data.CanShowBombsAround)
	        {
				SetCellBombsAroundCounter(data.BombsAroundCount);
	        }
        }

        private void SetCellBombsAroundCounter(int bombsAroundCount)
        {
	        if (bombsAroundCount == 0)
	        {
		        text.text = string.Empty;
		        return;
	        }

	        text.text = bombsAroundCount.ToString();
        }

        private void SetCellBackground(CellState cellState)
        {
	        image.color = cellColorAndStateTuples.First(tuple => tuple.state == cellState).color;
	        flagGameObject.SetActive(cellState == CellState.Flagged);
        }

        private void OnCellLeftClicked()
        {
	        CellLeftClicked?.Invoke(cell);
        }
        
        private void OnCellRightClicked()
        {
	        CellRightClicked?.Invoke(cell);
        }
	}
}