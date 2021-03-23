using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[CustomEditor(typeof(ColorBank))]
public class ColorBankEditor : Editor
{
    private ColorBank _myScript;

    private bool _setupConfirm;

    public enum InspectorButton
    {
        RecalculateColors
    }

    private InspectorButton _actionTool;
    private InspectorButton _action
    {
        get { return _actionTool; }
        set
        {
            _actionTool = value;
            _setupConfirm = true;
        }
    }

    public override void OnInspectorGUI()
    {
        _myScript = (ColorBank)target;

        EditorGUILayout.BeginVertical("box");
        GUILayout.Space(5);

        GUILayout.Label("Colors");
        GUILayout.Space(5);

        if (GUILayout.Button("Recalculate Colors"))
            _action = InspectorButton.RecalculateColors;

        GUILayout.Space(5);
        EditorGUILayout.EndVertical();

        //--------------------------------------------------------------------------------------------------------------------------------------------------------
        GUILayout.Space(20);    // CONFIRM
                                //--------------------------------------------------------------------------------------------------------------------------------------------------------

        if (_setupConfirm)
        {
            EditorGUILayout.BeginVertical("box");
            GUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();

            GUILayout.FlexibleSpace();

            if (GUILayout.Button("Confirm", GUILayout.Width(Find(_percent: 25, _of: Screen.width)), GUILayout.Height(50)))
                ConfirmAccepted();

            if (GUILayout.Button("Cancel", GUILayout.Width(Find(_percent: 25, _of: Screen.width)), GUILayout.Height(50)))
                _setupConfirm = false;

            GUILayout.FlexibleSpace();

            EditorGUILayout.EndHorizontal();

            GUILayout.Space(5);
            EditorGUILayout.EndVertical();
        }

        // Show default inspector property editor
        DrawDefaultInspector();
    }

    private void ConfirmAccepted()
    {
        // Debug.Log(_action);
        switch (_action)
        {
            case InspectorButton.RecalculateColors:

                _myScript.CalculateColors();
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
        _setupConfirm = false;
    }
    private float Find(float _percent, float _of)
    {
        return (_of / 100f) * _percent;
    }
    private float What(float _is, float _of)
    {
        return (_is * 100f) / _of;
    }
}
