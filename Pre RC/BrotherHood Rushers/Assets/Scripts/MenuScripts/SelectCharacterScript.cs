using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
//Script qui gère la sélection des personnages e ndébut de partie
public class SelectCharacterScript : MonoBehaviour {
    [SerializeField]
    private GameObject _characterUsed;
    [SerializeField]
    private CharacterManager _characterManager;
    [SerializeField]
    private GameManagerScript _gameManager;
    [SerializeField]
    private Color _ColorMyButtonLock;
    [SerializeField]
    private Color _ColorOthersButtonLock;

    [SerializeField]
    private GameObject _panelSelectCharacter;
    [SerializeField]
    private Button[] _buttonsArray;
    [SerializeField]
    private GameObject _buttonStartGame;

    private int _nbCharacterLocked = 0; 

    public void selectMyButton(int index)
    {
        setColorButton(index, 0);
        networkView.RPC("setColorButton", RPCMode.Others, index, 1);

        setButtonUninteractable(new int[] { 0, 1, 2 });
        networkView.RPC("setButtonUninteractable", RPCMode.Others, new int[] { index });

        GameObject currentCharacter = _characterManager.getCharactersByIndex(index);
        currentCharacter.renderer.material.color = _ColorMyButtonLock;

        GameObject currentCharacterPosition = _characterManager.getCharactersPositionByIndex(index);
        _gameManager.setIdMyCharacter(index);
        _gameManager.setIdPlayerActif(index);
        _gameManager.setPlayerActif(currentCharacterPosition);
        if(Network.isClient)
            networkView.RPC("sendMyIdCharact", RPCMode.Server, index, PlayerPrefs.GetString("MyKeyGame"));

    }
    
    [RPC]
    public void sendReadyToPlay()
    {
        if(Network.isServer)
            networkView.RPC("sendReadyToPlay", RPCMode.Others);
        _panelSelectCharacter.SetActive(false);
    }
    
    [RPC]
    public void setColorButton(int index, int player)
    {
        if (Network.isServer)
        {
            _nbCharacterLocked++;
            if (_nbCharacterLocked == _buttonsArray.Length)
                _buttonStartGame.SetActive(true);
        }
           
        
        if (player == 0)
            _buttonsArray[index].image.color = _ColorMyButtonLock;
        else
            _buttonsArray[index].image.color = _ColorOthersButtonLock;
    }
    [RPC]
    public void setButtonUninteractable(int[] index)
    {
        for (int i = 0; i < index.Length; i++ )
            _buttonsArray[index[i]].interactable = false;
    }
    [RPC]
    public void sendMyIdCharact(int index, string key)
    {
        List<Player> playerList = NetworkManager.Instance.getListPlayer();
        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i].getKey().Equals(key))
            {
                NetworkManager.Instance.setIdCharacter(i, index);
            }
        }
    }
   
    

}
