using System;
using Assets.HeadStart.Core;
using DentedPixel;
using UnityEngine;

public class ActionDisabler : MonoBehaviour
{
    private int? _rotateEllipseTwid;
    private bool _enableWhenAnimFinish;
    private float _rotateAnimTime;

    internal void Disable()
    {
        if (_rotateEllipseTwid.HasValue)
        {
            return;
        }

        _rotateAnimTime = CoreReadonly.ROTATE_ANIM_TIME;
        transform.eulerAngles = CoreReadonly.FROM_ROTATE;
        _rotateEllipseTwid = LeanTween.rotateAround(
            gameObject,
            Vector3.forward,
            CoreReadonly.ROTATE_TO.z,
            _rotateAnimTime
        ).id;
        LeanTween.descr(_rotateEllipseTwid.Value).setEase(LeanTweenType.linear);
        LeanTween.descr(_rotateEllipseTwid.Value).setOnComplete(() =>
        {
            if (_enableWhenAnimFinish)
            {
                _enableWhenAnimFinish = false;
                return;
            }
            _rotateEllipseTwid = null;
            Disable();
        });
    }

    internal void Enable()
    {
        _enableWhenAnimFinish = true;
        if (_rotateEllipseTwid.HasValue)
        {
            _rotateAnimTime = CoreReadonly.ROTATE_ANIM_TIME * CoreReadonly.ROTATE_ENABLE_PERCENT;
            LTDescr ltd = LeanTween.descr(_rotateEllipseTwid.Value);
            if (ltd == null)
            {
                _rotateEllipseTwid = null;
                return;
            }
            ltd.setTime(_rotateAnimTime);
        }
    }

    internal void Reset()
    {
        if (_rotateEllipseTwid.HasValue)
        {
            LTDescr ltd = LeanTween.descr(_rotateEllipseTwid.Value);
            if (ltd != null) {
                LeanTween.cancel(_rotateEllipseTwid.Value);
            }
            _rotateEllipseTwid = null;
        }
        _enableWhenAnimFinish = false;
    }
}
