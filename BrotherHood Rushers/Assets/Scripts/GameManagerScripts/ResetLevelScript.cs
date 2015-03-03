using UnityEngine;
using System.Collections;

public class ResetLevelScript : MonoBehaviour {

    [SerializeField]
    private GameManagerScript _gameManager;
    [SerializeField]
    private CharacterManager _characterControler;
    [SerializeField]
    private SlidersManagerScript _sliderManager;
    [SerializeField]
    private GameObject[] _startStateObject;
    [SerializeField]
    private int[] _idObjectToReset;

    private Vector3[] _startNewPositionStateObject;
    private Quaternion[] _startNewRotationStateObject;
	// Use this for initialization
	void Start () {
        _startNewPositionStateObject = new Vector3[_startStateObject.Length];
        _startNewRotationStateObject = new Quaternion[_startStateObject.Length];
        for (int i = 0; i < _startStateObject.Length; i++)
        {
            _startNewPositionStateObject[i] = new Vector3();
            _startNewRotationStateObject[i] = new Quaternion();
        }
        resetNewPosition();    
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
        _gameManager.resetPileActions(true);
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
    //Fait la même chose que resetLevel mais sans reset les actions
    public void resetPositions()
    {
        _gameManager.setFakeSimulation(false);
        _gameManager.resetPileActions(false);
        for (int i = 0; i < _idObjectToReset.Length; i++)
        {
            _characterControler.getObjectLevelById(_idObjectToReset[i]).transform.position = _startNewPositionStateObject[i];
            _characterControler.getObjectLevelById(_idObjectToReset[i]).transform.rotation = _startNewRotationStateObject[i];
            _characterControler.getObjectLevelById(_idObjectToReset[i]).SetActive(_startStateObject[i].activeSelf);
        }
        //_sliderManager.deleteAllMarkers();
    }
    //Fait la même chose que resetLevel mais sans reset les positions
    public void resetAction()
    {
        //On coupe la simulation
        _gameManager.setFakeSimulation(false);
        //On reset les tableaux d'actions
        _gameManager.resetPileActions(true);
        //On supprime les marker sur les sliders
        _sliderManager.deleteAllMarkers();
    }

	public void saveNewPosition(){//Save the new position of the object to continue


        for (int i = 0; i < _startNewPositionStateObject.Length; i++)
		{
			//On reset les positions
            _startNewPositionStateObject[i] = _characterControler.getObjectLevelById(_idObjectToReset[i]).transform.position;
            _startNewRotationStateObject[i] = _characterControler.getObjectLevelById(_idObjectToReset[i]).transform.rotation;
		}
	}

	public void resetNewPosition(){//Save the new position of the object to continue
        for (int i = 0; i < _startNewPositionStateObject.Length; i++)
        {
            //On reset les positions
            _startNewPositionStateObject[i] = _startStateObject[i].transform.position;
            _startNewRotationStateObject[i] = _startStateObject[i].transform.rotation;
        }
	}
}
