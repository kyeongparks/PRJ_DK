using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;


namespace PRJ
{
    public abstract class UIPopup : UILayer
    {
        public override UILayerTypes LayerType { get { return UILayerTypes.Popup; } }
        public bool TopMost = false;
        public bool CancelOnBack = true;
        protected PopupState state = null;


        protected override void OnAwakeLayer()
        {
            foreach (Button button in transform.GetComponentsInChildren<Button>(true))
            {
                string buttonName = button.name;
                try
                {
                    if (buttonName.Length > 0 && Char.IsDigit(buttonName[0]))
                        continue;

                    PopupResults resultCode = (PopupResults)Enum.Parse(typeof(PopupResults), buttonName, true);
                    if (!resultCode.IsNone())
                    {
                        button.onClick.AddListener(() => OnResult(resultCode));
                    }
                }
                catch
                {
                }
            }
        }

        internal override void ShowLayer()
        {
            if (TopMost)
                transform.SetAsLastSibling();
            else
            {
                List<UIPopup> popups = new List<UIPopup>();
                popups.AddRange(transform.parent.GetComponentsInChildren<UIPopup>(false));

                //top most 설정된 팝업이 존재한다면 해당 팝업보다 낮은 sibling index를 설정... 없다면 SetAsLastSibling 설정..
                List<UIPopup> finds = popups.FindAll(p => p.TopMost);
                if (finds.Count > 0)
                {
                    int? minIndex = null;
                    foreach (UIPopup popup in finds)
                    {
                        if (!minIndex.HasValue)
                        {
                            minIndex = popup.transform.GetSiblingIndex() - 1;
                        }
                        else
                        {
                            minIndex = Mathf.Min(minIndex.Value, popup.transform.GetSiblingIndex());
                        }
                    }

                    transform.SetSiblingIndex(minIndex.Value);
                }
                else
                    transform.SetAsLastSibling();
            }

            base.ShowLayer();
            if (state == null)
                state = new PopupState();

            state.State = PopupStates.Open;
        }

        protected PopupState<T> ShowPopup<T>()
        {
            if (state != null)
            {
                state.Result = PopupResults.Cancel;
                state = null;
            }

            state = new PopupState<T>();
            ShowLayer();

            return state as PopupState<T>;
        }

        protected PopupState ShowPopup()
        {
            if (state != null)
            {
                state.Result = PopupResults.Cancel;
                state = null;
            }

            state = new PopupState();
            ShowLayer();

            return state;
        }

        public virtual void Close()
        {
            OnResult(PopupResults.Close);
        }

        protected virtual void OnResult(PopupResults result)
        {
            HideLayer();
            if (state != null)
                state.Result = result;

            state = null;
            SendMessage("On" + result.ToString(), SendMessageOptions.DontRequireReceiver);
        }

        internal override void OnEscapePressed()
        {
            if (CancelOnBack)
                OnResult(PopupResults.Cancel);
        }
    }
}

