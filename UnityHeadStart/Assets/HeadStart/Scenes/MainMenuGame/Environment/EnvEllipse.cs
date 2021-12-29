using UnityEngine;

public class EnvEllipse : MonoBehaviour
{
    private int? _rotateEllipseTwid;
    public float ELLIPSE_TIME_RNDM_A;
    public float ELLIPSE_TIME_RNDM_B;
    public Vector3 FromRot;
    public Vector3 ToRot;

    public void Play()
    {
        transform.eulerAngles = FromRot;
        float time = Random.Range(ELLIPSE_TIME_RNDM_A, ELLIPSE_TIME_RNDM_B);
        LeanTweenType tweenType = (LeanTweenType)((int)(Random.Range(1, 12)));
        _rotateEllipseTwid = LeanTween.rotateAround(
            gameObject,
            Vector3.forward,
            ToRot.z,
            time
        ).id;
        LeanTween.descr(_rotateEllipseTwid.Value).setEase(tweenType);
        LeanTween.descr(_rotateEllipseTwid.Value).setOnComplete(() =>
        {
            Play();
        });
    }
}
