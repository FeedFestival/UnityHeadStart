using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScrypt.GSCamera {
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
        public Canvas Canvas;
        public RectTransform Views;
        public Image LoadingOverlay;
        // TODO: add Logo functionality to HeadStart
        public Transform LogoT;
        protected Camera _camera;
        protected float _currentCameraSize;
        protected float _multiplierCameraSize;
        protected float _toCameraSize;
        protected Vector3 _toLogoSize;
        protected int? _alignCameraToHelperTwid;
        protected int? _enlargeLogoTwid;
        protected const float HIDE_LOGO_TIME = 0.77f;
        protected readonly Vector3 TO_LOGO_SIZE = new Vector3(44, 44, 44);
        protected bool _foundTheSweetSpot;
        protected bool _debugActivated;
        protected bool _foundBoundsFlag = false;
        protected float _zoomedOutCount;
    }
}
