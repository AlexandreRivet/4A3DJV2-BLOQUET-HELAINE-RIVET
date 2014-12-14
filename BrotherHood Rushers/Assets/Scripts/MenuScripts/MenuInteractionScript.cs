using UnityEngine;
using System.Collections;

public class MenuInteractionScript : MonoBehaviour {

	// Use this for initialization
	public void leaveGame()
    {
        Application.Quit();
    }
    public void loadLevel(string levelName)
    {
        Application.LoadLevel(levelName);
    }
}
