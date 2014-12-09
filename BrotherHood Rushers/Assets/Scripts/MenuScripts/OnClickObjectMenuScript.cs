using UnityEngine;
using System.Collections;

public class OnClickObjectMenuScript : MonoBehaviour {

    public GameManagerScript _gameManager;
    public int[] _idPlayerWhoCanClick;
    public GameObject _objectPanelActive;
    public GameObject _objectMenuActive;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        for(int i = 0; i < _idPlayerWhoCanClick.Length; i++)
        {
            if(_gameManager.getIdPlayerActif() == _idPlayerWhoCanClick[i])
            {
                _objectPanelActive.SetActive(true);
                _objectMenuActive.SetActive(true);
                return;
            }
        }
    }
}
