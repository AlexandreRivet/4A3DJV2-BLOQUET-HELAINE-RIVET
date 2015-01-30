using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SecurityConnectionServerScript : MonoBehaviour {
    [SerializeField]
    private GameObject _labelError;
    [SerializeField]
    private Text _labelErrorText;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnFailedToConnectToMasterServer(NetworkConnectionError info)
    {
        _labelErrorText.text = "Could not connect to master server: " + info;
        _labelError.SetActive(true);
    }
    public void OnMasterServerEvent(MasterServerEvent mse)
    {
        //Registration failed because an empty game name was given.
        if (mse == MasterServerEvent.RegistrationFailedGameName)
        {
            _labelErrorText.text = "Registration failed because an empty game name was given.";
            _labelError.SetActive(true);
        }
        //Registration failed because an empty game type was given. 
        else if (mse == MasterServerEvent.RegistrationFailedGameType)
        {
            _labelErrorText.text = "Registration failed because an empty game type was given.";
            _labelError.SetActive(true);
        }
        //Registration failed because no server is running. 
        else if (mse == MasterServerEvent.RegistrationFailedNoServer)
        {
            _labelErrorText.text = "Registration failed because no server is running. ";
            _labelError.SetActive(true);
        }
        
    }

}
