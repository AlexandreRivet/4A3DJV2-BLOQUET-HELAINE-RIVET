using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class actionWaitOnClickSript : MonoBehaviour {
    [SerializeField]
    private GameManagerScript _gameManager;
    [SerializeField]
    private SlidersManagerScript _sliderManager;
    //ici l'objet timeline
    [SerializeField]
    private int _idTarget;
    //public Vector3 _rangeMax;
    [SerializeField]
    private Text _otherValues;
    
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

        Action action_tmp = new Action(_gameManager.getIdPlayerActif(), new List<float>() { float.Parse(_otherValues.text) }, 0, null, "Wait");
        _gameManager.addActionPlayers(_gameManager.getIdPlayerActif(), action_tmp);
        _sliderManager.createMarker(_gameManager.getIdPlayerActif(), action_tmp, _gameManager.getPlayerActif().transform.position.x);
    }
}
