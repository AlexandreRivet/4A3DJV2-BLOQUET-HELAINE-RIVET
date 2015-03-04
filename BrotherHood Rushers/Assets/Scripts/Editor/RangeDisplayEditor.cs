using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

//------------------- OUTIL DE DEBUG DE DISTANCE ------------------------//
public class RangeDisplayEditor : EditorWindow
{
    private GameObject[] _selectedGameObjects = new GameObject[0];
    private Transform[] _selectedTransforms = null;
    private bool[] _isDisplay;
    private Vector2 _scrollPosition;
    private float _distance = 0.0f;
    private float _rangePreview = 0.0f;
    [MenuItem("Window/RangeEditor")]
    static void Init()
    {
        // Get existing open window or if none, make a new one:
        EditorWindow.GetWindow(typeof(RangeDisplayEditor));
    }
    void OnSelectionChange()
    {
        _selectedGameObjects = Selection.gameObjects;
        _selectedTransforms = new Transform[_selectedGameObjects.Length];
        for (int i = 0; i < _selectedGameObjects.Length; i++)
        {
            _selectedTransforms[i] = _selectedGameObjects[i].transform;
        }
        
        _isDisplay = new bool[_selectedTransforms.Length];
        for (int i = 0; i < _isDisplay.Length; i++)
        {
            _isDisplay[i] = false;
        }
        Repaint();
    }
    void OnGUI()
    {
        _scrollPosition = GUILayout.BeginScrollView(_scrollPosition, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
        {
            EditorGUILayout.LabelField("Select One or Two Object(s)");
            EditorGUILayout.Space();

            if (_selectedTransforms != null && _selectedTransforms.Length == 1)
            {
                displayTransformSelected();
                _rangePreview = EditorGUILayout.Slider("Range Preview: ", _rangePreview, 0.0f, 100.0f);
                SceneView.lastActiveSceneView.Repaint();

            }
            else if (_selectedTransforms != null && _selectedTransforms.Length == 2)
            {
                displayTransformSelected();
                _distance = Vector3.Distance(_selectedTransforms[0].position, _selectedTransforms[1].position);
                float distancex = Math.Abs(_selectedTransforms[0].position.x - _selectedTransforms[1].position.x);
                float distancey = Math.Abs(_selectedTransforms[0].position.y - _selectedTransforms[1].position.y);
                EditorGUILayout.LabelField("Distance:", _distance.ToString());
                EditorGUILayout.LabelField("Distance X:", distancex.ToString());
                EditorGUILayout.LabelField("Distance Y:", distancey.ToString());
            }
                
        }
        EditorGUILayout.EndScrollView();
    }
    void drawCircleRange()
    {
        if (_rangePreview == 0 || _selectedTransforms.Length != 1)
            return;
        Handles.color = new Color(1.0f, 0.0f, 0.0f, 0.10f);
        Handles.DrawSolidDisc(_selectedTransforms[0].position, Vector3.forward, _rangePreview);
        Handles.color = Color.red;
    }
    void OnFocus()
    {
        //Permet le focus de la scène View
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;

        SceneView.onSceneGUIDelegate += this.OnSceneGUI;
    }

    void OnDestroy()
    {
        SceneView.onSceneGUIDelegate -= this.OnSceneGUI;
    }

    void OnSceneGUI(SceneView sceneView)
    {
        //Dessin du cercle de portée
        drawCircleRange();
    }
    
	void displayTransformSelected()
    {
        for(int i = 0; i < _selectedTransforms.Length; i++)
        {
            _isDisplay[i] = EditorGUILayout.Foldout(_isDisplay[i], _selectedGameObjects[i].name);
            if(_isDisplay[i])
            {
                EditorGUI.indentLevel++;
                _selectedTransforms[i].position = EditorGUILayout.Vector3Field("Position :", _selectedTransforms[i].position);
                EditorGUI.indentLevel--;
            }
        }
       
    }
}
