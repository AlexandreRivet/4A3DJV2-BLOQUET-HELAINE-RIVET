using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class actionOnClickScript : MonoBehaviour {
    
    [SerializeField]
    private CharacterManager _characterManager;
    [SerializeField]
    private GameManagerScript _gameManager;
    [SerializeField]
    private SlidersManagerScript _sliderManager;
    //ici l'objet timeline
    [SerializeField]
    private int _idTarget;
    //public Vector3 _rangeMax;
    [SerializeField]
    private List<float> _otherValues;
    [SerializeField]
    private GameObject[] _otherGameObjects;
    [SerializeField]
    private int[] _sceneIdGameObjects;
    [SerializeField]
    private string _actionName;
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

        Action action_tmp = new Action(_gameManager.getIdPlayerActif(), _otherValues, _idTarget, _sceneIdGameObjects, _actionName);
        _gameManager.addActionPlayers(_gameManager.getIdPlayerActif(), action_tmp);
        _sliderManager.createMarker(_gameManager.getIdPlayerActif(), action_tmp, _characterManager.getObjectLevelById(_idTarget).transform.position.x);
       
    }
}
