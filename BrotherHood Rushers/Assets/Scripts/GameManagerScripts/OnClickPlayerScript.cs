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
        GameObject currentCharacter;
        for (int i = 0; i < arrayCharacter.Length; i++ )
        {
            currentCharacter = arrayCharacter[i];
   
            if(currentCharacter.Equals(gameObject))
            {
                currentCharacter.renderer.material.color = _ColorMyButtonLock;
                _gameManager.setIdPlayerActif(_idPlayer);
                _gameManager.setPlayerActif(gameObject);
            }
            else
                currentCharacter.renderer.material.color = _ColorMyButtonDeLock;
        }
            
    }
}
