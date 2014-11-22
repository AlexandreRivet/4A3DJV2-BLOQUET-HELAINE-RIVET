using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectCharacterScript : MonoBehaviour {

    public GameObject _characterUsed;
    public CharacterManager _characterManager;
    
    public Color _ColorMyButtonLock;
    public Color _ColorOthersButtonLock;

    public Button[] _buttonsArray;

    public void selectMyButton(int index)
    {
        setColorButton(index, 0);
        networkView.RPC("setColorButton", RPCMode.Others, index, 1);

        setButtonUninteractable(new int[] { 0, 1, 2 });
        networkView.RPC("setButtonUninteractable", RPCMode.Others, new int[]{index});

        _characterUsed = _characterManager.getCharactersByIndex(index);
        _characterManager.setMainColorByIndex(index, _ColorMyButtonLock);
    }


    [RPC]
    public void setColorButton(int index, int player)
    {
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
