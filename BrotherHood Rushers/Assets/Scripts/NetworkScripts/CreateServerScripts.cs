using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateServerScripts : MonoBehaviour {

    public int _maxConnection = 3;
    public string _levelName;
    public GameObject _menuNetwork;
    public GameObject _menuLobby;
    public Text[] _lobbyTextArray;

    private string _privateName = " Game Name Empty";
    private int _port = 21000;
    

	// Use this for initialization
	void Start () {
	
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
        Network.InitializeServer(32, _port, !Network.HavePublicAddress());
        MasterServer.RegisterHost("BHR", _privateName, "Welcome to Brotherhood Runners");
    }

    //Fonction de debug appelée quand le serveur est initialisé
    public void OnServerInitialized()
    {
        Debug.Log("Server initialized");
    }

    //Fonction de debug appelée quand le Master serveur est créé
    public void OnMasterServerEvent(MasterServerEvent mse)
    {
        if (mse == MasterServerEvent.RegistrationSucceeded && !(Application.loadedLevelName.Equals(_levelName)))
	    {
            Debug.Log("Connection Succeful");
            //Application.LoadLevel(_levelName);
            _menuLobby.SetActive(true);
            _menuNetwork.SetActive(false);
	    }
    }

    //Fonction de debug appelée quand un nouveau joueur se connecte
    private void OnPlayerConnected(NetworkPlayer player)
    {
        Debug.Log("New player");
        if (Network.connections.Length > _maxConnection)
        {
            Debug.Log("Trop de connection");
            Network.CloseConnection(player, true);
        }
        else
        {
            networkView.RPC("ConnectPlayerToGame",RPCMode.Others,player);
        }
    }

    [RPC]
    private void ConnectPlayerToGame(NetworkPlayer player)
    {
        if (Network.isClient && Network.player.Equals(player))
        {
            //Application.LoadLevel(_levelName);
            _menuLobby.SetActive(true);
            _menuNetwork.SetActive(false);
        }
    }
}
