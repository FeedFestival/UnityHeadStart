using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public static class __style
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
    private static Subject<bool> _calculateDomSub__ = new Subject<bool>();
    private static Subject<int> _eachStyleSub__;
    private static List<LayoutStyle> _layoutStyles;

    public static void Register(LayoutStyle layoutStyle)
    {
        if (_layoutStyles == null)
        {
            _layoutStyles = new List<LayoutStyle>();

            _calculateDomSub__
            .Throttle(TimeSpan.FromMilliseconds(10))
            .Subscribe((bool _) =>
            {
                Debug.Log("CalculateDom");
                CalculateDom();
            });
        }
        Debug.Log(layoutStyle.gameObject.name);
        _layoutStyles.Add(layoutStyle);
        _calculateDomSub__.OnNext(true);
    }

    public static void CalculateDom()
    {
        if (_layoutStyles == null)
        {
            return;
        }

        _eachStyleSub__ = new Subject<int>();
        _eachStyleSub__
        .Delay(TimeSpan.FromMilliseconds(10))
        .Subscribe((int index) =>
        {
            _layoutStyles[index].CalculateDom();
            Debug.Log("index: " + index);
            Debug.Log("_layoutStyles.Count: " + _layoutStyles.Count);
            if (index == _layoutStyles.Count - 1)
            {
                Clear();
            }
            else
            {
                int newIndex = index + 1;
                Debug.Log("newIndex: " + newIndex);
                _eachStyleSub__.OnNext(newIndex);
            }
        });

        _eachStyleSub__.OnNext(0);
    }

    private static void Clear()
    {
        _layoutStyles.Clear();
        _layoutStyles = null;

        _calculateDomSub__.Dispose();
        _eachStyleSub__.Dispose();
        _eachStyleSub__ = null;
    }
}
