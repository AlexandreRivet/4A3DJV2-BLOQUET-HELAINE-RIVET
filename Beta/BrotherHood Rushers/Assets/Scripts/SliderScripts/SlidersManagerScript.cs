﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SlidersManagerScript : MonoBehaviour {

    [SerializeField]
    GameManagerScript _gameManager;

    [SerializeField]
    CharacterManager _characterManager;

    [SerializeField]
    Slider[] _sliders;

    [SerializeField]
    RectTransform[] _slidersRect;

    [SerializeField]
    Transform _sliderParent;

    [SerializeField]
    float _xLevelStart = 0.0f;

    [SerializeField]
    float _xLevelEnd = 0.0f;

    [SerializeField]
    float _scaleFactor = 0.1f;

    [SerializeField]
    float _offsetYSliders = 15.0f;

    [SerializeField]
    Button _markerModel;

    List<Marker>[] _markerList = new List<Marker>[3];
    List<GameObject>[] _markersArray = new List<GameObject>[3];

    float[] lastIndex = new float[] { 0, 0, 0 };

	// Use this for initialization
	void Start () 
    {
        // Sliders initialization
        if (_slidersRect.Length != 3)
            return;

        for (int i = 0; i < _slidersRect.Length; i++)
        {
            float x = _xLevelStart * (1 / _scaleFactor);
            float y = _slidersRect[i].anchoredPosition3D.y;
            float width = (1 / _scaleFactor) * (_xLevelEnd - _xLevelStart);
            float height = _slidersRect[i].rect.height;

            _slidersRect[i].sizeDelta = new Vector2(width, height);
            _slidersRect[i].anchoredPosition = new Vector2(x, y);

            // Debug.Log("(" + x + "," + y + ")"); -> Controle de la nouvelle position
        }

        for (int i = 0; i < _markersArray.Length; i++)
        {
            _markerList[i] = new List<Marker>();
            _markersArray[i] = new List<GameObject>();
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void sendPositionInScene(int index)
    {  
        // Calcul de la position dans la scène
        float value = _sliders[index].value;
        float position = value * (_xLevelEnd - _xLevelStart) + _xLevelStart;
        //TODO: faire un tableau de liste d'actions
        Action action_tmp =  new Action(index, new List<float>{position}, -1, new int[]{index}, "Move");
        _gameManager.addActionPlayers(index, action_tmp);
        
        // Création du marker
        createMarker(index, action_tmp, position);

        // Ajouter d'autres traitements ?
    }

    public void createMarker(int index, Action action, float position)
    {
        // Test si un bouton est deja sur la position, si oui, on ajoute juste l'action au bouton présent
        Marker _marker_Tmp;
        for (int i = 0; i < _markerList[index].Count(); i++)
        {
            _marker_Tmp = _markerList[index][i];
            if (Mathf.Abs(position - _marker_Tmp.getPosition()) < 0.5)
            {
                _marker_Tmp.addAction(action);
                return;
            }

        }
        
        // Création du button
        Button but = (Button)Instantiate(_markerModel);
        but.name = "Marker_" + index + "_" + lastIndex[index];
        but.transform.localScale = new Vector3(_scaleFactor, _scaleFactor, _scaleFactor);
        but.transform.SetParent(_sliderParent);
        _markersArray[index].Add(but.gameObject);
        // Placement du button
        RectTransform rect_transform = but.GetComponent<RectTransform>();
        rect_transform.pivot = new Vector2(0.5f, 0);
        rect_transform.anchoredPosition3D = new Vector3(0, 0, 0);
        rect_transform.anchoredPosition = new Vector2(position * (1 / _scaleFactor), -(index * _offsetYSliders));
        //TODO: petit problème de décalage dans les button trouver pourquoi mm si ça doit être lié au slider
        _marker_Tmp = new Marker(index, action, but.gameObject, position);
        _markerList[index].Add(_marker_Tmp);
        _markersArray[index].Add(but.gameObject);

        OnClickMarker functionCLick = but.gameObject.GetComponent<OnClickMarker>();
        functionCLick.setSliderManager(this);
        functionCLick.setMarker(_marker_Tmp);
        // Mise à jour du prochain ID
        lastIndex[index]++;
    }
    public void destroyMarker(GameObject marker)
    {
        int firstIndex = -1;
        int lastIndex = -1;
        List<Marker> markerList_tmp;
        Marker marker_tmp;
        GameObject objectMarker_tmp;
        for(int i = 0; i < 3; i++)
        {
            markerList_tmp = _markerList[i];
            for(int j = 0; j < markerList_tmp.Count; j++)
            {
                marker_tmp = markerList_tmp[j];
                objectMarker_tmp = marker_tmp.getMarker();
                if(objectMarker_tmp.Equals(marker))
                {
                    firstIndex = i;
                    lastIndex = j;
                    List<Action> actionList_tmp = marker_tmp.getActionList();
                    for (int k = 0; k < actionList_tmp.Count; k++)
                        _gameManager.removeAction(i, actionList_tmp[k]);
                    Destroy(objectMarker_tmp);
                    break;
                }
            }
            if (firstIndex != -1)
                break;
        }
        _markerList[firstIndex].RemoveAt(lastIndex); 
    }
    public void deleteAllMarkers()
    {
        for(int i = 0; i < _markersArray.Length; i++)
        {
            
            for (int j = 0; j < _markersArray[i].Count; j++)
            {
                Destroy(_markersArray[i][j]);
            }
               
        }
        _markersArray = new List<GameObject>[3];
        for (int i = 0; i < _markersArray.Length; i++)
        {
            _markersArray[i] = new List<GameObject>();
        }
    }
}
