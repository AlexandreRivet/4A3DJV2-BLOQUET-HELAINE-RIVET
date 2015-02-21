using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class ActionManager : MonoBehaviour {

    private bool _moveBefore = true;
    private float _waitBefore = 0.0f;
    private string _typeObjectChosen = "";
    private string _actionChosen = "";

    [SerializeField]
    private GameObject _actionPanel;
    [SerializeField]
    private List<Text> _actionTextList = new List<Text>();
    [SerializeField]
    private List<GameObject> _actionButtonList = new List<GameObject>();



    [SerializeField]
    private List<ActionDatas> _typeBox_Object = new List<ActionDatas>();
    [SerializeField]
    private string[] _typeBox_Action;

    [SerializeField]
    private List<ActionDatas> _typePlateformS_Object = new List<ActionDatas>();
    [SerializeField]
    private string[] _typePlateformS_Action;

    [SerializeField]
    private List<ActionDatas> _typePlateformM_Object = new List<ActionDatas>();
    [SerializeField]
    private string[] _typePlateformM_Action;

    [SerializeField]
    private List<ActionDatas> _typePlant_Object = new List<ActionDatas>();
    [SerializeField]
    private string[] _typePlant_Action;

    [SerializeField]
    private List<ActionDatas> _typeTeleporter_Object = new List<ActionDatas>();
    [SerializeField]
    private string[] _typeTeleporter_Action;

    [SerializeField]
    private List<ActionDatas> _typeLever_Object = new List<ActionDatas>();
    [SerializeField]
    private string[] _typeLever_Action;




	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void setActionActionPanel(bool value)
    {
        _actionPanel.SetActive(value);
    }

    //setters
    public void setMoveBefore(bool  value)
    {
        _moveBefore = value;
    }
    public void setMoveBeforeByToggle(Toggle value)
    {
        _moveBefore = value.isOn;
    }
    public void setWaitBefore(float value)
    {
        _waitBefore = value;
    }
    public void setWaitBeforeByFIeldText(InputField value)
    {
        _waitBefore = float.Parse(value.text);
    }
    public void setTypeObjectChosen(string value)
    {
        _typeObjectChosen = value;
    }
    public void setActionChosen(string value)
    {
        _actionChosen = value;
    }
    public void setTypeObjectChosenByText(Text value)
    {
        _typeObjectChosen = value.text;
    }
    public void setActionChosenByText(Text value)
    {
        _actionChosen = value.text;
    }
    public void setObjectByType(string type, ActionDatas go)
    {
        switch (type)
        {
            case "Box":
                _typeBox_Object.Add(go);
                break;
            case "PlateformS":
                _typePlateformS_Object.Add(go);
                break;
            case "PlateformM":
                _typePlateformM_Object.Add(go);
                break;
            case "Plant":
                _typePlant_Object.Add(go);
                break;
            case "Teleporter":
                _typeTeleporter_Object.Add(go);
                break;
            case "Lever":
                _typeLever_Object.Add(go);
                break;
        }
    }

    public void setActionByType(string type, string action)
    {

    }
 
    //getters
    public bool getMoveBefore()
    {
        return _moveBefore;
    }
    public float getWaitBefore()
    {
        return _waitBefore;
    }
    public string getTypeObjectChosen()
    {
        return _typeObjectChosen;
    }
    public string getActionChosen()
    {
        return _actionChosen;
    }

    public List<ActionDatas> getObjectByType(string type)
    {
        switch(type)
        {
            case "Box":
                return _typeBox_Object;
            case "PlateformS":
                return _typePlateformS_Object;
            case "PlateformM":
                return _typePlateformM_Object;
            case "Plant":
                return _typePlant_Object;
            case "Teleporter":
                return _typeTeleporter_Object;
            case "Lever":
                return _typeLever_Object;
        }
        return null;
    }

    public string[] getActionByType(string type)
    {
        switch (type)
        {
            case "Box":
                return _typeBox_Action;
            case "PlateformS":
                return _typePlateformS_Action;
            case "PlateformM":
                return _typePlateformM_Action;
            case "Plant":
                return _typePlant_Action;
            case "Teleporter":
                return _typeTeleporter_Action;
            case "Lever":
                return _typeLever_Action;
        }
        return null;
    }

    //Display Action Label by Type Object
    public void refreshActionLabel(string type)
    {
        string[] listAction = getActionByType(type);
        
        for(int i = 0; i < _actionTextList.Count(); i++)
        {
            _actionButtonList[i].SetActive(false);
            if (listAction != null && i < listAction.Length)
            {
                _actionTextList[i].text = listAction[i];
                _actionButtonList[i].SetActive(true);
            }
                
        }
    }

}
