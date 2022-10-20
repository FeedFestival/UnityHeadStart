using System.Collections.Generic;
using UnityEngine;
using GameScrypt.GSUtils.DataModels;

namespace GameScrypt.GSUtils
{
    public static class GSWorld2D
    {
#pragma warning disable 0414
        public static readonly string _version = "3.0.0";
#pragma warning restore 0414

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
