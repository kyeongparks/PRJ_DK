using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor.PackageManager.UI;

namespace PRJ
{
    public class MainView : UIView
    {
        [SerializeField] List<Sprite> images;

        private int curIdx = 0, prevIdx = -1;
        public void Show()
        {
            ShowLayer();
        }

        protected override void OnFirstShow()
        {
            Find<Button>("ButtonGroup/Tutorial1_Btn").onClick.AddListener(OnClick_Btn1);
            Find<Button>("ButtonGroup/Tutorial2_Btn").onClick.AddListener(OnClick_Btn2);
            Find<Button>("ButtonGroup/Tutorial3_Btn").onClick.AddListener(OnClick_Btn3);
            Find<Button>("ButtonGroup/Tutorial4_Btn").onClick.AddListener(OnClick_Btn4);
            Find<Button>("ButtonGroup/Tutorial5_Btn").onClick.AddListener(OnClick_Btn5);
            Find<Button>("ButtonGroup/Tutorial6_Btn").onClick.AddListener(OnClick_Btn6);
        }

        protected override void OnShow()
        {
            Find<Text>("TitleBG/Title").text = "Unity Tutorial Example";
        }

        protected override void OnEnableLayer()
        {
            while(curIdx == prevIdx)
            {
                curIdx = Random.Range(0, images.Count);
                if (prevIdx == -1) break;
            }

            Find<Image>("BG").sprite = images[curIdx];
            prevIdx = curIdx;
        }

        #region Event

        private void OnClick_Btn1()
        {
            ProjectRootUI.Instance.View<Tutorial1View>().Show();
        }
        private void OnClick_Btn2()
        {
            ProjectRootUI.Instance.View<Tutorial2View>().Show();
        }
        private void OnClick_Btn3()
        {
            ProjectRootUI.Instance.View<Tutorial3View>().Show();
        }
        private void OnClick_Btn4()
        {
            ProjectRootUI.Instance.View<Tutorial4View>().Show();
        }
        private void OnClick_Btn5()
        {
            ProjectRootUI.Instance.View<Tutorial5View>().Show();
        }
        private void OnClick_Btn6()
        {
            ProjectRootUI.Instance.View<Tutorial6View>().Show();
        }
        #endregion
    }
}