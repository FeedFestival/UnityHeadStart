using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.utils;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

[CustomEditor(typeof(DomainLogic))]
public class DomainLogicEditor : Editor
{
    private DomainLogic _myScript;

    private bool _setupConfirm;

    public enum InspectorButton
    {
        DeleteDataBase, RecreateDataBase, CleanUpUsers, LoadLevelsCsv, UpdateMap, CreateSpecialityTable
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
        _myScript = (DomainLogic)target;

        EditorGUILayout.BeginVertical("box");
        GUILayout.Space(5);

        GUILayout.Label("Database");
        GUILayout.Space(5);

        if (GUILayout.Button("Delete Database"))
            _action = InspectorButton.DeleteDataBase;
        if (GUILayout.Button("Recreate Database"))
            _action = InspectorButton.RecreateDataBase;

        if (GUILayout.Button("Clean Up Users"))
            _action = InspectorButton.CleanUpUsers;

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

            if (GUILayout.Button("Confirm", GUILayout.Width(__percent.Find(_percent: 25, _of: Screen.width)), GUILayout.Height(50)))
                ConfirmAccepted();

            if (GUILayout.Button("Cancel", GUILayout.Width(__percent.Find(_percent: 25, _of: Screen.width)), GUILayout.Height(50)))
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
        Debug.Log(_action);
        switch (_action)
        {
            case InspectorButton.DeleteDataBase:

                _myScript.DeleteDataBase();
                break;

            case InspectorButton.RecreateDataBase:

                _myScript.RecreateDataBase();
                break;

            case InspectorButton.CleanUpUsers:

                _myScript.CleanUpUsers();
                break;

            default:
                throw new ArgumentOutOfRangeException();
        }
        _setupConfirm = false;
    }
}
