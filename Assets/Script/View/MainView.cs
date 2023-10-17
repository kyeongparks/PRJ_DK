using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace PRJ
{
    public class MainView : UIView
    {
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

        }

        #region Event

        private void OnClick_Btn1()
        {
            Debug.Log("btn 1 Clicked!!!");
        }
        private void OnClick_Btn2()
        {
            Debug.Log("btn 2 Clicked!!!");
        }
        private void OnClick_Btn3()
        {
            Debug.Log("btn 3 Clicked!!!");
        }
        private void OnClick_Btn4()
        {
            Debug.Log("btn 4 Clicked!!!");
        }
        private void OnClick_Btn5()
        {
            Debug.Log("btn 5 Clicked!!!");
        }
        private void OnClick_Btn6()
        {
            Debug.Log("btn 6 Clicked!!!");
        }
        #endregion
    }
}