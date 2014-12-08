using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class actionOnClickScript : MonoBehaviour {

    public GameManagerScript _gameManager;
    //ici l'objet timeline
    
    public GameObject _target;
    public float _rangeMaxAction;
    public string _actionName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnClick()
    {
        switch (_gameManager.getIdPlayerActif())
        {
            case 0:
                _gameManager.addActionPlayer1(new Action(_gameManager.getPlayerActif(), _rangeMaxAction, _target, _actionName));
                break;
            case 1:
                _gameManager.addActionPlayer2(new Action(_gameManager.getPlayerActif(), _rangeMaxAction, _target, _actionName));
                break;
            case 2:
                _gameManager.addActionPlayer3(new Action(_gameManager.getPlayerActif(), _rangeMaxAction, _target, _actionName));
                break;
        }
       
    }
}
