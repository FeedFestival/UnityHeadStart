using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScrypt.GSCamera
{
    /// <summary>
    /// Class <c>GSCamera</c> is the main Camera in the scene.
    /// <para>
    /// </para>
    /// <seealso>v.3.0.0</seealso>
    /// </summary>
    public class GSCamera : MonoBehaviour
    {
        public static readonly float CAMERA_SETUP_TIME = 8f;
        public RectTransform CanvasRt;
        [HideInInspector]
        public RectTransform Views;
        public Image LoadingOverlay;
        public GSLogo GSLogo;
        public GSLoading LoadingVersion;
        public float CurrentCameraSize;
        public float OrthographicSize { set { _camera.orthographicSize = value; } get { return _camera.orthographicSize; } }
        protected Camera _camera;
        protected bool _debugActivated;
        protected float _zoomedOutCount;
    }
}
