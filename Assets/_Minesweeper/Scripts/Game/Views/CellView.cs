using System;
using System.Collections.Generic;
using System.Linq;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;

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

		public Action<Cell> CellClicked;

        [SerializeField]
        private Button button;
        [SerializeField]
        private Image image;

        [SerializeField] 
        private List<CellColorAndStateTuple> cellColorAndStateTuples;

        [SerializeField]
        [ReadOnly]
        private Cell cell;

        private void Awake()
        {
	        button.onClick.AddListener(OnCellClicked);
        }

        public void SetUp(Cell newCell)
        {
	        cell = newCell;
        }

        public void UpdateView(Cell newCell)
        {
	        cell = newCell;
	        SetCellState(cell.State);
        }

        private void SetCellState(CellState cellState)
        {
	        image.color = cellColorAndStateTuples.First(tuple => tuple.state == cellState).color;
        }

        private void OnCellClicked()
        {
	        CellClicked?.Invoke(cell);
        }
	}
}