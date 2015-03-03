using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Author: Bloquet Pierre
//Date :02/11/2014
//Last modification : 03/12/2014
//Descrition:
//Note:
[System.Serializable]
public class PileActions {

	private List<Action> _pileActionPlayer;
	private int _markerPile;

	public PileActions(){
		this._pileActionPlayer = new List<Action>();
		this._markerPile = 0;
	}

	//add
	public void addActionPlayer(Action action){
        action.set_IdAction(_pileActionPlayer.Count());
        this._pileActionPlayer.Add(action);   
	}
    public void addActionPlayerList(List<Action> actions)
    {
        for (int i = 0; i < actions.Count; i++ )
        {
            this._pileActionPlayer.Add(actions[i]);
        }
        refreshIdActions();
    }
	//supr
	public void removeActionPlayerAt(int index){
		this._pileActionPlayer.RemoveAt(index);
        refreshIdActions();
	}
    public void removeActionPlayer(Action action)
    {
        for (int i = 0; i < _pileActionPlayer.Count; i++)
            if (_pileActionPlayer[i].Equals(action))
                removeActionPlayerAt(i);

        refreshIdActions();
    }
	//suprall
	public void removeAllActionPlayer(){
		for(int i = this._pileActionPlayer.Count()-1; i>0; i--){
			removeActionPlayerAt(i);
		}
	}

	//addwhere
	public void addActionPlayerAt(Action action, int index){
        this._pileActionPlayer.Insert(index, action);
        refreshIdActions();
	}
    
	//refesh Id Action
    public void refreshIdActions()
    {
        for(int i = 0; i < _pileActionPlayer.Count(); i++)
        {
            _pileActionPlayer[i].set_IdAction(i);
        }
    }

	//Getter & Setter
	public void set_markerPile(int markerPile){
		this._markerPile = markerPile;
	}
	public int get_markerPile(){
		return this._markerPile;
	}
	public void set_pileActionPlayer(List<Action> pileActionPlayer){
		this._pileActionPlayer = pileActionPlayer;
	}
	public List<Action> get_pileActionPlayer(){
		return this._pileActionPlayer;
	}
    public int getLength()
    {
        return this._pileActionPlayer.Count;
    }
    public Action getAction(int index)
    {
        return this._pileActionPlayer[index];
    }
}
