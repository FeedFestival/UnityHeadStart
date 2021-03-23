using System.Collections;
using UnityEngine;
public interface IUiView
{
    GameObject Gobject { get; }
    void OnShow();
}
