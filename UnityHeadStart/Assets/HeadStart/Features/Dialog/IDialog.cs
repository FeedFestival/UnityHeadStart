using Assets.HeadStart.Core;

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
        public CoreCallback RetryCallback;
        public CoreCallback ContinueCallback;
        public DialogOptions()
        {

        }

        public DialogOptions(
            string title, string info,
            CoreCallback retryCallback = null,
            CoreCallback continueCallback = null
        ) {
            Title = title;
            Info = info;
            RetryCallback = retryCallback;
            ContinueCallback = continueCallback;
        }
    }
}
