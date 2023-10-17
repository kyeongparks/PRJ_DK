using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PRJ
{
    public class Tutorial1View : UIView
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
            Find<Image>("BG").color = UtilityManager.Instance.HexColor("#FFCECEFF");
        }

        #region Event
        private void OnClick_BackBtn()
        {
            ProjectRootUI.Instance.View<MainView>().Show();
        }

        #endregion
    }
}
