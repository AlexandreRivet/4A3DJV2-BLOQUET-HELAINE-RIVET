using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class ReplayManager : MonoBehaviour {

    public static ReplayManager Instance { get; private set; }

    [SerializeField]
    GameObject[] _listButtonReplay = new GameObject[0];
    [SerializeField]
    Text[] _listTextReplay = new Text[0];

    private string _dataPileAction = "";
    private int _levelId = 0;
    PileActions[] _replayPileActions = null;
    private bool _isReplay = false;
	// Use this for initialization
    void Awake()
    {
        Instance = this;
    }
	void Start () {
        DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Systeme de load de Replay

    public void displayListGameSave()
    {
       
        int incId = 1;
        for(int i = 1; i < 3; i++)
        {
            incId = 1;
            Debug.Log(PlayerPrefs.GetInt((i - 1) + ":Save"));
            
            int numberSave = PlayerPrefs.GetInt(i + ":Save");
            int numberSavePrevious = PlayerPrefs.GetInt((i - 1) + ":Save");
            Debug.Log("Nbsave " + numberSave);
            for (int j = numberSavePrevious; j < _listButtonReplay.Length; j++)
            {
                
                _listButtonReplay[j].SetActive(false);

                if (numberSave == 0 || incId > numberSave )
                    continue;

                _listButtonReplay[j].SetActive(true);
                _listTextReplay[j].text = i + ":Game N°: " + incId;
                Debug.Log(j + " " + incId);
                incId++;
            }
        }

    }
    public void clearAllSave()
    {
        for (int i = 1; i < 3; i++)
        {
            int nbSave = PlayerPrefs.GetInt(i + ":Save");
            for (int j = 1; j < nbSave + 1; j++)
            {
                PlayerPrefs.DeleteKey(i+":Game N°: " + i);
            }
            PlayerPrefs.DeleteKey(i + ":Save");
        }
    }
    public void loadLevelReplay(Text replayName)
    {
        string replayNameS = replayName.text;
        _levelId = int.Parse(replayNameS.Substring(0, 1));
        _dataPileAction = PlayerPrefs.GetString(replayNameS);
        if (!string.IsNullOrEmpty(_dataPileAction))
        {
            //BinaryFormatter puis renvoyer les données
            var b = new BinaryFormatter();
            //Création d'un MemoryStream avec les données
            var m = new MemoryStream(Convert.FromBase64String(_dataPileAction));
            //Charger les nouveaux scores
            _replayPileActions = (PileActions[])b.Deserialize(m);
            _isReplay = true;
            Application.LoadLevel(_levelId);
        }
    }
    public void setIsReplay(bool value)
    {
        _isReplay = value;
    }
    public bool getIsReplay()
    {
        return _isReplay;
    }
    public PileActions[] getPileActions()
    {
        return _replayPileActions;
    }
}
