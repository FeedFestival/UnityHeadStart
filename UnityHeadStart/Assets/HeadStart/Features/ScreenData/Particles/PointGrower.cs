using System.Collections;
using Assets.HeadStart.Core;
using Assets.Scripts.utils;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.HeadStart.Features.ScreenData
{
    public class PointGrower : MonoBehaviour
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
        private RectTransform _rt;
        public Vector2 Pos;
        private Text _text;
        private CoreNrCallback _addPoints;
        float _time;
        float _splitAdd;
        float _addPoint;
        IEnumerator __setTimedPoints;
        int? _enlargeTweenId;
        int? _colorizeTweenId;
        private Vector2 _normalSize;
        private Vector2 _bigSize;
        //--------------------------- GAME CONSTANTS ------------------------
        private readonly float POINTS_ENLARGE_DURATION_SECONDS = 0.3f;

        internal void Init(
            WorldCanvasPoint pointsWCP,
            CoreNrCallback addPoints
        )
        {
            _addPoints = addPoints;

            _text = gameObject.GetComponent<Text>();
            _text.text = "";

            _rt = gameObject.GetComponent<RectTransform>();
            __world2d.PositionRtBasedOnScreenAnchors(
                pointsWCP, rt: _rt,
                screenSize: Main._.CoreCamera.CanvasRt.sizeDelta
            );
            _normalSize = _rt.sizeDelta;
            Pos = new Vector2(
                _rt.anchoredPosition.x + (_normalSize.x / 2),
                _rt.anchoredPosition.y + (_normalSize.y / 2)
            );
            _bigSize = new Vector2(_normalSize.x, _normalSize.y * 2);
        }

        public void UpdateScreenData(int toAdd, Color color)
        {
            SetupTweenVariables(toAdd);
            SetPoints();
            Enlarge();
            ColorizePoints(color);
        }

        private void SetPoints()
        {
            if (_splitAdd <= 0)
            {
                Finished();
                return;
            }
            if (__setTimedPoints != null)
            {
                __setTimedPoints = null;
            }
            __setTimedPoints = SetPointsCo();
            StartCoroutine(__setTimedPoints);
        }

        void SetupTweenVariables(int toAdd)
        {
            if (__setTimedPoints != null)
            {
                StopCoroutine(__setTimedPoints);
                __setTimedPoints = null;
                var lastAdditionalPoints = _splitAdd * _addPoint;

                _addPoints(lastAdditionalPoints);
            }
            if (toAdd > 10)
            {
                _splitAdd = 10;
                _addPoint = toAdd / _splitAdd;
            }
            else
            {
                _splitAdd = toAdd;
                _addPoint = 1;
            }
            _time = POINTS_ENLARGE_DURATION_SECONDS / _splitAdd;
        }

        private void Finished()
        {
            StopCoroutine(__setTimedPoints);
            __setTimedPoints = null;

            Enlarge(false);
            ColorizePoints(__gameColor.GetColorByName("White"));
        }

        IEnumerator SetPointsCo()
        {
            yield return new WaitForSeconds(_time);

            _splitAdd -= 1;
            _addPoints(_addPoint);
            SetPoints();
        }

        internal void WriteTotal(int total)
        {
            _text.text = total.ToString();
        }

        void Enlarge(bool enlarge = true)
        {
            if (_enlargeTweenId.HasValue)
            {
                LeanTween.cancel(_enlargeTweenId.Value);
            }

            _enlargeTweenId = LeanTween.size(_rt,
                enlarge == true ? _bigSize : _normalSize,
                POINTS_ENLARGE_DURATION_SECONDS
            ).id;
            LeanTween.descr(_enlargeTweenId.Value).setEase(LeanTweenType.linear);
        }

        void ColorizePoints(Color color)
        {
            if (_colorizeTweenId.HasValue)
            {
                LeanTween.cancel(_colorizeTweenId.Value);
            }

            _colorizeTweenId = LeanTween.colorText(_rt,
                color,
                POINTS_ENLARGE_DURATION_SECONDS
                ).id;
            LeanTween.descr(_colorizeTweenId.Value).setEase(LeanTweenType.linear);
        }
    }
}
