using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RefreshGameListScript : MonoBehaviour {

    public GameObject buttonToConnect;
    public RectTransform _rectTranform;
    private GameObject[] buttonListToConnect;
    private HostData[] hostData = new HostData[0];
    private bool refresh = false;
    private int index = 0;
	// Use this for initialization
	void Start () {
	  
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (MasterServer.PollHostList().Length > 0 && refresh)
        {
            hostData = MasterServer.PollHostList();
            addButtonGameList();
            refresh = false;
        }
	}
    public void refreshHostList()
    {
        MasterServer.RequestHostList("BHR");
        refresh = true;
    }
    public void addButtonGameList()
    {
        RectTransform rectTransform_current;
        Button button_current;
        buttonListToConnect = new GameObject[hostData.Length];
        if (_rectTranform.rect.height < hostData.Length * (30 + 5))
            _rectTranform.sizeDelta = new Vector2(_rectTranform.rect.width, (hostData.Length * (30 + 5)) );
        _rectTranform.anchoredPosition = new Vector2(0, -_rectTranform.rect.height / 2);
        
        for(int i = 0; i < hostData.Length; i++)
		{
            if (!(hostData[i].gameType.Equals("BHR")))
                return;
            index = i;
           
            buttonListToConnect[i] = Instantiate(buttonToConnect) as GameObject;
            buttonListToConnect[i].GetComponentInChildren<Text>().text = hostData[i].gameName;
            buttonListToConnect[i].name = i.ToString();
           
            rectTransform_current = buttonListToConnect[i].GetComponent<RectTransform>();
            button_current = buttonListToConnect[i].GetComponent<Button>();

            rectTransform_current.SetParent(transform);
            
            rectTransform_current.anchoredPosition = new Vector2(0, -i * (rectTransform_current.sizeDelta.y + 10) - rectTransform_current.sizeDelta.y/2);

      
            button_current.onClick.AddListener(() => { connectAtGame(index); });
           
		}
    }
    public void connectAtGame(int i)
    {
        Network.Connect(hostData[i]);
    }
}
