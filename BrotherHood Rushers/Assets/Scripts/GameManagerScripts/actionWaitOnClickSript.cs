using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class actionWaitOnClickSript : MonoBehaviour {

    public GameManagerScript _gameManager;
    public SlidersManagerScript _sliderManager;
    //ici l'objet timeline

    public GameObject _target;
    public Vector3 _rangeMax;
    public Text _otherValues;
    
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClick()
    {
        //TODO:Securité pour le parse
        switch (_gameManager.getIdPlayerActif())
        {
            case 0:
                _gameManager.addActionPlayer1(new Action(_gameManager.getPlayerActif(), _rangeMax, new List<float>(){float.Parse(_otherValues.text) }, _target, "Wait"));
                _sliderManager.createMarker(0, _gameManager.getPlayerActif().transform.position.x);
                break;
            case 1:
               
                _gameManager.addActionPlayer2(new Action(_gameManager.getPlayerActif(), _rangeMax, new List<float>() { float.Parse(_otherValues.text) }, _target, "Wait"));
                _sliderManager.createMarker(1, _gameManager.getPlayerActif().transform.position.x);
                break;
            case 2:
                _gameManager.addActionPlayer3(new Action(_gameManager.getPlayerActif(), _rangeMax, new List<float>() { float.Parse(_otherValues.text) }, _target, "Wait"));
                _sliderManager.createMarker(2, _gameManager.getPlayerActif().transform.position.x);
                break;
        }

    }
}
