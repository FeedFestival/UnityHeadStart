using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.Scripts.utils
{
    public static class __utils
    {
#pragma warning disable 0414 // private field assigned but not used.
        public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
        public static string ConvertNumberToK(int num)
        {
            if (num >= 1000)
                return string.Concat(num / 1000, "k");
            else
                return num.ToString();
        }

        public static Color SetColorAlpha(Color color, int value)
        {
            Color tempColor = color;
            tempColor.a = GetAlphaValue(value);
            return tempColor;
        }

        public static float GetAlphaValue(int value)
        {
            var perc = __percent.What(value, 255);
            return perc * 0.01f;
        }

        public static int GetRGBAAlphaValue(float value)
        {
            float perc = value * 100;
            return (int)__percent.Find(perc, 255);
        }

        public static void AddIfNone(int value, ref List<int> array, string debugAdd = null)
        {
            if (array.Contains(value))
            {
                return;
            }
            array.Add(value);
            if (string.IsNullOrEmpty(debugAdd) == false)
            {
                Debug.Log(debugAdd);
            }
        }

        public static int CreateLayerMask(bool aExclude, params int[] aLayers)
        {
            int v = 0;
            foreach (var L in aLayers)
                v |= 1 << L;
            if (aExclude)
                v = ~v;
            return v;
        }
    }

    public static class __percent
    {
        public static float Find(float _percent, float _of)
        {
            return (_of / 100f) * _percent;
        }
        public static float What(float _is, float _of)
        {
            return (_is * 100f) / _of;
        }

        public static int PennyToss(int _from = 0, int _to = 100)
        {
            var randomNumber = Random.Range(_from, _to);
            return (randomNumber > 50) ? 1 : 0;
        }

        public static T GetRandomFromArray<T>(T[] list)
        {
            List<int> percentages = new List<int>();
            int splitPercentages = Mathf.FloorToInt(100 / list.Length);
            int remainder = 100 - (splitPercentages * list.Length);
            for (int i = 0; i < list.Length; i++)
            {
                int percent = i == (list.Length - 1) ? splitPercentages + remainder : splitPercentages;
                percentages.Add(percent);
            }
            for (int i = 1; i < percentages.Count; i++)
            {
                percentages[i] = percentages[i - 1] + percentages[i];
            }
            int randomNumber = UnityEngine.Random.Range(0, 100);
            int index = percentages.FindIndex(p => randomNumber < p);
            percentages = null;
            return list[index];
        }

        public static T GetRandomFromList<T>(List<T> list)
        {
            List<int> percentages = new List<int>();
            int splitPercentages = Mathf.FloorToInt(100 / list.Count);
            int remainder = 100 - (splitPercentages * list.Count);
            for (int i = 0; i < list.Count; i++)
            {
                int percent = i == (list.Count - 1) ? splitPercentages + remainder : splitPercentages;
                percentages.Add(percent);
            }
            for (int i = 1; i < percentages.Count; i++)
            {
                percentages[i] = percentages[i - 1] + percentages[i];
            }
            int randomNumber = UnityEngine.Random.Range(0, 100);
            int index = percentages.FindIndex(p => randomNumber < p);
            percentages = null;
            return list[index];
        }
    }

    public static class __world2d
    {
        public static Vector2 GetNormalizedDirection(Vector2 lastVelocity, Vector2 collisionNormal)
        {
            return Vector2.Reflect(lastVelocity.normalized, collisionNormal).normalized;
        }

        public static Vector3 LookRotation2D(Vector2 from, Vector2 to, bool fromFront = false)
        {
            Vector2 vectorToTarget = to - from;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            if (fromFront)
            {
                return new Vector3(q.eulerAngles.x, q.eulerAngles.y, q.eulerAngles.z - 90);
            }
            return q.eulerAngles;
        }

        public static void PositionRtBasedOnScreenAnchors(
            Vector3 topLeftAnchor,
            Vector3 bottomRightAnchor,
            RectTransform rt,
            Vector2 screenSize,
            Vector2? pivot
        )
        {
            var left = topLeftAnchor.x;
            var top = bottomRightAnchor.y;
            var width = Mathf.Abs(topLeftAnchor.x - bottomRightAnchor.x);
            var height = Mathf.Abs(topLeftAnchor.y - bottomRightAnchor.y);

            if (pivot.HasValue)
            {
                rt.pivot = Vector2.one / 2;
            }
            else
            {
                rt.pivot = Vector2.zero;
            }

            rt.anchorMax = Vector2.zero;
            rt.anchorMin = Vector2.zero;

            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;

            rt.SetLeft(left);

            rt.offsetMax = new Vector2(
                rt.offsetMin.x + rt.offsetMax.x + width,
                rt.offsetMin.y + (height + top)
            );

            rt.offsetMin = new Vector2(
                rt.offsetMin.x,
                rt.offsetMax.y + rt.offsetMin.y - height
            );
        }

        public static void PositionRtBasedOnScreenAnchors(
                    WorldCanvasPoint worldCanvasPoint,
                    RectTransform rt,
                    Vector2 screenSize,
                    Vector2? pivot = null
                )
        {
            PositionRtBasedOnScreenAnchors(
                Camera.main.WorldToScreenPoint(worldCanvasPoint.transform.position),
                Camera.main.WorldToScreenPoint(worldCanvasPoint.BottomRightPoint.position),
                rt, screenSize, pivot
            );
        }

        public static void ShowOnScreen(
            ref RectTransform rt,
            Vector3 worldPosition,
            Vector2 canvasSize,
            bool isAtCenter = true
        )
        {
            rt.anchoredPosition = GetWorldObjScreenPos(worldPosition, canvasSize, isAtCenter);
        }

        // then you calculate the position of the UI element
        // 0,0 for the canvas is at the center of the screen, whereas WorldToViewPortPoint treats the lower left corner as 0,0.
        // Because of this, you need to subtract the height / width of the canvas * 0.5 to get the correct position.
        public static Vector2 GetWorldObjScreenPos(Vector3 worldPosition, Vector2 canvasSize, bool isAtCenter = true)
        {
            var viewportPosition = Camera.main.WorldToViewportPoint(worldPosition);
            Vector2 worldObjectScreenPosition = new Vector2(
                ((viewportPosition.x * canvasSize.x) - (canvasSize.x * 0.5f)),
                ((viewportPosition.y * canvasSize.y) - (canvasSize.y * 0.5f))
            );
            if (isAtCenter == false)
            {
                worldObjectScreenPosition = new Vector2(
                    ((viewportPosition.x * canvasSize.x)),
                    ((viewportPosition.y * canvasSize.y))
                );
            }
            return worldObjectScreenPosition;
        }

        public static void SetPivot(ref RectTransform rectTransform, Vector2 pivot)
        {
            if (rectTransform == null) return;

            Vector2 size = rectTransform.rect.size;
            Vector2 deltaPivot = rectTransform.pivot - pivot;
            Vector3 deltaPosition = new Vector3(deltaPivot.x * size.x, deltaPivot.y * size.y);
            rectTransform.pivot = pivot;
            rectTransform.localPosition -= deltaPosition;
        }

        public static Vector2 GetCenterPosition(List<Vector2> positions)
        {
            Vector2 groupVectors = Vector2.zero;
            foreach (Vector2 pos in positions)
            {
                groupVectors += pos;
            }
            return groupVectors / positions.Count;
        }
    }
}
