using Assets.HeadStart.Core;
using Assets.HeadStart.Core.SFX;
using DentedPixel;
using UnityEngine;

public delegate void Clicked();

public class GameButton : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.2.0";
#pragma warning restore 0414 //
    public Color EnableColor;
    public Color DisableColor;
    public ActionDisabler ActionDisabler;
    private bool _interactable = true;
    private SpriteRenderer _sprite;
    private Vector3 _defaultSize;
    private Vector3 _defaultLocalPos;
    private Vector3 _minSize;
    private Vector3 _minLocalPos;
    private int? _scaleButtonTwid;

    public static readonly float ROTATE_ANIM_TIME = 6f;
    public static readonly float ROTATE_ENABLE_PERCENT = 0.3f;
    public static readonly Vector3 FROM_ROTATE = new Vector3(0, 0, 0);
    public static readonly Vector3 ROTATE_TO = new Vector3(0, 0, 359.99f);
    public static readonly float MIN_SCALE_PERCENT = 0.2f;

    public bool Interactable
    {
        get { return _interactable; }
        set
        {
            _interactable = value;
            if (_interactable)
            {
                if (ActionDisabler)
                {
                    ActionDisabler.Enable();
                }
                EnableButton();
            }
            else
            {
                if (ActionDisabler)
                {
                    ActionDisabler.Disable();
                }
                DisableButton();
            }
        }
    }
    private Clicked _clicked;

    public void Init()
    {
        if (_sprite == null)
        {
            _sprite = GetComponent<SpriteRenderer>();
        }
        _defaultSize = transform.localScale;
        _defaultLocalPos = transform.localPosition;
        _minLocalPos = new Vector3(_defaultLocalPos.x, _defaultLocalPos.y, 0);
        _minSize = new Vector3(
            _defaultSize.x * GameButton.MIN_SCALE_PERCENT,
            _defaultSize.y * GameButton.MIN_SCALE_PERCENT,
            _defaultSize.z * GameButton.MIN_SCALE_PERCENT
        );
    }

    public void OnClick(Clicked clicked)
    {
        _clicked = clicked;
    }

    void OnMouseDown()
    {
        MouseDown();
    }

    public void MouseDown()
    {
        if (Interactable == false)
        {
            return;
        }
        MusicOpts mOpts = new MusicOpts("Click", loop: false);
        //__.SFX.PlaySFX(mOpts);
        _clicked();
    }

    public void Disable()
    {
        _interactable = false;
        if (ActionDisabler)
        {
            ActionDisabler.Disable();
        }
        DisableButton();
    }

    private void DisableButton()
    {
        if (_sprite == null)
        {
            return;
        }
        _sprite.color = DisableColor;
        transform.localPosition = _minLocalPos;

        _scaleButtonTwid = LeanTween.scale(
            gameObject,
            _minSize,
            GameButton.ROTATE_ANIM_TIME * GameButton.ROTATE_ENABLE_PERCENT
        ).id;
        LeanTween.descr(_scaleButtonTwid.Value).setEase(LeanTweenType.easeOutElastic);
    }

    public void Enable()
    {
        _interactable = true;
        if (ActionDisabler)
        {
            ActionDisabler.Enable();
        }
        EnableButton();
    }

    private void EnableButton()
    {
        if (_sprite == null)
        {
            return;
        }

        _sprite.color = EnableColor;
        transform.localPosition = _defaultLocalPos;

        _scaleButtonTwid = LeanTween.scale(
            gameObject,
            _defaultSize,
            GameButton.ROTATE_ANIM_TIME * GameButton.ROTATE_ENABLE_PERCENT
        ).id;
        LeanTween.descr(_scaleButtonTwid.Value).setEase(LeanTweenType.easeOutElastic);
    }

    public void Reset()
    {
        ActionDisabler.Reset();
        Interactable = false;
    }
}
