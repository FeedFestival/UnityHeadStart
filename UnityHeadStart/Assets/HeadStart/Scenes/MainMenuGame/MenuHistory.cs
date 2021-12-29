using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHistory
{
    private Dictionary<int, VIEW> _history;
    private List<VIEW> _bannedViews;
    private int _depth;
    public MenuHistory(List<VIEW> bannedViews = null)
    {
        _bannedViews = bannedViews;
        _history = new Dictionary<int, VIEW>();
    }

    public void Push(VIEW view)
    {
        if (_history.ContainsValue(view) || (_bannedViews != null && _bannedViews.Contains(view)))
        {
            return;
        }
        _depth++;
        _history.Add(_depth, view);
    }

    public VIEW Pop()
    {
        VIEW view = _history[_depth];
        _history.Remove(_depth);
        _depth--;
        return view;
    }

    public int Depth()
    {
        return _depth;
    }

    public void Clear()
    {
        _depth = 0;
        _history.Clear();
    }
}
