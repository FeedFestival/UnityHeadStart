using UnityEngine.Events;

namespace Assets.HeadStart.Features.Dialog
{
    public interface IDialog
    {
        void Show(DialogOptions options);
    }

    public class DialogOptions
    {
        public string Title;
        public string Info;
        public UnityAction RetryCallback;
        public UnityAction ContinueCallback;
        public DialogOptions()
        {

        }

        public DialogOptions(
            string title, string info,
            UnityAction retryCallback = null,
            UnityAction continueCallback = null
        ) {
            Title = title;
            Info = info;
            RetryCallback = retryCallback;
            ContinueCallback = continueCallback;
        }
    }
}
