using Assets.HeadStart.Core;
using Assets.HeadStart.Core.SFX;
using Assets.Scripts.utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HeadStart.Features.ScreenData
{
    public class PointTextParticle : MonoBehaviour, IPoolObject, IPointText
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
        private int _id;
        private bool _isUsed;
        protected Text _textComponent;
        protected int _pointsValue;
        private Vector2 _viewportPosition;
        private Vector2 _worldObjectScreenPosition;
        private RectTransform _rt;
        private int? _reflectTweenId;
        private int? _sizeTweenId;
        private int? _towardsTotalTweenId;
        protected Color _currentColor;
        public TrailRenderer Trail;
        private Vector2Int _actualScreenSize;
        private OnPointsUpdated _onPointsUpdated;
        private Vector2 _totalPointsPos;

        // CONSTANTS
        private readonly float TOWARD_TOTAL_ANIM_TIME = 1;
        public delegate void OnPointsUpdated(int points, Color Color);

        int IPoolObject.Id
        {
            get => _id;
        }
        bool IPoolObject.IsUsed
        {
            get => _isUsed;
        }
        void IPointText.Init(int id, Vector2 totalPointsPos, Vector2Int actualScreenSize, OnPointsUpdated onPointsUpdated)
        {
            _id = id;
            _textComponent = gameObject.GetComponent<Text>();
            _totalPointsPos = totalPointsPos;
            _actualScreenSize = actualScreenSize;
            _onPointsUpdated = onPointsUpdated;
            _rt = this.gameObject.GetComponent<RectTransform>();
        }
        void IPoolObject.Show() => InternalShow(true);
        void IPoolObject.Hide() => InternalShow(false);
        void IPointText.ChangeValue(int points, Color color)
        {
            _pointsValue = points;
            _textComponent.text = _pointsValue.ToString();
            _currentColor = color;
            _textComponent.color = color;
        }
        public void ShowOnScreen(Vector3 ballsCenterPosition, Vector2 reflectDir)
        {
            __world2d.ShowOnScreen(ref _rt, ballsCenterPosition, _actualScreenSize, isAtCenter: false);

            var multiplier = _actualScreenSize.x / 4;
            reflectDir = new Vector2(reflectDir.x, -1);
            Vector2 reflectToPos = _rt.anchoredPosition + (reflectDir * multiplier);

            float timeDiff = UnityEngine.Random.Range(0.15f, 0.5f);

            AnimateShowNumber(reflectToPos, timeDiff);
            AnimateIncreaseSize(timeDiff);
        }

        private void InternalShow(bool show = true)
        {
            _isUsed = show;
            _textComponent.gameObject.SetActive(show);
            //
            Trail.gameObject.SetActive(false);
        }

        private void AnimateShowNumber(Vector2 reflectToPos, float timeDiff)
        {
            _reflectTweenId = LeanTween.move(_rt,
                reflectToPos,
                timeDiff
                ).id;
            LeanTween.descr(_reflectTweenId.Value).setEase(LeanTweenType.easeOutExpo);
            LeanTween.descr(_reflectTweenId.Value).setOnComplete(() =>
            {
                AnimateAddNumberToTotal();
                ShowPointsTrail();
            });
        }

        private void AnimateIncreaseSize(float timeDiff)
        {
            float smallestSize = _actualScreenSize.x * 0.02f;
            float biggestSize = _actualScreenSize.x * 0.6f;

            _sizeTweenId = LeanTween.value(gameObject,
                smallestSize,
                biggestSize,
                timeDiff
                ).id;
            LeanTween.descr(_sizeTweenId.Value).setEase(LeanTweenType.linear);
            LeanTween.descr(_sizeTweenId.Value).setOnUpdate((float value) =>
            {
                _rt.sizeDelta = new Vector2(value, value);
            });
            LeanTween.descr(_sizeTweenId.Value).setOnComplete(() =>
            {
                _sizeTweenId = null;
                AnimateDecreaseSize(timeDiff);
            });
        }

        private void AnimateDecreaseSize(float timeDiff)
        {
            float smallestSize = _actualScreenSize.x * 0.03f;
            float biggestSize = _actualScreenSize.x * 0.7f;

            _sizeTweenId = LeanTween.value(gameObject,
                    biggestSize,
                    smallestSize,
                    timeDiff
                    ).id;
            LeanTween.descr(_sizeTweenId.Value).setEase(LeanTweenType.easeInOutQuint);
            LeanTween.descr(_sizeTweenId.Value).setOnUpdate((float value) =>
            {
                _rt.sizeDelta = new Vector2(value, value);
            });
            LeanTween.descr(_sizeTweenId.Value).setOnComplete(() =>
            {
                _sizeTweenId = null;
            });
        }

        private void AnimateAddNumberToTotal()
        {
            _towardsTotalTweenId = LeanTween.move(_rt,
                _totalPointsPos,
                TOWARD_TOTAL_ANIM_TIME
                ).id;
            LeanTween.descr(_towardsTotalTweenId.Value).setEase(LeanTweenType.easeInOutQuart);
            LeanTween.descr(_towardsTotalTweenId.Value).setOnComplete(() =>
            {
                _onPointsUpdated(_pointsValue, _currentColor);
                var sound = new MusicOpts("MoneyPump", 1f, false);
                __.SFX.PlaySFX(sound);
                InternalShow(false);
            });

            var sound = new MusicOpts("MoneyTravel", 1f, false);
            __.SFX.PlaySFX(sound);
        }

        void ShowPointsTrail()
        {
            Color color = _currentColor;
            GradientColorKey colorKey = new GradientColorKey(color, 0.5f);
            GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
            alphaKeys[0] = new GradientAlphaKey(0, 0);
            alphaKeys[1] = new GradientAlphaKey(1, 1);
            var colorGradient = new Gradient();
            colorGradient.SetKeys(new GradientColorKey[1] { colorKey }, alphaKeys);

            Trail.colorGradient = colorGradient;
            Trail.gameObject.SetActive(true);
        }
    }
}
