using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.GameTester
{
    public class GameTestBase : MonoBehaviour
    {
        public float TimeScale = 2;
        public virtual void Test()
        {
            Debug.Log("Start Test");
        }

        public void InvokeButton(string buttonName)
        {
            var button = GetButtonByName(buttonName);

            if (button == null) { return; }

            button.onClick.Invoke();
        }

        public Button GetButtonByName(string buttonName)
        {
            var go = GetGo(buttonName);

            if (go == null) { return null; }

            return go.GetComponent<Button>();
        }

        public GameObject GetGo(string name)
        {
            return GameObject.Find(name);
        }
    }
}