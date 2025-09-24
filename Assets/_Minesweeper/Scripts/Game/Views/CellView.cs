using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
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
        private Image backgroundImage;
        [SerializeField]
        private TextMeshProUGUI text;
        [SerializeField]
        private GameObject flagGameObject;
        [SerializeField]
        private GameObject bombGameObject;

        [Header("Blinking")]
        [SerializeField] 
        private float blinkingFrequencyInSeconds = 1;
        [SerializeField, Range(0f, 1f)] 
        private float blinkMinAlpha = 0.4f;
        [Space]
        [SerializeField]
        private List<CellColorAndStateTuple> cellColorAndStateTuples;
        [SerializeField]
        private Color selectedBombBackgroundColor;

        [SerializeField]
        [ReadOnly]
        private Cell cell;
        
        private CancellationTokenSource blinkingCts;
        private Color baseBackgroundColor;
        private bool isBlinking;

        private void Awake()
        {
	        button.onLeftClick.AddListener(OnCellLeftClicked);
	        button.onRightClick.AddListener(OnCellRightClicked);
        }

        private void OnDestroy()
        {
	        blinkingCts?.Cancel();
	        blinkingCts?.Dispose();
	        blinkingCts = null;
        }

        public void SetUp(Cell newCell)
        {
	        cell = newCell;
        }

        public void UpdateView(CellViewData viewData)
        {
	        cell = viewData.Cell;
	        SetCellBackground(cell.State);
	        if (viewData.CanShowBombsAround)
	        {
				SetCellBombsAroundCounter(viewData.BombsAroundCount);
	        }
        }

        public void RevealBomb(bool isSelectedBomb)
        {
	        bombGameObject.SetActive(cell.HasBomb);
	        if (isSelectedBomb)
	        {
		        SetCellBackgroundColor(selectedBombBackgroundColor);
	        }
	        else
	        {
		        SetCellBackground(CellState.Open);
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
	        SetCellBackgroundColor(GetBackgroundColorByCellState(cellState));
	        flagGameObject.SetActive(cellState == CellState.Flagged);
        }

        private Color GetBackgroundColorByCellState(CellState cellState)
        {
	        return cellColorAndStateTuples.First(tuple => tuple.state == cellState).color;
        }

        private void SetCellBackgroundColor(Color color)
        {
	        backgroundImage.color = color;
        }

        private void OnCellLeftClicked()
        {
	        CellLeftClicked?.Invoke(cell);
        }
        
        private void OnCellRightClicked()
        {
	        CellRightClicked?.Invoke(cell);
        }

        [Button]
        public void StartBlinking()
        {
	        if (isBlinking)
	        {
		        return;
	        }

	        isBlinking = true;
	        baseBackgroundColor = backgroundImage.color;

	        blinkingCts?.Cancel();
	        blinkingCts?.Dispose();
	        blinkingCts = new CancellationTokenSource();

	        BlinkLoop(blinkingCts.Token).Forget();
        }
        
        [Button]
        public void StopBlinking()
        {
	        if (!isBlinking)
	        {
		        return;
	        }

	        isBlinking = false;

	        blinkingCts?.Cancel();
	        blinkingCts?.Dispose();
	        blinkingCts = null;

	        SetCellBackgroundColor(GetBackgroundColorByCellState(cell.State));
        }

        private async UniTaskVoid BlinkLoop(CancellationToken ct)
        {
	        var halfPeriodSeconds = Mathf.Max(0.01f, blinkingFrequencyInSeconds / 2f);

	        var faded = baseBackgroundColor;
	        faded.a = baseBackgroundColor.a * Mathf.Clamp01(blinkMinAlpha);

	        var showFaded = false;

	        while (!ct.IsCancellationRequested)
	        {
		        SetCellBackgroundColor(showFaded ? faded : GetBackgroundColorByCellState(cell.State));
		        showFaded = !showFaded;

		        await UniTask.Delay(TimeSpan.FromSeconds(halfPeriodSeconds), cancellationToken: ct);
	        }

	        SetCellBackgroundColor(baseBackgroundColor);
        }
	}
}