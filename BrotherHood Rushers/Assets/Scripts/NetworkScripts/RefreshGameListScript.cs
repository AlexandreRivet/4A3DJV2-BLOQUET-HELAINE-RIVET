using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RefreshGameListScript : MonoBehaviour {

    public GameObject _buttonToConnect;
    public RectTransform _rectTranform;
    public float _button_height = 30;

    private GameObject[] _buttonListToConnect;
    private HostData[] _hostData = new HostData[0];
    private bool _refresh = false;
    private int _indexGameToConnect = 0;

	// Use this for initialization
	void Start () {
	  
	}
	
	// Update is called once per frame
	void Update () 
    {
        //S'il existe des parties en cours et si le joueur a demandé à refresh la liste des parties.
        if (MasterServer.PollHostList().Length > 0 && _refresh)
        {
            _hostData = MasterServer.PollHostList();
            addButtonGameList();
            _refresh = false;
        }
	}

    // Resfresh la liste des parties en cours
    public void refreshHostList()
    {
        MasterServer.RequestHostList("BHR");
        _refresh = true;
    }

    //Ajoute, positionne les boutons de la liste des parties en cours. Permettant ainsi de se connecter à une partie
    public void addButtonGameList()
    {
        if (_buttonToConnect == null || _rectTranform == null)
            return;

        RectTransform rectTransform_current;
        Button button_current;

        _buttonListToConnect = new GameObject[_hostData.Length];

        if (_rectTranform.rect.height < _hostData.Length * (_button_height + 5))
            _rectTranform.sizeDelta = new Vector2(_rectTranform.rect.width, (_hostData.Length * (_button_height + 5)));

        _rectTranform.anchoredPosition = new Vector2(0, -_rectTranform.rect.height / 2);

        for (int i = 0; i < _hostData.Length; i++)
		{
            if (!(_hostData[i].gameType.Equals("BHR")))
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
            button_current.onClick.AddListener(() => { connectAtGame(_indexGameToConnect); });
           
		}
    }

    //Connexion à une partie
    public void connectAtGame(int i)
    {
        Network.Connect(_hostData[i]);
    }
}
