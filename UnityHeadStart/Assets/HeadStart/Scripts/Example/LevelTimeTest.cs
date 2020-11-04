using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.LevelService;
using UnityEngine;

public class LevelTimeTest : MonoBehaviour, ILevel
{
    public void StartLevel()
    {
        Debug.Log("Starting Level");
        Timer._.InternalWait(() =>
        {
            Debug.Log("Waited 1 Second. Should be Last");
        }, 1);

        Timer._.InternalWait(() =>
        {
            Debug.Log("Waited 0.5 Seconds. Should be First");
            Timer._.InternalWait(() =>
            {
                Debug.Log("Waited 0.2 Seconds, after 0.5 Seconds. Should be Second");
            }, 0.2f);
        }, 0.5f);

        Timer._.InternalWait(() =>
        {
            Debug.Log("Waited 0.8 Seconds. Should be Third");
        }, 0.8f);

        Timer._.InternalWait(() =>
        {
            Debug.Log("Waited 0.001 Seconds");
        }, 0.001f);

        Timer._.InternalWait(() =>
        {
            Debug.Log("Waited 0.002 Seconds");
        }, 0.002f);

        Timer._.InternalWait(() =>
        {
            Debug.Log("Waited 1 Frame");
        });
    }

}
