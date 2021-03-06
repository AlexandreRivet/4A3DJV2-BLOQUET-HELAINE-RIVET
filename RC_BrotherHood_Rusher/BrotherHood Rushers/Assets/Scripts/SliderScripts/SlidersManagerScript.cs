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
    List<Button> _ButtonMarkerActive = new List<Button>();

    [SerializeField]
    List<Button> _ButtonMarkerAvailable;

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

    [SerializeField]
    GameObject _panelMarker;

    [SerializeField]
    GameObject[] _actionPanel;

    [SerializeField]
    Text[] _actionLabel;

    Marker _markerActive = null;
    Button _markerObjectCurrent = null;

    List<Marker>[] _markerList = new List<Marker>[3];

    float[] lastIndex = new float[] { 0, 0, 0 };

    float _positionMarker = 0;
    int _indexSliderCurrent = 0;
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

        for (int i = 0; i < _markerList.Length; i++)
        {
            _markerList[i] = new List<Marker>();
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    //Ouvre le panel de création d'action quand on clique sur un slider
    public void sendPositionInScene(int index)
    {
        _gameManager.setActiveActionPanel(true);
        _gameManager.setTypeObjectChosen("");
        _gameManager.setActionChosen("");
        _gameManager.setIdPlayerActif(index);
        _gameManager.refreshActionLabel("");
        // Calcul de la position dans la scène
        float value = _sliders[index].value;
        _positionMarker = value * (_xLevelEnd - _xLevelStart) + _xLevelStart;
        _indexSliderCurrent = index;
        //TODO: faire un tableau de liste d'action
        // Ajouter d'autres traitements ?
    }
    //Création de l'action avec les informations données dans le panel d'action
    public void createAction()
    {
        Action action_tmp;
        if (_gameManager.getWaitBefore() > 0)
        {
            action_tmp = new Action(_indexSliderCurrent, "Wait", _gameManager.getTypeObjectChosen(), _gameManager.getMoveBefore(), _gameManager.getWaitBefore(), _positionMarker); // TODO: Généraliser l'action avec l'actionManager
            _gameManager.addActionPlayers(_indexSliderCurrent, action_tmp);
            createMarker(_indexSliderCurrent, action_tmp, _positionMarker);
        }
        if (_gameManager.getMoveBefore())
        {
            action_tmp = new Action(_indexSliderCurrent, "Move", _gameManager.getTypeObjectChosen(), _gameManager.getMoveBefore(), _gameManager.getWaitBefore(), _positionMarker); // TODO: Généraliser l'action avec l'actionManager
            _gameManager.addActionPlayers(_indexSliderCurrent, action_tmp);
            createMarker(_indexSliderCurrent, action_tmp, _positionMarker);
        }
        if (!_gameManager.getTypeObjectChosen().Equals("") && !_gameManager.getActionChosen().Equals(""))
        {
            Debug.Log(_gameManager.getTypeObjectChosen());
            action_tmp = new Action(_indexSliderCurrent, _gameManager.getActionChosen(), _gameManager.getTypeObjectChosen(), _gameManager.getMoveBefore(), _gameManager.getWaitBefore(), _positionMarker); // TODO: Généraliser l'action avec l'actionManager
            _gameManager.addActionPlayers(_indexSliderCurrent, action_tmp);

            // Création du marker
            createMarker(_indexSliderCurrent, action_tmp, _positionMarker);
        }
        _gameManager.setTypeObjectChosen("");
        _gameManager.setActionChosen("");
        _gameManager.refreshActionLabel("");
        
    }
    //Création du bouton en récupérant des boutons disponibles. Cela permet d'éviter des "instantiate"
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
        Button but = getButtonAvailable();
        but.transform.localScale = new Vector3(_scaleFactor, _scaleFactor, _scaleFactor);
        but.transform.SetParent(_sliderParent);
        // Placement du button
        RectTransform rect_transform = but.GetComponent<RectTransform>();
        rect_transform.pivot = new Vector2(0.5f, 0);
        rect_transform.anchoredPosition3D = new Vector3(0, 0, 0);
        rect_transform.anchoredPosition = new Vector2(position * (1 / _scaleFactor), -(index * _offsetYSliders));
        //TODO: petit problème de décalage dans les button trouver pourquoi mm si ça doit être lié au slider
        _marker_Tmp = new Marker(index, action, but, position);
        _markerList[index].Add(_marker_Tmp);

        /*OnClickMarker functionCLick = but.gameObject.GetComponent<OnClickMarker>();
        functionCLick.setSliderManager(this);
        functionCLick.setMarker(_marker_Tmp);*/
        // Mise à jour du prochain ID
        lastIndex[index]++;
    }
    public void destroyMarker(Button marker)
    {
        int firstIndex = -1;
        int lastIndex = -1;
        List<Marker> markerList_tmp;
        Marker marker_tmp;
        Button objectMarker_tmp;
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
                    removeButton(marker);
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
        for (int i = 0; i < _markerList.Length; i++)
        {

            for (int j = 0; j < _markerList[i].Count; j++)
            {
                removeButton(_markerList[i][j].getMarker());
            }
               
        }
        _markerList = new List<Marker>[3];
        for (int i = 0; i < _markerList.Length; i++)
        {
            _markerList[i] = new List<Marker>();
        }
    }

    public void destroyActionMarkerById(int id)
    {
        List<Action> actionList_tmp = _markerActive.getActionList();
        _gameManager.removeAction(_markerActive.getIdPlayer(), actionList_tmp[id]);
        actionList_tmp.RemoveAt(id);
        if (actionList_tmp.Count == 0)
        {
            _markerList[_markerActive.getIdPlayer()].Remove(_markerActive);
            setActiveMarkerPanel(false);
            removeButton(_markerObjectCurrent);
            return;
        }
            
        refreshDisplayListAction(_markerActive);
    }
    public Marker getMarkerWithObject(Button marker)
    {
        List<Marker> markerList_tmp;
        Marker marker_tmp;
        Button objectMarker_tmp;
        for (int i = 0; i < 3; i++)
        {
            markerList_tmp = _markerList[i];
            for (int j = 0; j < markerList_tmp.Count; j++)
            {
                marker_tmp = markerList_tmp[j];
                
                objectMarker_tmp = marker_tmp.getMarker();
                if (objectMarker_tmp.Equals(marker))
                {
                    return marker_tmp;
                }
            }
        }
        return null;
    }
 
    public void setActiveMarkerPanel(bool value)
    {
        _panelMarker.SetActive(value);
    }
    public void setActiveMarkerAction(int id, bool value)
    {
        _actionPanel[id].SetActive(value);
    }
    public void setActiveMarkerActionAll(bool value)
    {
        for (int i = 0; i < _actionPanel.Length; i++ )
            _actionPanel[i].SetActive(value);
    }
    public void setTextMarkerLabelAction(int id, string text)
    {
        _actionLabel[id].text = text;
    }

    public void setMarkerActiveCurrent(Marker marker)
    {
        _markerActive = marker;
    }
    public void setMarkerObject(Button marker)
    {
        _markerObjectCurrent = marker;
    }
    public Marker getMarkerActiveCurrent()
    {
        return _markerActive;
    }
    public Button getMarkerObject()
    {
        return _markerObjectCurrent;
    }
    public Button getButtonAvailable()
    {
        Button but = _ButtonMarkerAvailable[0];
        _ButtonMarkerActive.Add(but);
        _ButtonMarkerAvailable.RemoveAt(0);
        but.gameObject.SetActive(true);
        return but;
    }
    public void removeButton(Button but)
    {
        _ButtonMarkerAvailable.Add(but);
        _ButtonMarkerActive.Remove(but);
        but.gameObject.SetActive(false);

    }
    //Affichage du contenu d'un marqueur ( sa liste d'actions ) permet de voir quelles sont nos actions et de supprimer celles qu'on veut
    public void OnClickMarker(Button markerObject)
    {
        if (markerObject == null)
            return;
        
        Marker marker = getMarkerWithObject(markerObject);
        setMarkerActiveCurrent(marker);
        setMarkerObject(markerObject);
        List<Action> actionList = marker.getActionList();
        setActiveMarkerPanel(true);
        setActiveMarkerActionAll(false);
        for (int i = 0; i < actionList.Count; i++)
        {
             setActiveMarkerAction(i, true);
             if (actionList[i].get_typeAction().Equals("Wait"))
             {
                 setTextMarkerLabelAction(i, actionList[i].getIdAction() + " " + actionList[i].get_typeAction() + ": " + actionList[i].get_secondToWait()+ "s");
             }
             else if (actionList[i].get_typeAction().Equals("Move"))
             {
                 setTextMarkerLabelAction(i, actionList[i].getIdAction() + " " + actionList[i].get_typeAction());
             }
             else
             {
                 setTextMarkerLabelAction(i, actionList[i].getIdAction() + " " + actionList[i].get_typeAction() + "-> " + actionList[i].get_typeTarget());
             }
             
           
        }
    }
    public void refreshDisplayListAction(Marker marker)
    {
        List<Action> actionList = marker.getActionList();
        setActiveMarkerPanel(true);
        setActiveMarkerActionAll(false);
        for (int i = 0; i < actionList.Count; i++)
        {
            setActiveMarkerAction(i, true);
            if (actionList[i].get_typeAction().Equals("Wait"))
            {
                setTextMarkerLabelAction(i, actionList[i].getIdAction() + " " + actionList[i].get_typeAction() + ": " + actionList[i].get_secondToWait() + "s");
            }
            else if (actionList[i].get_typeAction().Equals("Move"))
            {
                setTextMarkerLabelAction(i, actionList[i].getIdAction() + " " + actionList[i].get_typeAction());
            }
            else
            {
                setTextMarkerLabelAction(i, actionList[i].getIdAction() + " " + actionList[i].get_typeAction() + "-> " + actionList[i].get_typeTarget());
            }

        }
    }
}
