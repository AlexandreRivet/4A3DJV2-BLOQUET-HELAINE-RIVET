using UnityEngine;
using System.Collections;

public class Player {

    string _key;
    string _ipAddress = "";
    string _externalIpAddress = "";
    int _port = 0;
    int _externalPort = 0;
    string _guid = "";
    int _idCharacterChoose = -1;


    public Player(NetworkPlayer player, string key, int idCharacter)
    {
        _key = key;
        _ipAddress = player.ipAddress;
        _externalIpAddress = player.externalIP;
        _port = player.port;
        _externalPort = player.externalPort;
        _guid = player.guid;
        _idCharacterChoose = idCharacter;
    }
    public void setIdCHaracter(int id)
    {
        _idCharacterChoose = id;
    }
    public string getKey()
    {
        return _key;
    }
    public string getIP()
    {
        return _ipAddress;
    }
    public string getExtIP()
    {
        return _externalIpAddress;
    }
    public int getPort()
    {
        return _port;
    }
    public int getExtPort()
    {
        return _externalPort;
    }
    public string getGuid()
    {
        return _guid;
    }
    public int getId()
    {
        return _idCharacterChoose;
    }
    
}
