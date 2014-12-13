using UnityEngine;
using System.Collections;

public class OnClickPlayerScript : MonoBehaviour {

    public GameManagerScript _gameManager;
    public CharacterManager _characterManager;
    public int _idPlayer;
    public Color _ColorMyButtonLock;
    public Color _ColorMyButtonDeLock;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnMouseDown()
    {
        GameObject[] arrayCharacter = _characterManager.getCharactersArray();
        GameObject[] arrayCharacterPosition = _characterManager.getCharactersPositionsArray();
        GameObject currentCharacter;
        GameObject currentCharacterPosition;
        for (int i = 0; i < arrayCharacter.Length; i++ )
        {
            currentCharacter = arrayCharacter[i];
            currentCharacterPosition = arrayCharacterPosition[i];
            if(currentCharacter.Equals(gameObject))
            {
                currentCharacter.renderer.material.color = _ColorMyButtonLock;
                _gameManager.setIdPlayerActif(_idPlayer);
                _gameManager.setPlayerActif(currentCharacterPosition);
            }
            else
                currentCharacter.renderer.material.color = _ColorMyButtonDeLock;
        }
            
    }
}
