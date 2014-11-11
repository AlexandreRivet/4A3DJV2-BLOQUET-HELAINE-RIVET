using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateServerScripts : MonoBehaviour {

    public string _privateName = "";
    public int _port = 21000;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void setPrivateName(InputField name)
    {
        _privateName = name.text;
    }
    public void setPort(InputField port)
    {
        _port = int.Parse(port.text);
    }
    public void StartServer()
    {
        Network.InitializeServer(32, _port, !Network.HavePublicAddress());
        MasterServer.RegisterHost("BHR", _privateName, "Welcome to Brotherhood Runners");
    }

    public void OnServerInitialized()
    {
        Debug.Log("Server initialized");
    }

    public void OnMasterServerEvent(MasterServerEvent mse)
    {
	    if(mse == MasterServerEvent.RegistrationSucceeded)
	    {
            Debug.Log("Connection Succeful");
	    }
    }
}
