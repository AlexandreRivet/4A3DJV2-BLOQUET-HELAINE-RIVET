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

    public void onClick()
    {
        //TODO:Securité pour le parse
        if (_gameManager.getPlayerActif() == null)
            return;

        _gameManager.addActionPlayers(_gameManager.getIdPlayerActif(),new Action(_gameManager.getIdPlayerActif(), new List<float>() { float.Parse(_otherValues.text) }, 0, null, "Wait"));
        _sliderManager.createMarker(_gameManager.getIdPlayerActif(), _gameManager.getPlayerActif().transform.position.x);

       /* switch (_gameManager.getIdPlayerActif())
        {
            case 0:
                _gameManager.addActionPlayer1(new Action(_gameManager.getIdPlayerActif(), _rangeMax, new List<float>() { float.Parse(_otherValues.text) }, 0, null, "Wait"));
                _sliderManager.createMarker(0, _gameManager.getPlayerActif().transform.position.x);
                break;
            case 1:

                _gameManager.addActionPlayer2(new Action(_gameManager.getIdPlayerActif(), _rangeMax, new List<float>() { float.Parse(_otherValues.text) }, 1, null, "Wait"));
                _sliderManager.createMarker(1, _gameManager.getPlayerActif().transform.position.x);
                break;
            case 2:
                _gameManager.addActionPlayer3(new Action(_gameManager.getIdPlayerActif(), _rangeMax, new List<float>() { float.Parse(_otherValues.text) }, 2, null, "Wait"));
                _sliderManager.createMarker(2, _gameManager.getPlayerActif().transform.position.x);
                break;
        }*/

    }
}
