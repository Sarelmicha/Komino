
using Komino.GameEvents.Events;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Komino.UI
{
    public class Popup : MonoBehaviour
    {
        [SerializeField] private Text header = null;
        [SerializeField] private Text message = null;
        [SerializeField] private Text buttonText = null;
        [SerializeField] private ValueEvent onServerReturnedValue = null;

        private PopupInfo popupInfo;
        private Canvas CanvasObject; // Assign in inspector


        private void Start()
        {
            CanvasObject = GetComponent<Canvas>();
        }

        public void UpdatePopup(PopupInfo popupInfo)
        {
            SetPopupInfo(popupInfo);
        }

        public void SetEnable(bool isEnable)
        {
            print("i was called with isEnabled = " + isEnable);
            CanvasObject.enabled = isEnable;
        }

        public void SetHeader(string header)
        {
            this.header.text = header;
        }

        public void SetMessage(string message)
        {
            this.message.text = message;
        }

        private void SetButtonText(string buttonText)
        {
            this.buttonText.text = buttonText;
        }

        private void SetPopupInfo(PopupInfo popupInfo)
        {
            this.popupInfo = popupInfo;

            SetHeader(this.popupInfo.GetHeader());
            SetMessage(this.popupInfo.GetMessage());
            SetButtonText(this.popupInfo.GetButtonText());
        }

        public  void OnClick()
        {
            object value = popupInfo.GetOnClickCallback()?.Invoke();

            if (value != null)
            {
                if (value.GetType() == typeof(int))
                {
                    onServerReturnedValue.Raise(new Value((int)value));
                }
                
            }
            SetEnable(false);
        }
    }
}
