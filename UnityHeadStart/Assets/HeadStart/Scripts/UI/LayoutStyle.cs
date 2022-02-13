using Assets.Scripts.utils;
using MyBox;
using UnityEngine;

public class LayoutStyle : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
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
    [Separator("Position Fixed (Percentages)")]
    public bool PositionFixed;
    [ConditionalField(nameof(PositionFixed), false, true)]
    public float PF_Top;
    [ConditionalField(nameof(PositionFixed), false, true)]
    public float PF_Left;
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
            if (debug) Debug.Log("newSizeDelta: " + newSizeDelta);
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
                if (debug) Debug.Log("newAnchoredPosition: " + newAnchoredPosition);
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

        if (PositionFixed)
        {
            _rt.anchoredPosition = new Vector2(
                __percent.Find(PF_Left, parentRect.width),
                __percent.Find(PF_Top, parentRect.height)
            );
        }
    }
}
