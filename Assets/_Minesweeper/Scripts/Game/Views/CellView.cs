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
        [SerializeField] 
        private Image blinkingTimerImage;
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
        private float blinkingDurationInSeconds;

        private void Awake()
        {
	        button.onLeftClick.AddListener(OnCellLeftClicked);
	        button.onRightClick.AddListener(OnCellRightClicked);

	        blinkingTimerImage.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
	        blinkingCts?.Cancel();
	        blinkingCts?.Dispose();
	        blinkingCts = null;
        }

        public void SetUp(Cell newCell, TimeSpan challengeDuration)
        {
	        cell = newCell;
	        blinkingDurationInSeconds = (float)challengeDuration.TotalSeconds;
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
	        
	        ShowBlinkingTimerFull();

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

	        HideBlinkingTimer();
	        SetCellBackgroundColor(GetBackgroundColorByCellState(cell.State));
        }

        private async UniTaskVoid BlinkLoop(CancellationToken ct)
		{
			var period = Mathf.Max(0.02f, blinkingFrequencyInSeconds);

		    var baseColorNow = GetBackgroundColorByCellState(cell.State);
		    baseBackgroundColor = baseColorNow;

		    var aMin = baseBackgroundColor.a * Mathf.Clamp01(blinkMinAlpha);
		    var aMax = baseBackgroundColor.a;
		    var startTime = Time.unscaledTime;

		    if (blinkingTimerImage != null)
		    {
		        blinkingTimerImage.gameObject.SetActive(true);
		        blinkingTimerImage.fillAmount = 1f;
		    }

		    while (!ct.IsCancellationRequested)
		    {
		        var elapsed = Time.unscaledTime - startTime;
		        if (blinkingTimerImage != null && blinkingDurationInSeconds > 0f)
		        {
		            var t = Mathf.Clamp01(elapsed / blinkingDurationInSeconds);
		            blinkingTimerImage.fillAmount = 1f - t;

		            if (t >= 1f)
		            {
		                break;
		            }
		        }
		        var phase01 = (elapsed % period) / period;
		        var wave01 = (Mathf.Sin(phase01 * Mathf.PI * 2f) + 1f) * 0.5f;
		        var alpha = Mathf.Lerp(aMin, aMax, wave01);

		        var c = GetBackgroundColorByCellState(cell.State);
		        c.a = alpha;
		        SetCellBackgroundColor(c);
		        await UniTask.Yield(cancellationToken: ct);
		    }

		    SetCellBackgroundColor(GetBackgroundColorByCellState(cell.State));
		    HideBlinkingTimer();
		}

        private void ShowBlinkingTimerFull()
        {
	        if (blinkingTimerImage == null)
	        {
		        return;
	        }

	        blinkingTimerImage.gameObject.SetActive(true);
	        blinkingTimerImage.fillAmount = 1f;
        }

        private void HideBlinkingTimer()
        {
	        if (blinkingTimerImage == null)
	        {
		        return;
	        }

	        blinkingTimerImage.gameObject.SetActive(false);
	        blinkingTimerImage.fillAmount = 0f;
        }
	}
}