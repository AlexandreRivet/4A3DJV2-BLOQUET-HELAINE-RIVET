using UnityEngine;
using System.Collections;

public class OnClickPlayerScript : MonoBehaviour {

    [SerializeField]
    private GameManagerScript _gameManager;
    [SerializeField]
    private CharacterManager _characterManager;
    [SerializeField]
    private int _idPlayer;
    [SerializeField]
    private Color _ColorMyButtonLock;
    [SerializeField]
    private Color _ColorMyButtonDeLock;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //Si on clique sur un personnage, le sien devient vert, et les autres redeviennent blancs
    void OnMouseDown()
    {
        if (_gameManager.getIsReady())
            return;
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
