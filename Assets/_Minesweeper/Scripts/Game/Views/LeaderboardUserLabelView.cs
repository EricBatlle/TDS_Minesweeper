using System.Globalization;
using TMPro;
using UnityEngine;

namespace Game
{
    public class LeaderboardUserLabelView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI userNameText;
        [SerializeField] 
        private TextMeshProUGUI userScoreText;

        public void UpdateView(LeaderboardUserLabelViewData viewData)
        {
            userNameText.text = viewData.UserName;
            userScoreText.text = viewData.UserScore.ToString(CultureInfo.InvariantCulture);
        }
    }
}
