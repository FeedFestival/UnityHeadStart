using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameScrypt.GSInteractSystem
{
    public class GSSMallText : MonoBehaviour
    {
        public Image Background;
        public TMP_Text Text;

        private RectTransform _backgroundRt;
        private static readonly float LETTER_SIZE_X = 5.6f;

        void Start()
        {
            _backgroundRt = (Background.transform as RectTransform);
        }

        internal void Show(string text = null)
        {
            gameObject.SetActive(true);

            Text.text = text;

            float sizeX = Text.text.Length * LETTER_SIZE_X;
            //Debug.Log("sizeX: " + "[text.Length] " + text.Length + " * [LETTER_SIZE_X] " + LETTER_SIZE_X);
            _backgroundRt.sizeDelta = new Vector2(sizeX, _backgroundRt.sizeDelta.y);
        }
    }
}