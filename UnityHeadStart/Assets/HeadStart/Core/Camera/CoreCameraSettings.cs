using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CoreCameraSettings", order = 1)]
public class CoreCameraSettings : ScriptableObject
{
    [Header("Prefabs")]
    public GameObject IngameDebugConsole;
}
