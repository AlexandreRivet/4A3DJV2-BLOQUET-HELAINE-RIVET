using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class Marker {
    int _idPlayer;
    List<Action> _actionList = new List<Action>();
    Button _marker;
    float _positionMarker;
    //Classe de création de marker, qui relie un bouton à une liste d'actions
    public Marker(int id, Action action, Button marker, float positionMarker)
    {
        _idPlayer = id;
        _actionList.Add(action);
        _marker = marker;
        _positionMarker = positionMarker;
    }
	public void addAction(Action action)
    {
        _actionList.Add(action);
    }
    public void setMarker(Button marker)
    {
        _marker = marker;
    }
    public void setPosition(float position)
    {
        _positionMarker = position;
    }
    public int getIdPlayer()
    {
        return _idPlayer;
    }
    public Button getMarker()
    {
        return _marker;
    }
    public List<Action> getActionList()
    {
        return _actionList;
    }
    public Action getActionById(int id)
    {
        return _actionList[id];
    }
    public float getPosition()
    {
        return _positionMarker;
    }
}
