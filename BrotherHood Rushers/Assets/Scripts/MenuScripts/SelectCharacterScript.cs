using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectCharacterScript : MonoBehaviour {

    public GameObject _characterUsed;
    public CharacterManager _characterManager;
    public GameManagerScript _gameManager;
    public Color _ColorMyButtonLock;
    public Color _ColorOthersButtonLock;

    public GameObject _panelSelectCharacter;
    public Button[] _buttonsArray;
    public GameObject _buttonStartGame;

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

}
