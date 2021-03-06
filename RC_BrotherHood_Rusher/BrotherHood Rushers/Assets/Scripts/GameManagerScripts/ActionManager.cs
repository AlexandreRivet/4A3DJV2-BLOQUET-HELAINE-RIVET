﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Globalization;

public class ActionManager : MonoBehaviour {

    private bool _moveBefore = true;
    private float _waitBefore = 0.0f;
    private string _typeObjectChosen = "";
    private string _actionChosen = "";
    private int _idCurrent = 0;
    private Dictionary<string,List<int>> _idsPlayersByAction = new Dictionary<string,List<int>>();
    [SerializeField]
    private GameObject _actionPanel;
    [SerializeField]
    private RectTransform _actionPanelList;
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
        //On définie quelles actions que chaque joueur peut faire.
	    _idsPlayersByAction.Add("PushBox", new List<int>{0});
        _idsPlayersByAction.Add("PullBox", new List<int>{0});
        _idsPlayersByAction.Add("Jump", new List<int>{0,1,2});
        _idsPlayersByAction.Add("Teleport", new List<int>{2});
        _idsPlayersByAction.Add("Pull", new List<int>{1});
        _idsPlayersByAction.Add("Destroy", new List<int>{1});
        _idsPlayersByAction.Add("LeverOn", new List<int>{2});
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
        NumberStyles style = NumberStyles.AllowDecimalPoint;
        CultureInfo culture = CultureInfo.CreateSpecificCulture("fr-FR");
        if (float.TryParse(value.text, style, culture, out _waitBefore))
            _waitBefore = float.Parse(value.text);
        else
            _waitBefore = 0.0f;
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
    public void setIdCurrent(int value)
    {
        _idCurrent = value;
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
    //Permet de récuperer la liste des actions effectuables sur un type d'objet en particulier
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

    //Affiche la liste des actions selon l'objet selectionné
    public void refreshActionLabel(string type)
    {
        string[] listAction = getActionByType(type);
        int inc = 0;
        _actionPanelList.anchoredPosition = new Vector2(0, 0);
        for(int i = 0; i < _actionTextList.Count(); i++)
        {
            _actionButtonList[i].SetActive(false);
            if (listAction == null || i > listAction.Length - 1)
                continue;
            string currentActionName = listAction[i];
            List<int> currentIdHarray = _idsPlayersByAction[currentActionName];
            if (currentIdHarray.Contains(_idCurrent))
            {
                _actionTextList[inc].text = currentActionName;
                _actionButtonList[inc].SetActive(true);
                inc++;
            }
                
        }
    }

}
