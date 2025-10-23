using System.Globalization;
using TMPro;
using UnityEngine;

namespace Leaderboard
{
    public class LeaderboardUserLabelView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI userNameText;
        [SerializeField] 
        private TextMeshProUGUI userScoreText;

        public void UpdateView(int position, LeaderboardUserLabelViewData viewData)
        {
            userNameText.text = position.ToString() +" - " + viewData.UserName;
            userScoreText.text = viewData.UserScore.ToString(CultureInfo.InvariantCulture);
        }
    }
}
