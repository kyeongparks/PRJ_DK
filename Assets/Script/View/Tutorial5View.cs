using UnityEngine.UI;

namespace PRJ
{
    public class Tutorial5View : UIView
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
            Find<Image>("BG").color = UtilityManager.Instance.HexColor("#ABDEE6FF");
        }

        #region Event
        private void OnClick_BackBtn()
        {
            ProjectRootUI.Instance.View<MainView>().Show();
        }

        #endregion
    }
}
