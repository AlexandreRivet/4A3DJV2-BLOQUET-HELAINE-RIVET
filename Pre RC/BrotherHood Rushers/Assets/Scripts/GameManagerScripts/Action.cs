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
    private int _idAction;
    private int _idCharacter;
    private string _typeAction;
    private string _typeTarget;
    private bool _moveBefore;
    private float _secondToWait;
    private float _xPositionTarget;
	//Type de l'action effectuer
    private int _actionState;

	//initialize
    public Action(int idCharacter, string typeAction, string typeTarget, bool moveBefore, float secondToWait, float xPositionTarget)
    {
        _idCharacter = idCharacter;
        _typeAction = typeAction;
        _typeTarget = typeTarget;
        _moveBefore = moveBefore;
        _secondToWait = secondToWait;
        _xPositionTarget = xPositionTarget;
        _actionState = 0;
	}
  
	//getter & setter
    public int getIdAction()
    {
        return _idAction;
    }
    public int getIdCharacter()
    {
        return _idCharacter;
    }
    public string get_typeAction()
    {
        return _typeAction;
    }
    public string get_typeTarget()
    {
        return _typeTarget;
    }
    public bool get_moveBefore()
    {
        return _moveBefore;
    }
    public float get_secondToWait()
    {
        return _secondToWait;
    }
    public float get_xPositionTarget()
    {
        return _xPositionTarget;
    }
    public int get_actionState()
    {
        return _actionState;
    }

    public void set_IdAction(int value)
    {
        _idAction = value;
    }
    public void set_IdCharacter(int value)
    {
        _idCharacter = value;
    }
    public void set_typeAction(string value)
    {
        _typeAction = value;
    }
    public void set_typeTarget(string value)
    {
        _typeTarget = value;
    }
    public void set_moveBefore(bool value)
    {
        _moveBefore = value;
    }
    public void set_secondToWait(float value)
    {
        _secondToWait = value;
    }
    public void set_xPositionTarget(float value)
    {
        _xPositionTarget = value;
    }
    public void set_actionState(int state)
    {
        this._actionState = state;
    }
    
}
