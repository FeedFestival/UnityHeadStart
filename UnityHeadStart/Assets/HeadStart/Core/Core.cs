using UnityEngine;

namespace Assets.HeadStart.Core
{
    public delegate void CoreCallback();
    public delegate void CoreNrCallback(float Id);
    public delegate void CoreIdCallback(int Id);
    public delegate void CoreNumbersCallback(params int[] list);
    public delegate void CoreIdNCallback(int? Id);
    public delegate void CoreObjCallback(object obj);

    public static class CoreReadonly
    {
        public static readonly float ROTATE_ANIM_TIME = 6f;
        public static readonly float ROTATE_ENABLE_PERCENT = 0.3f;
        public static readonly Vector3 FROM_ROTATE = new Vector3(0, 0, 0);
        public static readonly Vector3 ROTATE_TO = new Vector3(0, 0, 359.99f);
        public static readonly float MIN_SCALE_PERCENT = 0.2f;
    }

    public class CoreObservedValueBase
    {

    }
}