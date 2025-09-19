using System;
using System.Globalization;
using NavigationSystem;
using TMPro;
using UnityEngine;
using Utils;

namespace Game
{
    public class LoseView : MonoBehaviour, IViewWithData<LoseViewData>
    {
        public event Action UserNameConfirmed;

        [SerializeField]
        private TextMeshProUGUI scoreText;
        [SerializeField]
        private TMP_InputField userNameInputField;
        [SerializeField]
        private CustomButton confirmButton;

        private void Awake()
        {
            confirmButton.onLeftClick.AddListener(OnConfirmUserName);
            userNameInputField.onSubmit.AddListener(OnConfirmUserName);
        }

        public void UpdateView(LoseViewData viewData)
        {
            scoreText.text = viewData.Score.ToString(CultureInfo.InvariantCulture);
        }

        public void SetIntent(LoseViewData viewData)
        {
            UpdateView(viewData);
        }
        
        private void OnConfirmUserName(string arg0)
        {
            UserNameConfirmed?.Invoke();
        }

        private void OnConfirmUserName()
        {
            UserNameConfirmed?.Invoke();
        }
    }
}
