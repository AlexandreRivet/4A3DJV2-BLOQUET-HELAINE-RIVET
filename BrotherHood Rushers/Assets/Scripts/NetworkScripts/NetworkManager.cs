using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class NetworkManager : MonoBehaviour {

    public static NetworkManager Instance { get; private set; }
    [SerializeField]
    private int _maxConnection = 3;
    private bool _isInit = false;
    private List<Player> playersList = new List<Player>();
    private bool _isaReconnexionPlayer = false;
    void Awake()
    {
        Instance = this;
    }
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	public void InitNetwork()
    {
        if (!Network.isServer)
            return;

        for (int i = 0; i < Network.connections.Length; i++)
        {
            string keyPlayer = createKeyPlayer();
            playersList.Add(new Player(Network.connections[i], keyPlayer, - 1));
            networkView.RPC("GiveKeyPlayer", Network.connections[i], "MyKeyGame", keyPlayer);
        }
        _isInit = true;
    }
	// Update is called once per frame
	void Update () {
	
	}
    private void OnPlayerConnected(NetworkPlayer player)
    {
        if (!_isInit)
            return;
        //Debug.Log("New player");
       // Debug.Log("Connexion actuelle : "+ Network.connections.Length);
        if (Network.connections.Length > _maxConnection - 1)
        {
            //Debug.Log("Trop de connection");
            Network.CloseConnection(player, true);
        }
        else
        {
            //Debug.Log("Nombre de joueur dans PlayerList: " + playersList.Count);
            networkView.RPC("GiveMeYourKeyPlayer", player);
            /*for (int i = 0; i < playersList.Count; i++)
            {

                Debug.Log("Joueur numéro: "+ i + " IP:" + playersList[i].getIP() + "  IP du mec qui se co: " + player.ipAddress);
                Debug.Log("Joueur numéro: " + i + " ExtIP:" + playersList[i].getExtIP() + "  ExtIP du mec qui se co: " + player.externalIP);
                Debug.Log("GUID PlayerList : " + playersList[i].getGuid() + " GUID player" + player.guid);
                Debug.Log("Port PlayerList : " + playersList[i].getPort() + " Port player" + player.port);
                Debug.Log("ExtPort PlayerList : " + playersList[i].getExtPort() + " ExtPort player" + player.externalPort);
                if (playersList[i].getNetworkPlayer().ipAddress == player.ipAddress && playersList[i].getNetworkPlayer().externalIP == player.externalIP)
               {
                  
                   networkView.RPC("ConnectionToGame", player, Application.loadedLevel);
                   return;
               }
            }
            Network.CloseConnection(player, true);*/
        }
    }
    public void checkReconnexionPlayer(string key, NetworkPlayer player)
    {
        for (int i = 0; i < playersList.Count; i++)
        {
            /*Debug.Log("Joueur numéro: " + i + " Key:" + playersList[i].getKey() + "  IP du mec qui se co: " + key);
            Debug.Log("Joueur numéro: " + i + " IP:" + playersList[i].getIP() + "  IP du mec qui se co: " + player.ipAddress);
            Debug.Log("Joueur numéro: " + i + " ExtIP:" + playersList[i].getExtIP() + "  ExtIP du mec qui se co: " + player.externalIP);
            Debug.Log("GUID PlayerList : " + playersList[i].getGuid() + " GUID player" + player.guid);
            Debug.Log("Port PlayerList : " + playersList[i].getPort() + " Port player" + player.port);
            Debug.Log("ExtPort PlayerList : " + playersList[i].getExtPort() + " ExtPort player" + player.externalPort);*/
            if (playersList[i].getKey().Equals(key))
            {
                networkView.RPC("ConnectionToGame", player, Application.loadedLevel, true);
                return;
            }
        }
        Network.CloseConnection(player, true);
    }
    public void setIsReconnexionPlayer(bool value)
    {
        _isaReconnexionPlayer = value;
    }
    public bool getIsReconnexionPlayer()
    {
        return _isaReconnexionPlayer;
    }
    //Fonction de debug appelée quand un nouveau joueur se deconnecte
    private void OnPlayerDisconnected(NetworkPlayer player)
    {
        if (!_isInit)
            return;
    }
    public List<Player> getListPlayer()
    {
        return playersList;
    }
    public void setIdCharacter(int idPlayer, int idCharact)
    {
        playersList[idPlayer].setIdCHaracter(idCharact);
    }
    public string createKeyPlayer()
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new System.Random();
        var result = new string(
            Enumerable.Repeat(chars, 8)
                      .Select(s => s[random.Next(s.Length)])
                      .ToArray());
        return result;
    }

    [RPC]
    private void ConnectionToGame(int level, bool reconnect)
    {
        _isaReconnexionPlayer = reconnect;
        Application.LoadLevel(level);
    }
    [RPC]
    public void TakeMyCharacter(string key, NetworkPlayer player)
    {
        if(Network.isServer)
        {
            for (int i = 0; i < playersList.Count; i++)
            {
                if (playersList[i].getKey().Equals(key))
                {
                    //Debug.Log(playersList[i].getId());
                    networkView.RPC("GiveMyCharacter", player, playersList[i].getId());
                }
            }
        }
    }
    [RPC]
    public void GiveMyCharacter(int idCHaracter)
    {
        Debug.Log("GiveMyCharacter" + idCHaracter + " ");
        GameManagerScript.Instance.setIdMyCharacter(idCHaracter);
        if (idCHaracter != -1)
            GameManagerScript.Instance.setActivePanelSelectCharac(false);
    }
    [RPC]
    public void GiveKeyPlayer(string idplayer, string key)
    {
        PlayerPrefs.SetString(idplayer, key);
        PlayerPrefs.Save();
    }
    [RPC]
    public void GiveMeYourKeyPlayer()
    {
        networkView.RPC("ThisIsMyKeyPlayer",RPCMode.Server, PlayerPrefs.GetString("MyKeyGame"), Network.player);
    }
    [RPC]
    public void ThisIsMyKeyPlayer(string key,NetworkPlayer player)
    {
        checkReconnexionPlayer(key, player);
    }
}
