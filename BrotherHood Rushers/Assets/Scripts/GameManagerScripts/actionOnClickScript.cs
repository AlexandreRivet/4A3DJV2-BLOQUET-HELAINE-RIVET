using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class actionOnClickScript : MonoBehaviour {

    public CharacterManager _characterManager;
    public GameManagerScript _gameManager;
    public SlidersManagerScript _sliderManager;
    //ici l'objet timeline
    
    public int _idTarget;
    //public Vector3 _rangeMax;
    public List<float> _otherValues;
    public GameObject[] _otherGameObjects;
    public int[] _sceneIdGameObjects;
    public string _actionName;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    // Ajout d'une action quand on clique sur un bouton
    public void onClick()
    {
        if (_gameManager.getPlayerActif() == null)
            return;

        _gameManager.addActionPlayers(_gameManager.getIdPlayerActif(), new Action(_gameManager.getIdPlayerActif(), _otherValues, _idTarget, _sceneIdGameObjects, _actionName));
        _sliderManager.createMarker(_gameManager.getIdPlayerActif(), _characterManager.getObjectLevelById(_idTarget).transform.position.x);
       
    }
}
