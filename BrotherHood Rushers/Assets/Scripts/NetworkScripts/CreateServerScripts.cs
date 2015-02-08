using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CreateServerScripts : MonoBehaviour {
    [SerializeField]
    private NetworkManager _networkManager;
    [SerializeField]
    private int _maxConnection = 3;
    //public string _levelName;
    [SerializeField]
    private GameObject _menuNetwork;
    [SerializeField]
    private GameObject _menuLobby;
    [SerializeField]
    private Text[] _lobbyTextArray;
    [SerializeField]
    private GameObject _buttonStartGame;

    private string _privateTypeGame = "Level1";
    private string _privateName = " Game Name Empty";
    private int _port = 21000;
    private bool _initServer = false;
    private List<NetworkPlayer> playersArray = new List<NetworkPlayer>();
    private bool _gameStart = false;
	// Use this for initialization
	void Start () {
        Debug.developerConsoleVisible = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Setter

    public void setPrivateName(InputField name)
    {
        _privateName = name.text; //TODO: voir s'il est possible d'écraser une partie déjà existante, si oui mettre une sécurité
    }
    public void setPort(InputField port)
    {
        if (int.TryParse(port.text, out _port))
            _port = int.Parse(port.text); 
    }
    public void setPrivateTypeGame(string name)
    {
        _privateTypeGame = name; //TODO: voir s'il est possible d'écraser une partie déjà existante, si oui mettre une sécurité
    }
    //Getter

    public string getPrivateName()
    {
        return _privateName;
    }
    public int setPort()
    {
        return _port;
    }


    //Others Functions

    //Démarre le Serveur
    public void StartServer()
    {
        //Network.InitializeSecurity(); //Permet de protéger son jeu des tricheurs :D
        Network.InitializeServer(_maxConnection, _port, !Network.HavePublicAddress());
        MasterServer.RegisterHost(_privateTypeGame, _privateName, "Welcome to Brotherhood Runners");
    }

    //Fonction de debug appelée quand le serveur est initialisé
    public void OnServerInitialized()
    {
        Debug.Log("Server initialized");
    }

    //Fonction de debug appelée quand le Master serveur est créé
    public void OnMasterServerEvent(MasterServerEvent mse)
    {
        if (mse == MasterServerEvent.RegistrationSucceeded && !_initServer && !_gameStart)
	    {
            Debug.Log("Connection Succeful");
            //Application.LoadLevel(_levelName);
            _menuLobby.SetActive(true);
            _menuNetwork.SetActive(false);
            _initServer = true;
	    }
    }

    //Fonction de debug appelée quand un nouveau joueur se connecte
    private void OnPlayerConnected(NetworkPlayer player)
    {
        if (_gameStart)
            return;
        Debug.Log("New player");
       
        if (Network.connections.Length > _maxConnection-1)
        {
            Debug.Log("Trop de connection");
            Network.CloseConnection(player, true);
        }
        else
        {
            playersArray.Add(player);
            networkView.RPC("ConnectPlayerToGameRPC", RPCMode.All, player, playersArray.Count);
            if (playersArray.Count == _maxConnection - 1)
            {
                _buttonStartGame.SetActive(true);
            }
        }
    }

    //Fonction de debug appelée quand un nouveau joueur se deconnecte
    private void OnPlayerDisconnected(NetworkPlayer player)
    {
        if (_gameStart)
            return;
        Debug.Log("Disconnected player");
        Debug.Log("Clean up after player " + player);
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
        playersArray.Remove(player);
        networkView.RPC("DisconnectPlayerToGameRPC", RPCMode.All, playersArray.Count);
        if (playersArray.Count < _maxConnection - 1)
        {
            _buttonStartGame.SetActive(false);
        }
    }
    public void ClickToStart()
    {
        _gameStart = true;
        _networkManager.InitNetwork();
        networkView.RPC("ClickToStartRPC", RPCMode.All, _privateTypeGame);
    }

    [RPC]
    private void ClickToStartRPC(string levelName)
    {
        Application.LoadLevel(levelName);
    }

    [RPC]
    private void ConnectPlayerToGameRPC(NetworkPlayer player, int nbConnection)
    {
        if (Network.isClient && Network.player.Equals(player))
        {
            //Application.LoadLevel(_levelName);
            _menuLobby.SetActive(true);
            _menuNetwork.SetActive(false);
        }
        for(int i = 1; i < _lobbyTextArray.Length ; i++)
        {
            if (i <= nbConnection)
                _lobbyTextArray[i].text = "V";
            else
                _lobbyTextArray[i].text = "X";
        }
    }

    [RPC]
    private void DisconnectPlayerToGameRPC(int nbConnection)
    {
        for (int i = 1; i < _lobbyTextArray.Length; i++)
        {
            if (i <= nbConnection)
                _lobbyTextArray[i].text = "V";
            else
                _lobbyTextArray[i].text = "X";
        }
    }
}
