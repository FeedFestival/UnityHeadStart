using UnityEngine;

public class CameraResolution : MonoBehaviour
{
#pragma warning disable 0414 // private field assigned but not used.
    public static readonly string _version = "1.0.3";
#pragma warning restore 0414 //
    private static CameraResolution _cameraResolution;
    public static CameraResolution _ { get { return _cameraResolution; } }
    [HideInInspector]
    public int ScreenSizeX = 0;
    [HideInInspector]
    public int ScreenSizeY = 0;
    public bool IsPortrait;
    public Vector2 TargetAspect = new Vector2(16, 9);

    void Awake()
    {
        _cameraResolution = this;
    }

    void Start()
    {
        RescaleCamera();

        // float currAspect = 1.0f * Screen.width / Screen.height;
        // Debug.Log(Camera.main.projectionMatrix);
        // Debug.Log(baseAspect + ", " + currAspect + ", " + baseAspect / currAspect);
        // Camera.main.projectionMatrix = Matrix4x4.Scale(new Vector3(currAspect / baseAspect, 1.0f, 1.0f)) * Camera.main.projectionMatrix;
    }

    private void RescaleCamera()
    {
        if (Screen.width == ScreenSizeX && Screen.height == ScreenSizeY) return;

        if (IsPortrait == false)
        {
            float targetaspect = TargetAspect.x / TargetAspect.y;
            float windowaspect = (float)Screen.width / (float)Screen.height;
            float scaleheight = windowaspect / targetaspect;
            Camera camera = GetComponent<Camera>();

            if (scaleheight < 1.0f)
            {
                Rect rect = camera.rect;

                rect.width = 1.0f;
                rect.height = scaleheight;
                rect.x = 0;
                rect.y = (1.0f - scaleheight) / 2.0f;

                camera.rect = rect;
            }
            else // add pillarbox
            {
                float scalewidth = 1.0f / scaleheight;

                Rect rect = camera.rect;

                rect.width = scalewidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scalewidth) / 2.0f;
                rect.y = 0;

                camera.rect = rect;
            }
        }
        else
        {
            float targetaspect = TargetAspect.y / TargetAspect.x;
            float windowaspect = (float)Screen.height / (float)Screen.width;
            float scaleheight = windowaspect / targetaspect;
            Camera camera = GetComponent<Camera>();
        }

        ScreenSizeX = Screen.width;
        ScreenSizeY = Screen.height;
    }

    void OnPreCull()
    {
        if (Application.isEditor) return;
        Rect wp = Camera.main.rect;
        Rect nr = new Rect(0, 0, 1, 1);

        Camera.main.rect = nr;
        GL.Clear(true, true, Color.black);

        Camera.main.rect = wp;

    }

    // Update is called once per frame
    void Update()
    {
        RescaleCamera();
    }
}