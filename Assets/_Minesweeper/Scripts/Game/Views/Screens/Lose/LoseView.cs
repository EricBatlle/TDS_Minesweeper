using System;
using System.Globalization;
using Leaderboard;
using NavigationSystem;
using TMPro;
using UnityEngine;
using Utils;

namespace Game
{
    public class LoseView : BaseView, IViewWithData<LoseViewData>
    {
        public event Action<UserLeaderboardSubmission> UserLeaderboardSubmissionConfirmed;

        [SerializeField]
        private TextMeshProUGUI scoreText;
        [SerializeField]
        private TMP_InputField userNameInputField;
        [SerializeField]
        private CustomButton confirmButton;
        
        private float userScore;

        private void Awake()
        {
            confirmButton.onLeftClick.AddListener(OnConfirmUserName);
            userNameInputField.onSubmit.AddListener(OnConfirmUserName);
        }

        public void UpdateView(LoseViewData viewData)
        {
            userScore = viewData.Score;
            scoreText.text = viewData.Score.ToString(CultureInfo.InvariantCulture);
        }

        public void SetData(LoseViewData viewData)
        {
            UpdateView(viewData);
        }

        private void OnConfirmUserName(string arg0)
        {
            UserLeaderboardSubmissionConfirmed?.Invoke(GetUserLeaderboardSubmission());
        }

        private void OnConfirmUserName()
        {
            UserLeaderboardSubmissionConfirmed?.Invoke(GetUserLeaderboardSubmission());
        }

        private UserLeaderboardSubmission GetUserLeaderboardSubmission()
        {
            return new UserLeaderboardSubmission(userNameInputField.text, userScore);
        }
    }
}
