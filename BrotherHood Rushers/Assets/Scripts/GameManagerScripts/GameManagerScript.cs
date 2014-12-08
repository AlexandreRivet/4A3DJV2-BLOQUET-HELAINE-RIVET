using UnityEngine;
using System.Collections;


//Author: Bloquet Pierre
//Date :28/11/2014
//Last modification : 03/12/2014
//Descrition:
//Note: c'est juste du test, donc rien d'important

public class GameManagerScript : MonoBehaviour {



    private int _idPlayerActif;
    private GameObject _gameObjectPlayerActif;
    private PileActions _pileActionPlayer1;
	private PileActions _pileActionPlayer2;
	private PileActions _pileActionPlayer3;

	// Use this for initialization
	void Start () {
		_pileActionPlayer1 = new PileActions();
        _pileActionPlayer2 = new PileActions();
        _pileActionPlayer3 = new PileActions();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setIdPlayerActif(int id)
    {
        _idPlayerActif = id;
    }
    public int getIdPlayerActif()
    {
       return  _idPlayerActif;
    }
    public void setPlayerActif(GameObject player)
    {
        _gameObjectPlayerActif = player;
    }
    public GameObject getPlayerActif()
    {
        return _gameObjectPlayerActif;
    }
    public void addActionPlayer1(Action action)
    {
        _pileActionPlayer1.addActionPlayer(action);
    }
    public void addActionPlayer2(Action action)
    {
        _pileActionPlayer2.addActionPlayer(action);
    }
    public void addActionPlayer3(Action action)
    {
        _pileActionPlayer3.addActionPlayer(action);
    }

}
