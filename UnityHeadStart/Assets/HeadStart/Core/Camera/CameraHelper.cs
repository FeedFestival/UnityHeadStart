using UnityEngine;

public class CameraHelper : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "2.0.8";
#pragma warning restore 0414 //
    public Transform TLIn;
    public Transform TRIn;
    public Transform BLIn;
    public Transform BRIn;

    public bool CanSeeBounds()
    {
        Vector3 tLp = Camera.main.WorldToViewportPoint(TLIn.position);
        Vector3 tRp = Camera.main.WorldToViewportPoint(TRIn.position);
        Vector3 bLp = Camera.main.WorldToViewportPoint(BLIn.position);
        Vector3 bRp = Camera.main.WorldToViewportPoint(BRIn.position);

        return isInView(tLp) && isInView(tRp) && isInView(bLp) && isInView(bRp);
    }

    private bool isInView(Vector3 screenPos)
    {
        bool inView = (screenPos.z > 0
            && screenPos.x > 0
            && screenPos.x < 1
            && screenPos.y > 0
            && screenPos.y < 1);
        return inView;
    }
}
