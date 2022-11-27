using System;
using DentedPixel;
using UnityEngine;
using UnityEngine.UI;

namespace GameScrypt.GSCamera
{
    /// <summary>
    /// <seealso>v.3.0.0</seealso>
    /// </summary>
    public class GSLoading : MonoBehaviour
    {
        public Slider Slider;
        public Text VersionText;
        private float _loadingValue = 0;
        private int? _loadingTwId;

        void Start()
        {
            Slider.value = 0;
            ChangeVersion(null);
            ChangeLoading(0f);
        }

        public void CleanDestroy()
        {
            this.tryStopLoading();
            Destroy(gameObject);
        }

        public void ChangeVersion(float? seed)
        {
            VersionText.text = "v.3.0.0" + (seed.HasValue ? " (seed: " + String.Format("{0:0.00}", seed.Value) + ")" : "");
        }

        public void ChangeLoading(float? add = null)
        {
            var newVal = add.HasValue ? (_loadingValue + add.Value) : _loadingValue;
            _loadingValue = newVal < _loadingValue ? _loadingValue : newVal;
            this.tryStopLoading();
            _loadingTwId = LeanTween.value(gameObject, Slider.value, _loadingValue, GSCamera.CAMERA_SETUP_TIME).id;
            LeanTween.descr(_loadingTwId.Value).setEase(LeanTweenType.easeOutCirc);
            LeanTween.descr(_loadingTwId.Value).setOnUpdate((float val) =>
            {
                Slider.value = val;
            });
        }

        private void tryStopLoading()
        {
            if (_loadingTwId.HasValue)
            {
                LeanTween.cancel(_loadingTwId.Value);
                _loadingTwId = null;
            }
        }
    }
}