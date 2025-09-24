using TMPro;
using UnityEngine;

namespace Game
{
    public class StopwatchView : MonoBehaviour
    {
        [SerializeField] 
        private TextMeshProUGUI timeAliveText;

        public void UpdateView(int score)
        {
            timeAliveText.SetText("Time alive: {0}ms", score);
        }
    }
}
