using UnityEngine;
using System.Collections;

public class OnClickObjectMenuScript : MonoBehaviour {

    [SerializeField]
    private GameManagerScript _gameManager;
    [SerializeField]
    private int[] _idPlayerWhoCanClick;
    [SerializeField]
    private GameObject _objectPanelActive;
    [SerializeField]
    private GameObject _objectMenuActive;
    /*public GameObject[] _buttonsArrayWhoCanClickJ1;
    public GameObject[] _buttonsArrayWhoCanClickJ2;
    public GameObject[] _buttonsArrayWhoCanClickJ3; */
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        if (_gameManager.getIsReady())
            return;
        if (_gameManager.getMenuInteractObjectActif())
            return;
        for(int i = 0; i < _idPlayerWhoCanClick.Length; i++)
        {
            if(_gameManager.getIdPlayerActif() == _idPlayerWhoCanClick[i])
            {
               
                _objectPanelActive.SetActive(true);
                _objectMenuActive.SetActive(true);
                _gameManager.setMenuInteractObjectActif(true);
                
                return;
            }
        }
    }
}
