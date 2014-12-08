using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Author: Bloquet Pierre
//Date :02/11/2014
//Last modification : 03/12/2014
//Descrition:
//Note:

public class PileActions {

	private List<Action> _pileActionPlayer;
	private int _markerPile;

	public PileActions(){
		this._pileActionPlayer = new List<Action>();
		this._markerPile = 0;
	}

	//add
	public void addActionPlayer(Action action){
		this._pileActionPlayer.Add(action);
	}

	//supr
	public void removeActionPlayerAt(int index){
		this._pileActionPlayer.RemoveAt(index);
	}

	//suprall
	public void removeAllActionPlayer(){
		for(int i = this._pileActionPlayer.Count()-1; i>0; i--){
			removeActionPlayerAt(i);
		}
	}

	//addwhere
	public void addActionPlayerAt(Action action, int index){
		/*if(index <= this._pileActionPlayer.Count()){
			List<Action> temp = new List<Action>();
			for(int i=index; i<this._pileActionPlayer.Count()-1; i++){
				temp.Add(this._pileActionPlayer[i]);
			}
			this._pileActionPlayer.RemoveRange(index, (this._pileActionPlayer.Count()-1-index));
			this._pileActionPlayer.Add(action);
			this._pileActionPlayer.AddRange(temp);*/
        this._pileActionPlayer.Insert(index, action);
	}

	//smartAdd
	/*public void smartAddActionPlayer(Action action){
		if(!this._pileActionPlayer[this._pileActionPlayer.Count()-1].compareActionByPosition(action)){
			addActionPlayer(action);
		}else{
			for(int i = 0; i < this._pileActionPlayer.Count()-1; i++){
				if(this._pileActionPlayer[i].compareActionByPosition(action)){
					addActionPlayerAt(action, i);
				}
			}
		}
	}*/

	//tri a bulle simple
	/*public void sortPileByPosition(){
		int n = this._pileActionPlayer.Count();
		int i = 1, j;
		while (i < n)
		{
			for (j = n; j > i; j--)
			{
				if (!this._pileActionPlayer[j-1].compareActionByPosition(this._pileActionPlayer[j-2]))
				{
					Action tmp = this._pileActionPlayer[j-1];
					this._pileActionPlayer[j-1] = this._pileActionPlayer[j-2];
					this._pileActionPlayer[j-2] = tmp;
				}
			}
			i++;
		}
		//this._pileActionPlayer.OrderBy(x => x.get_position()).ToList();
	}*/

	/*public void sortPileByPositionAndLength(){
		this._pileActionPlayer = this._pileActionPlayer.OrderBy(x => x.get_position()-x.get_length()).ToList();
	}

	public Action readPileActionPlayer(float positionPlayer){//Brouillon, faut qu'on en discute.
		if(positionPlayer == this._pileActionPlayer[this._markerPile].get_position()){
			this._markerPile++;
			return this._pileActionPlayer[this._markerPile];
		}
		return null;
	}

	public void reset(){
		this._markerPile = 0;
	}*/

	/*public void swapAction(int i, int j){
		Action temp = this._pileActionPlayer[i];
		this._pileActionPlayer[i] = this._pileActionPlayer[j];
		this._pileActionPlayer[j] = temp;
	}

	public void displayPileActionPlayer(){
		Debug.Log("display all");
		foreach(Action act in this._pileActionPlayer){
			Debug.Log(act.get_position());
		}
	}*/

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
}
