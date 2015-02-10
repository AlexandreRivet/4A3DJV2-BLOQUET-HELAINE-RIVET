using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RefreshGameListScript : MonoBehaviour {

    [SerializeField]
    private GameObject _buttonToConnect;
    [SerializeField]
    private RectTransform _rectTranform;
    /*[SerializeField]
    private float _button_height = 30;*/
    [SerializeField]
    private string _gameLevel = "";

    private GameObject[] _buttonListToConnect;
    private HostData[] _hostData = new HostData[0];
    private int _indexGameToConnect = 0;

	// Use this for initialization
	void Start () {
	  
	}
	
	// Update is called once per frame
	void Update () 
    {
       
	}

    // Resfresh la liste des parties en cours
    public void refreshHostList()
    {
        MasterServer.RequestHostList(_gameLevel);
    }

    //Ajoute, positionne les boutons de la liste des parties en cours. Permettant ainsi de se connecter à une partie
    public void addButtonGameList()
    {
        if (_buttonToConnect == null || _rectTranform == null)
            return;

        RectTransform rectTransform_current;
        Button button_current;

        _buttonListToConnect = new GameObject[_hostData.Length];

        /*if (_rectTranform.rect.height < _hostData.Length * (_button_height + 5))
            _rectTranform.sizeDelta = new Vector2(_rectTranform.rect.width, (_hostData.Length * (_button_height + 5)));*/

        //_rectTranform.anchoredPosition = new Vector2(0, -_rectTranform.rect.height / 2);
        for (int i = 0; i < _hostData.Length; i++)
		{
            if (!(_hostData[i].gameType.Equals(_gameLevel)))
                return;

            //obligé de sauvegarder le i car il est perdu lors de l'ajout du listener au bouton. //TODO: Voir pourquoi
            _indexGameToConnect = i;

            _buttonListToConnect[i] = Instantiate(_buttonToConnect) as GameObject;
            _buttonListToConnect[i].GetComponentInChildren<Text>().text = _hostData[i].gameName;
            _buttonListToConnect[i].name = i.ToString();

            rectTransform_current = _buttonListToConnect[i].GetComponent<RectTransform>();
            rectTransform_current.SetParent(transform);
            rectTransform_current.anchoredPosition = new Vector2(0, -i * (rectTransform_current.sizeDelta.y + 10) - rectTransform_current.sizeDelta.y/2);

            button_current = _buttonListToConnect[i].GetComponent<Button>();
            button_current.onClick.AddListener(() => { this.connectAtGame(_indexGameToConnect); });
           
		}
    }

    //Connexion à une partie
    public void connectAtGame(int i)
    {
        //Debug.Log(_hostData[i].gameType + " " + _gameLevel + "  " + i + " " + _hostData[i].port);
        Network.Connect(_hostData[i]);
    }

    public void OnMasterServerEvent(MasterServerEvent mse)
    {
        //Received a host list from the master server
        if (mse == MasterServerEvent.HostListReceived)
        {
            //Debug.Log("Receive  " + _gameLevel);
            HostData[] hostData_tmp = MasterServer.PollHostList();

            if (hostData_tmp.Length >= 0 && hostData_tmp[0].gameType.Equals(_gameLevel))
            {
                _hostData = hostData_tmp;
                addButtonGameList();
            } 
        }
    }
}
