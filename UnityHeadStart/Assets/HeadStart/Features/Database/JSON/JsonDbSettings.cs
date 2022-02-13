using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.HeadStart.Features.Database.JSON
{
    [CreateAssetMenu(fileName = "JsonDbSettings", menuName = "HeadStart/JsonDbSettings", order = 1)]
    public class JsonDbSettings : ScriptableObject
    {
        public TextAsset PlayerJson;
    }
}
