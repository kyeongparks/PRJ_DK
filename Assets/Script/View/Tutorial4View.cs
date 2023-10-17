using UnityEngine.UI;

namespace PRJ
{
    public class Tutorial4View : UIView
    {
        public void Show()
        {
            ShowLayer();
        }

        protected override void OnFirstShow()
        {
            Find<Button>("ButtonGroup/BackBtn").onClick.AddListener(OnClick_BackBtn);
        }

        protected override void OnShow()
        {
            Find<Image>("BG").color = UtilityManager.Instance.HexColor("#C6DBDAFF");
        }

        #region Event
        private void OnClick_BackBtn()
        {
            ProjectRootUI.Instance.View<MainView>().Show();
        }

        #endregion
    }
}
