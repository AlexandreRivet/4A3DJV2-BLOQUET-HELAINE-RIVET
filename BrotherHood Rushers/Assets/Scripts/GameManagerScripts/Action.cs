using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//Author: Bloquet Pierre
//Date :28/11/2014
//Last modification : 03/12/2014
//Descrition:
//Note:

public class Action {
	//Position de l'action
	private GameObject _character;
	//Rayon dans lequel doit ce trouver le joueur pour effectuer l'action
	private Vector3 _rangeActionMax;

    private List<float> _otherInformations;
	//GameObject sur lequel l'action est effectuer
    private GameObject _target;

    private GameObject[] _otherGameObjects;
	//Type de l'action effectuer
	private string _typeAction;

    private int _actionState;

	//initialize
    public Action(GameObject character, Vector3 range, List<float> otherValues,GameObject target, GameObject[] otherGameObjects, string typeAction)
    {
        this._character = character;
        this._rangeActionMax = range;
        this._otherInformations = otherValues;
        this._target = target;
        this._otherGameObjects = otherGameObjects;
		this._typeAction = typeAction;
        this._actionState = 0;
	}
  
	//getter & setter
    public GameObject getCharacter()
    {
        return this._character;
    }
    public GameObject getTarget()
    {
        return this._target;
    }
	public float get_position(){
        return this._character.transform.position.x;
	}
    public float get_informationById(int i)
    {
        return this._otherInformations[i];
	}
    public Vector3 getRangeMax()
    {
        return this._rangeActionMax;
    }
    public GameObject[] getOtherGameObject()
    {
        return this._otherGameObjects;
    }
    public GameObject getOtherGameObjectById(int id)
    {
        return this._otherGameObjects[id];
    }
	public float get_positionTarget(){
        return this._target.transform.position.x;
	}
	public string get_typeAction(){
		return this._typeAction;
	}
    public int get_actionState()
    {
        return this._actionState;
    }
	public void set_character(GameObject character){
        this._character = character;
	}
    public void set_information(float value)
    {
        this._otherInformations.Add(value);
	}
    public void setRangeMax(Vector3 range)
    {
        this._rangeActionMax = range;
    }
	public void set_Object(GameObject target){
        this._target = target;
	}
	public void set_typeAction(string typeAction){
		this._typeAction = typeAction;
	}
    public void set_actionState(int state)
    {
        this._actionState = state;
    }

}
