using Assets.HeadStart.Core;
using Assets.HeadStart.Core.SFX;
using UnityEngine;

public class GameButton : MonoBehaviour
{
    private bool _interactable = true;
    public bool Interactable
    {
        get { return _interactable; }
        set
        {
            _interactable = value;
            if (_interactable)
            {
                // stop the animation
            }
            else
            {
                // start the animation
            }
        }
    }
    private Clicked _clicked;
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
}