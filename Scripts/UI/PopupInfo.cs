using System;
using System.Threading.Tasks;

namespace Komino.UI
{
    public class PopupInfo
    {
        string header;
        string message;
        string buttonText;
        Func<object> onClickCallback;

        public PopupInfo(string header, string message, string buttonText, Func<object> onClickCallback)
        {
            this.header = header;
            this.message = message;
            this.buttonText = buttonText;
            this.onClickCallback = onClickCallback;
        }

        public void SetHeader(string header)
        {
            this.header = header;
        }

        public void SetMessage(string message)
        {
            this.message = message;
        }

        public void SetButtonText(string buttonText)
        {
            this.buttonText = buttonText;
        }

        public string GetHeader()
        {
            return this.header;
        }

        public string GetMessage()
        {
            return this.message;
        }

        public string GetButtonText()
        {
            return this.buttonText;
        }

        public Func<object> GetOnClickCallback()
        {
            return this.onClickCallback;
        }


    }
}
