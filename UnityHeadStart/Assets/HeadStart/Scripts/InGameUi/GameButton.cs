using System;
using Assets.HeadStart.Core;
using Assets.HeadStart.Core.SFX;
using UnityEngine;

public class GameButton : MonoBehaviour
{
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
            _defaultSize.x * CoreReadonly.MIN_SCALE_PERCENT,
            _defaultSize.y * CoreReadonly.MIN_SCALE_PERCENT,
            _defaultSize.z * CoreReadonly.MIN_SCALE_PERCENT
        );
    }

    public void OnClick(Clicked clicked)
    {
        _clicked = clicked;
    }

    void OnMouseDown()
    {
        if (Interactable == false)
        {
            return;
        }
        MusicOpts mOpts = new MusicOpts("Click");
        __.SFX.PlaySound(mOpts);
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
            CoreReadonly.ROTATE_ANIM_TIME * CoreReadonly.ROTATE_ENABLE_PERCENT
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
            CoreReadonly.ROTATE_ANIM_TIME * CoreReadonly.ROTATE_ENABLE_PERCENT
        ).id;
        LeanTween.descr(_scaleButtonTwid.Value).setEase(LeanTweenType.easeOutElastic);
    }

    internal void Reset()
    {
        ActionDisabler.Reset();
        Interactable = false;
    }
}
