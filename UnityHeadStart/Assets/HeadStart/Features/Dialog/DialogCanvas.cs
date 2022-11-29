using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace Assets.HeadStart.Features.Dialog
{
    public class DialogCanvas : MonoBehaviour
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
        public TMP_Text TextTitle;
        public TMP_Text TextInfo;
        public Button ButtonRetry;
        public Button ButtonContinue;
        private UnityAction _retryCallback;
        private UnityAction _continueCallback;
        private bool _initialized;

        private void Init()
        {
            ButtonRetry.onClick.AddListener(() =>
            {
                _retryCallback?.Invoke();
            });
            ButtonContinue.onClick.AddListener(() =>
            {
                _continueCallback?.Invoke();
            });

            _initialized = true;
        }

        internal void Show(DialogOptions options)
        {
            if (_initialized == false)
            {
                Init();
            }

            TextTitle.text = options.Title;
            TextInfo.text = options.Info;

            _retryCallback = options.RetryCallback;
            _continueCallback = options.ContinueCallback;

            gameObject.SetActive(true);
        }

        public void Close()
        {
            _retryCallback = null;
            _continueCallback = null;
            gameObject.SetActive(false);
        }
    }
}
