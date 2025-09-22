using System;
using NavigationSystem;
using UnityEngine;
using Utils;

namespace Leaderboard
{
    public class LeaderboardView : BaseView, IViewWithData<LeaderboardViewData>
    {
        public event Action PlayAgainClicked;
        
        [SerializeField] 
        private Transform labelsParentTransform;
        [SerializeField] 
        private GameObject userLabelPrefab;
        [SerializeField] 
        private CustomButton playAgainButton;

        private void Awake()
        {
            playAgainButton.onLeftClick.AddListener(OnPlayAgain);
        }

        public void SetData(LeaderboardViewData viewData)
        {
            UpdateView(viewData);
        }

        public void UpdateView(LeaderboardViewData viewData)
        {
            labelsParentTransform.DestroyAllChilds();
            foreach (var user in viewData.UsersToShow)
            {
                CreateUserLabel(user);
            }
        }

        private void CreateUserLabel(LeaderboardUser user)
        {
            var labelGameObject = Instantiate(userLabelPrefab, labelsParentTransform, false);
            var leaderboardUserLabelView = labelGameObject.GetComponent<LeaderboardUserLabelView>();
            leaderboardUserLabelView.UpdateView(new LeaderboardUserLabelViewData(user.Name, user.Score));
        }
        
        private void OnPlayAgain()
        {
            PlayAgainClicked?.Invoke();
        }
    }
}
