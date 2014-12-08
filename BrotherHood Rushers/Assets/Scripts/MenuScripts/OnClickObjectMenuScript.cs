using UnityEngine;
using System.Collections;

public class OnClickObjectMenuScript : MonoBehaviour {

    public GameObject _objectPanelActive;
    public GameObject _objectMenuActive;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        _objectPanelActive.SetActive(true);
        _objectMenuActive.SetActive(true);
    }
}
