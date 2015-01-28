using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class actionWaitOnClickSript : MonoBehaviour {

    public GameManagerScript _gameManager;
    public SlidersManagerScript _sliderManager;
    //ici l'objet timeline

    public int _idTarget;
    //public Vector3 _rangeMax;
    public Text _otherValues;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    //Fonctions qui ajoute une action "Wait" a un personnage
    public void onClick()
    {
        //TODO:Securité pour le parse
        if (_gameManager.getPlayerActif() == null)
            return;

        _gameManager.addActionPlayers(_gameManager.getIdPlayerActif(),new Action(_gameManager.getIdPlayerActif(), new List<float>() { float.Parse(_otherValues.text) }, 0, null, "Wait"));
        _sliderManager.createMarker(_gameManager.getIdPlayerActif(), _gameManager.getPlayerActif().transform.position.x);
    }
}
