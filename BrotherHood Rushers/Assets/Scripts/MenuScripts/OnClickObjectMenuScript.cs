using UnityEngine;
using System.Collections;

public class OnClickObjectMenuScript : MonoBehaviour {

    public GameManagerScript _gameManager;
    public int[] _idPlayerWhoCanClick;
    public GameObject _objectPanelActive;
    public GameObject _objectMenuActive;
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
        if (_gameManager.getMenuInteractObjectActif())
            return;
        for(int i = 0; i < _idPlayerWhoCanClick.Length; i++)
        {
            if(_gameManager.getIdPlayerActif() == _idPlayerWhoCanClick[i])
            {
               
                _objectPanelActive.SetActive(true);
                _objectMenuActive.SetActive(true);
                _gameManager.setMenuInteractObjectActif(true);
                // Si on veut faire en sorte que seulement certain boutons s'affichent selon le personnage
               /* for (int j = 0; j < _buttonsArrayWhoCanClickJ1.Length; j++)
                {
                    _buttonsArrayWhoCanClickJ1[j].SetActive(true); 
                }
                for (int j = 0; j < _buttonsArrayWhoCanClickJ2.Length; j++)
                {
                    _buttonsArrayWhoCanClickJ2[j].SetActive(true);
                }
                for (int j = 0; j < _buttonsArrayWhoCanClickJ3.Length; j++)
                {
                    _buttonsArrayWhoCanClickJ3[j].SetActive(true);
                }*/
                return;
            }
        }
    }
}
