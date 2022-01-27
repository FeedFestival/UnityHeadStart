using Assets.Scripts.utils;
using MyBox;
using UnityEngine;

public class LayoutStyle : MonoBehaviour
{
    public bool debug;
    [Separator("Size")]
    public bool Size;
    [ConditionalField(nameof(Size), false, true)]
    public float Width;
    [ConditionalField(nameof(Size), false, true)]
    public float Height;

    [Separator("Margins")]
    public bool Margins;
    [ConditionalField(nameof(Margins), false, true)]
    public float MarginTop;

    [Separator("Position Absolute (Percentages)")]
    public bool PositionAbsolute;
    [ConditionalField(nameof(PositionAbsolute), false, true)]
    public float Top;
    [ConditionalField(nameof(PositionAbsolute), false, true)]
    public float Left;
    private RectTransform _rt;
    private RectTransform _parentRt;

    void Start()
    {
        __style.Register(this);
    }

    public void CalculateDom()
    {
        _rt = (transform as RectTransform);
        _parentRt = (transform.parent as RectTransform);
        var parentRect = RectTransformUtility.PixelAdjustRect(_parentRt, Main._.CoreCamera.Canvas);

        if (Size)
        {
            Vector2 newSizeDelta = new Vector2(
                Width > 0 ? __percent.Find(Width, parentRect.width) : parentRect.width,
                Height > 0 ? __percent.Find(Height, parentRect.height) : parentRect.height
            );
            if (debug)
            {
                Debug.Log("newSizeDelta: " + newSizeDelta);
            }
            _rt.sizeDelta = newSizeDelta;
        }

        if (PositionAbsolute)
        {
            if (Top > 0)
            {
                _rt.anchoredPosition = new Vector2(
                    _rt.anchoredPosition.x,
                    0 - __percent.Find(Top, parentRect.height)
                );
            }
            else if (Top < 0)
            {
                Top = Mathf.Abs(Top);
                _rt.anchoredPosition = new Vector2(
                    _rt.anchoredPosition.x,
                    __percent.Find(Top, parentRect.height)
                );
            }

            if (Left > 0)
            {
                var newAnchoredPosition = new Vector2(
                    __percent.Find(Left, parentRect.width),
                    _rt.anchoredPosition.y
                );
                if (debug)
                {
                    Debug.Log("newAnchoredPosition: " + newAnchoredPosition);
                }
                _rt.anchoredPosition = newAnchoredPosition;
            }
            else
            {
                _rt.anchoredPosition = new Vector2(
                    0 - __percent.Find(Left, parentRect.width),
                    _rt.anchoredPosition.y
                );
            }
        }
    }
}
