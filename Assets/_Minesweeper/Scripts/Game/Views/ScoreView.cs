using TMPro;
using UnityEngine;

namespace Game
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI timeAliveText;

        public void UpdateView(int score)
        {
            timeAliveText.SetText("Score: {0}", score);
        }
    }
}
