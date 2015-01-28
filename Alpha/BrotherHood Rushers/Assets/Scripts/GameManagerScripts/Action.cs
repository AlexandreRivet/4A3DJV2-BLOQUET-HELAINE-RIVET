using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//Author: Bloquet Pierre
//Date :28/11/2014
//Last modification : 03/12/2014
//Descrition:
//Note:
[System.Serializable]
public class Action {
	//Position de l'action
    private int _idCharacter;
	//Rayon dans lequel doit ce trouver le joueur pour effectuer l'action
	//private Vector3 _rangeActionMax;

    private List<float> _otherInformations;//Description needed
	//GameObject sur lequel l'action est effectuer
    private int _idTarget;

    private int[] _sceneIdGameObjects;
	//Type de l'action effectuer
	private string _typeAction;

    private int _actionState;

	//initialize
    public Action(int idCharacter, List<float> otherValues, int idTarget, int[] sceneIdGameObjects, string typeAction)
    {
        this._idCharacter = idCharacter;
        this._otherInformations = otherValues;
        this._idTarget = idTarget;
        this._sceneIdGameObjects = sceneIdGameObjects;
		this._typeAction = typeAction;
        this._actionState = 0;
	}

	public bool comparePositionAction(float position,float errorMarge){//Compare la position d'une action 
		if(this._otherInformations.Count != 0){
			if(this._otherInformations[0] >= position- errorMarge &&
			   this._otherInformations[0] <= position+ errorMarge){
				return true;
			}
		}
		return false;
	}
  
	//getter & setter
   
    public int getIdCharacter()
    {
        return this._idCharacter;
    }
    public int getIdTarget()
    {
        return this._idTarget;
    }
    public float get_informationById(int i)
    {
        return this._otherInformations[i];
	}
   /* public Vector3 getRangeMax()
    {
        return this._rangeActionMax;
    }*/
	public string get_typeAction(){
		return this._typeAction;
	}
    public int get_actionState()
    {
        return this._actionState;
    }
    public void set_information(float value)
    {
        this._otherInformations.Add(value);
	}
    /*public void setRangeMax(Vector3 range)
    {
        this._rangeActionMax = range;
    }*/
	public void set_typeAction(string typeAction){
		this._typeAction = typeAction;
	}
    public void set_actionState(int state)
    {
        this._actionState = state;
    }
    public int get_sceneIdObject(int id)
    {
        return this._sceneIdGameObjects[id]; 
    }
    public int[] get_allSceneIdObject()
    {
        return this._sceneIdGameObjects;
    }
}
