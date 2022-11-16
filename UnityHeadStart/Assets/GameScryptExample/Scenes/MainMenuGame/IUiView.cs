using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public interface IUiView
{
    GameObject GO();
    void Focus();
    UnityAction UiViewFocussed { get; }
}
