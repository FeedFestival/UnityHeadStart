using System;
using Assets.HeadStart.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HeadStart.Features.Dialog
{
    public class DialogCanvas : MonoBehaviour
    {
        public Text TextTitle;
        public Text TextInfo;
        public Button ButtonRetry;
        public Button ButtonContinue;
        private CoreCallback _retryCallback;
        private CoreCallback _continueCallback;
        private bool _initialized;

        private void Init()
        {
            ButtonRetry.onClick.AddListener(() =>
            {
                if (_retryCallback != null)
                {
                    _retryCallback();
                    // Close();
                }
            });
            ButtonContinue.onClick.AddListener(() =>
            {
                if (_continueCallback != null)
                {
                    _continueCallback();
                    // Close();
                }
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
