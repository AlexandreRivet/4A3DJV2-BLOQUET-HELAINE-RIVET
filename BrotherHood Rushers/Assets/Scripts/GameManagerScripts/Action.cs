using UnityEngine;
using System.Collections;

//Author: Bloquet Pierre
//Date :28/11/2014
//Last modification : 03/12/2014
//Descrition:
//Note:

public class Action {
	//Position de l'action
	private GameObject _character;
	//Rayon dans lequel doit ce trouver le joueur pour effectuer l'action
	private Vector3 _length;
	//GameObject sur lequel l'action est effectuer
    private GameObject _target;
	//Type de l'action effectuer
	private string _typeAction;

	//initialize
    public Action(GameObject character, Vector3 length, GameObject target, string typeAction)
    {
        this._character = character;
		this._length = length;
        this._target = target;
		this._typeAction = typeAction;
	}

	//getter & setter
	public float get_position(){
        return this._character.transform.position.x;
	}
    public Vector3 get_length()
    {
		return this._length;
	}
	public float get_positionObject(){
        return this._target.transform.position.x;
	}
	public string get_typeAction(){
		return this._typeAction;
	}
	public void set_character(GameObject character){
        this._character = character;
	}
    public void set_length(Vector3 lenght)
    {
		this._length = lenght;
	}
	public void set_Object(GameObject target){
        this._target = target;
	}
	public void set_typeAction(string typeAction){
		this._typeAction = typeAction;
	}
}
