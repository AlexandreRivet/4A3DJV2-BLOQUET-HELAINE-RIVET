using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class actionOnClickScript : MonoBehaviour {

    public GameManagerScript _gameManager;
    public SlidersManagerScript _sliderManager;
    //ici l'objet timeline
    
    public GameObject _target;
    public Vector3 _rangeMax;
    public List<float> _otherValues;
    public GameObject[] _otherGameObjects;
    public string _actionName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void onClick()
    {
        if (_gameManager.getPlayerActif() == null)
            return;
        switch (_gameManager.getIdPlayerActif())
        {
            case 0:
                _gameManager.addActionPlayer1(new Action(_gameManager.getPlayerActif(), _rangeMax,_otherValues, _target,_otherGameObjects, _actionName));
                _sliderManager.createMarker(0, _target.transform.position.x);
                break;
            case 1:
                _gameManager.addActionPlayer2(new Action(_gameManager.getPlayerActif(), _rangeMax, _otherValues, _target,_otherGameObjects, _actionName));
                _sliderManager.createMarker(1, _target.transform.position.x);
                break;
            case 2:
                _gameManager.addActionPlayer3(new Action(_gameManager.getPlayerActif(), _rangeMax, _otherValues, _target,_otherGameObjects, _actionName));
                _sliderManager.createMarker(2, _target.transform.position.x);
                break;
        }
       
    }
}
