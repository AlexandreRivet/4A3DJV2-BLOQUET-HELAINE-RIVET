using UnityEngine;
using System.Collections;

public class ResetLevelScript : MonoBehaviour {

    public GameManagerScript _gameManager;
    public CharacterManager _characterControler;
    public SlidersManagerScript _sliderManager;

    public GameObject[] _startStateObject;
    public int[] _idObjectToReset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //Reset toutes les infos du level à leur valeur du début de partie
    public void resetLevel()
    {
        //On coupe la simulation
        _gameManager.setFakeSimulation(false);
        //On reset les tableaux d'actions
        _gameManager.resetPileActions();
        for(int i = 0; i < _idObjectToReset.Length; i++)
        {
            //On reset les positions
            _characterControler.getObjectLevelById(_idObjectToReset[i]).transform.position = _startStateObject[i].transform.position;
            _characterControler.getObjectLevelById(_idObjectToReset[i]).transform.rotation = _startStateObject[i].transform.rotation;
            _characterControler.getObjectLevelById(_idObjectToReset[i]).SetActive(_startStateObject[i].activeSelf);
        }
        //On supprime les marker sur les sliders
        _sliderManager.deleteAllMarkers();
    }
    //Fait la même chose que resetLevel mais sans reset les positions
    public void resetPositions()
    {
        _gameManager.setFakeSimulation(false);
        for (int i = 0; i < _idObjectToReset.Length; i++)
        {
            _characterControler.getObjectLevelById(_idObjectToReset[i]).transform.position = _startStateObject[i].transform.position;
            _characterControler.getObjectLevelById(_idObjectToReset[i]).transform.rotation = _startStateObject[i].transform.rotation;
            _characterControler.getObjectLevelById(_idObjectToReset[i]).SetActive(_startStateObject[i].activeSelf);
        }
        _sliderManager.deleteAllMarkers();
    }
}
