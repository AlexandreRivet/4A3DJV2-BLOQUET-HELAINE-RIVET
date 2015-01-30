using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

// Script qui contient la plupard des fonctions qui font tourner le jeu, en particuliuer c'est ce dernier qui lis les listes des actions des joueurs

public class GameManagerScript : MonoBehaviour {


    [SerializeField]
    private int _idMyCharacter;
    
    [SerializeField]
    private int _idPlayerActif;
    private bool _menuInteractObjectActif = false;

    [SerializeField]
    private CharacterManager _characterManager;
    [SerializeField]
    private GameObject _gameObjectPlayerActif;

    [SerializeField]
    private ResetLevelScript _resetFunctions;
    [SerializeField]
    private GameObject[] _panelsToSetActive;
    [SerializeField]
    private GameObject _buttonIsReady;

    [SerializeField]
    private GameObject _panelSelectCharacter;
    
    [SerializeField]
    private Transform _FinishZone;


    [SerializeField]
    private GameObject _winPanel;

    private PileActions[] _pileActionPlayers = new PileActions[3];
    //private PileActions[] _pileActionPlayer2;
	//private PileActions _pileActionPlayer3;
    private bool _startFakeSimulation = false;
    
    private float[] waitBeforeOtherAction = new float[]{0.0f,0.0f,0.0f};
    private bool[] firstTimeCallAction = new bool[] { true, true, true };

    private bool _isReady = false;
    private int _nbOfPlayerReady = 0;
    private int _nbListActionReceive = 0;
	// Use this for initialization
	void Start () {
        if (Network.connections.Length == 0)
        {
            _buttonIsReady.SetActive(false);
            _panelSelectCharacter.SetActive(false);
        }
           
        _pileActionPlayers[0] = new PileActions();
        _pileActionPlayers[1] = new PileActions();
        _pileActionPlayers[2] = new PileActions();
	}
	
	// Update is called once per frame
	void Update () {
        if (!_startFakeSimulation)
            return;

        int statej1 = 0, statej2 = 0, statej3 = 0;
       // on lis les actions des joueurs si on a cliqué sur Start Fake Simulation
        statej1 = readPileAction(_pileActionPlayers[0], 0);
        statej2 = readPileAction(_pileActionPlayers[1], 1);
        statej3 = readPileAction(_pileActionPlayers[2], 2);

        if ((statej1 == 2 || statej1 == 3) && (statej2 == 2 || statej2 == 3) && (statej3 == 2 || statej3 == 3) && _isReady == true)
        {
            Vector3 finalPosition = _FinishZone.position;
            Vector3 positionJ1 = _characterManager.getCharactersPositionByIndex(0).transform.position;
            Vector3 positionJ2 = _characterManager.getCharactersPositionByIndex(1).transform.position;
            Vector3 positionJ3 = _characterManager.getCharactersPositionByIndex(2).transform.position;

            if (Vector3.Distance(finalPosition,positionJ1) <= 2100 && Vector3.Distance(finalPosition,positionJ2) <= 2100 && Vector3.Distance(finalPosition,positionJ3) <= 21000)
            {

                StartCoroutine(Wait(5.0f));
                
            }
            startANewLap();
        }
        
	}
    private IEnumerator Wait(float seconds)
    {
        _winPanel.SetActive(true);
        yield return new WaitForSeconds(seconds);
        Network.Disconnect();
        Application.LoadLevel(0);
    }
    //Fonction pour reset les listes des actions
    public void resetPileActions(bool force)
    {
        if (force)
        {
            _pileActionPlayers[0] = new PileActions();
            _pileActionPlayers[1] = new PileActions();
            _pileActionPlayers[2] = new PileActions();
        }
        else
        {
            for(int i = 0; i < _pileActionPlayers.Count(); i++)
            {
                for (int j = 0; j < _pileActionPlayers[i].getLength(); j++)
                {
                    _pileActionPlayers[i].getAction(j).set_actionState(0);
                }
            }
        }
    }
    public void setIdMyCharacter(int id)
    {
        _idMyCharacter = id;
    }
    public int getIdMyCharacter()
    {
        return _idMyCharacter;
    }
    public void setIdPlayerActif(int id)
    {
        _idPlayerActif = id;
    }
    public int getIdPlayerActif()
    {
       return  _idPlayerActif;
    }
    public void setMenuInteractObjectActif(bool value)
    {
        _menuInteractObjectActif = value;
    }
    public bool getMenuInteractObjectActif()
    {
        return _menuInteractObjectActif;
    }
    public void setPlayerActif(GameObject player)
    {
        _gameObjectPlayerActif = player;
    }
    public GameObject getPlayerActif()
    {
        return _gameObjectPlayerActif;
    }
    public void addActionPlayers(int id,Action action)
    {
        _pileActionPlayers[id].addActionPlayer(action);
    }
    public void removeAction(int id, Action action)
    {
        _pileActionPlayers[id].removeActionPlayer(action);
    }
    /*public void addActionPlayer2(Action action)
    {
        _pileActionPlayer2.addActionPlayer(action);
    }
    public void addActionPlayer3(Action action)
    {
        _pileActionPlayer3.addActionPlayer(action);
    }*/
    public void switchFakeSimulation()
    {
        _startFakeSimulation = !_startFakeSimulation;
    }
    public void setFakeSimulation(bool value)
    {
        _startFakeSimulation = value;
    }
    public void leaveApplication(string levelName)
    {
        Application.LoadLevel(levelName);
    }

    //Fonction qui lis une pile d'action
    public int readPileAction(PileActions pileActions, int id)
    {
        Action currenAction;
        int state = 0;
        for (int i = 0; i < pileActions.getLength(); i++)
        {
            //Une action est définie par un état 0: action pas commencée 1: action en cour 2: action terminée 3: action interrompue
            currenAction = pileActions.getAction(i);
            //Debug.Log(currenAction.get_actionState());
            switch (currenAction.get_actionState())
            {
                case 0:
                    currenAction.set_actionState(1);
                    return 0;
                case 1:
                    playAction(currenAction, id);
                    return 1;
                case 2:
                    state = 2;
                    continue;
                case 3:
                    return 3;
            }

        }

        return state;
    }
    //Lancement de l'action
    public void playAction(Action action, int id)
    {
        string typeAction = action.get_typeAction();
        // _characterManager.getObjectLevelById(action.get_sceneIdObject(1))
        //Selon le type, une action sera lancée
        switch(typeAction)
        {
            case "Move":
                action.set_actionState(move(_characterManager.getObjectLevelById(action.getIdCharacter()), action.get_informationById(0)));
                break;
            case "Grab":
                action.set_actionState(grab(_characterManager.getObjectLevelById(action.getIdCharacter()).transform, _characterManager.getObjectLevelById(action.getIdTarget()).transform));
                break;
            case "UnGrab":
                action.set_actionState(unGrab(_characterManager.getObjectLevelById(action.getIdTarget()).transform));
                break;
            case "Jump":
                action.set_actionState(jump(_characterManager.getObjectLevelById(action.getIdCharacter()).transform, _characterManager.getObjectLevelById(action.getIdTarget()).transform));
                break;
            case "Teleport":
                action.set_actionState(teleport(_characterManager.getObjectLevelById(action.getIdCharacter()).transform, _characterManager.getObjectLevelById(action.getIdTarget()).transform, _characterManager.getObjectLevelArrayById(action.get_allSceneIdObject() ) ));
                break;
            case "Pull":
                action.set_actionState(pull(_characterManager.getObjectLevelById(action.get_sceneIdObject(0)), _characterManager.getObjectLevelById(action.getIdTarget()).transform));
                break;
            case "Destroy":
                action.set_actionState(destroy(_characterManager.getObjectLevelById(action.getIdTarget()), _characterManager.getObjectLevelById(action.get_sceneIdObject(0))));
                break;
            case "LeverOn":
                action.set_actionState(lever(_characterManager.getObjectLevelById(action.getIdCharacter()).transform, _characterManager.getObjectLevelById(action.get_sceneIdObject(0)).transform, _characterManager.getObjectLevelById(action.get_sceneIdObject(1)).transform, _characterManager.getObjectLevelById(action.get_sceneIdObject(2)).transform, _characterManager.getObjectLevelById(action.get_sceneIdObject(3)).transform));
                break;
            case "Wait":
                action.set_actionState(wait(action.get_informationById(0), id));
                break;
        }
    }
    public void sendMyActionList()
    {
        sendActionList(_idMyCharacter);
    }

    //Serialisation des données des listes des actions pour l'envoyer au autres joueurs afin de lancer la simulation
    public void sendActionList(int id)
    {
        //Création d'un BinaryFormatter 
        var b = new BinaryFormatter();
        //Création d'un MemoryStream
        var m = new MemoryStream();
        for (int i = 0; i < _pileActionPlayers[id].getLength(); i++)
        {
           _pileActionPlayers[id].getAction(i).set_actionState(0);
        }
       
        b.Serialize(m, _pileActionPlayers[id]);
        //Addition à PlayerPrefs
        string message =  Convert.ToBase64String(m.GetBuffer());
        networkView.RPC("receiveActionList", RPCMode.Others, message, id);
    }

    [RPC]
    public void receiveActionList(string data, int id)
    {
        
        if (!string.IsNullOrEmpty(data))
        {
            //BinaryFormatter puis renvoyer les données
            var b = new BinaryFormatter();
            //Création d'un MemoryStream avec les données
            var m = new MemoryStream(Convert.FromBase64String(data));
           
            _pileActionPlayers[id] = (PileActions)b.Deserialize(m);
            _nbListActionReceive++;
            Debug.Log("nbListActionReceive: " + _nbListActionReceive);
            if (_nbListActionReceive == 2)
            {
                _nbListActionReceive = 0;
                _startFakeSimulation = true;
                _panelsToSetActive[0].SetActive(false);
            }
                
            
        }
    }
    
    public void sendIsReadyAtAll()
    {
        _isReady = true;
        _startFakeSimulation = false;
        _resetFunctions.resetPositions();
        _panelsToSetActive[0].SetActive(true);
        _panelsToSetActive[1].SetActive(false);
        _panelsToSetActive[2].SetActive(false);
        _panelsToSetActive[3].SetActive(false);
        networkView.RPC("isReadyToMe", RPCMode.All);
    }
    public void startANewLap()
    {
        _resetFunctions.resetLevel();
        _isReady = false;
        _startFakeSimulation = false;
        _panelsToSetActive[0].SetActive(false);
        _panelsToSetActive[2].SetActive(true);
        _panelsToSetActive[3].SetActive(true);
    }

    [RPC]
    public void isReadyToMe()
    {
        _nbOfPlayerReady++;
        Debug.Log("_nbOfPlayerReady: " + _nbOfPlayerReady);
        if(_nbOfPlayerReady == 3)
        {
            _nbOfPlayerReady = 0;
            sendActionList(_idMyCharacter);
        }
    }

    public bool getIsReady()
    {
        return _isReady;
    }



    //ACTIONS FUNCTIONS !!//

    public int move(GameObject objectWhoMove, float _targetMove)
    {

        //Attention truc dégueulasse avant la purge du code afin de réparer un "bug"
        Transform[] childrensObjects = new Transform[objectWhoMove.transform.childCount];
        for (int i = 0; i < childrensObjects.Length; i++)
            childrensObjects[i] = objectWhoMove.transform.GetChild(i);

        Transform childrenObject = childrensObjects[0];
        childrenObject.rigidbody.useGravity = true;
        float step = 4.0f * Time.deltaTime;
        Vector3 objectPosition = objectWhoMove.transform.position;
        Vector3 newPosition = Vector3.MoveTowards(objectWhoMove.transform.position, new Vector3(_targetMove, objectPosition.y, objectPosition.z), step);
        objectWhoMove.transform.position = newPosition;
        if (newPosition.x == _targetMove)
        {
            childrenObject.rigidbody.useGravity = false;
            objectWhoMove.transform.DetachChildren();
            objectWhoMove.transform.position = new Vector3(objectWhoMove.transform.position.x, childrenObject.position.y - 1.0f, objectWhoMove.transform.position.z);
            for (int i = 0; i < childrensObjects.Length; i++)
                childrensObjects[i].SetParent(objectWhoMove.transform);
            
            return 2;
        }
            

        return 1;
    }

    public int jump(Transform objectWhoMove, Transform targetMove)
    {
        if (Mathf.Abs(targetMove.position.x - objectWhoMove.position.x) > 6 || Mathf.Abs(targetMove.position.y - objectWhoMove.position.y) > 4)
            return 3;
        Transform childrenObject = objectWhoMove.GetChild(0);
        childrenObject.rigidbody.useGravity = false;
        if(childrenObject.gameObject.GetComponent<OnContactObjectScript>().isContact())
        {
            childrenObject.rigidbody.useGravity = true;
            return 3;
        }
        float step = 4.0f * Time.deltaTime;
        Vector3 objectPosition = objectWhoMove.position;

        if (objectPosition.y != targetMove.position.y)
        {
            Vector3 newPosition = Vector3.MoveTowards(objectWhoMove.position, new Vector3(objectPosition.x, targetMove.position.y, objectPosition.z), step);
            objectWhoMove.position = newPosition;
        }
        else if (objectPosition.x != targetMove.position.x)
        {
            Vector3 newPosition = Vector3.MoveTowards(objectWhoMove.transform.position, new Vector3(targetMove.position.x, objectPosition.y, objectPosition.z), step);
            objectWhoMove.position = newPosition;
        }
       

        if (objectWhoMove.position.x == targetMove.position.x && objectWhoMove.position.y == targetMove.position.y)
            return 2;
            

        return 1;
    }
    public int wait(float timeToWait, int id)
    {
        if (firstTimeCallAction[id])
        {
            firstTimeCallAction[id] = false;
            waitBeforeOtherAction[id] = Time.time + timeToWait;
        }
            

        if (Time.time >= waitBeforeOtherAction[id])
        {
            firstTimeCallAction[id] = true;
            return 2;
        }
        return 1;
    }
    public int grab(Transform objectTransform, Transform targetTransform)
    {
        if (Mathf.Abs(targetTransform.position.x - objectTransform.position.x) > 4 || Mathf.Abs(targetTransform.position.y - objectTransform.position.y) > 4)
            return 3;
        targetTransform.SetParent(objectTransform);
        return 2;
    }
    
    public int unGrab(Transform targetTransform)
    {
        targetTransform.SetParent(null);
        return 2;
    }

    public int teleport(Transform objectWhoMove, Transform targetTransform, GameObject[] otherObjectInformation)
    {
        Debug.Log("1 " + otherObjectInformation[0].transform.position.x + " " + objectWhoMove.gameObject.name + " " + objectWhoMove.position.x);
        Debug.Log("1 " + otherObjectInformation[0].transform.position.x + " " + targetTransform.gameObject.name + " " + targetTransform.position.x + " " + targetTransform.position.y);
       
        //Debug.Log("3 " + !otherObjectInformation[1].activeSelf + " " + otherObjectInformation[2].activeSelf);
        
        if (Mathf.Abs(otherObjectInformation[0].transform.position.x - objectWhoMove.position.x) > 1.5 || Mathf.Abs(otherObjectInformation[0].transform.position.y - objectWhoMove.position.y) > 1.5)
            return 3;
        else if (Mathf.Abs(otherObjectInformation[0].transform.position.x - targetTransform.position.x) > 12 || Mathf.Abs(otherObjectInformation[0].transform.position.y - targetTransform.position.y) > 15)
        {
            return 3;
        }
        if(otherObjectInformation.Length == 3)
        {
            if(otherObjectInformation[1].activeSelf || otherObjectInformation[2].activeSelf)
            {
                return 3;
            }
        }
        
        objectWhoMove.position = new Vector3(targetTransform.position.x, targetTransform.position.y, targetTransform.position.z);
        return 2;
    }

    public int pull(GameObject objectWhoMove, Transform targetTransform)
    {
        float step = 2.0f * Time.deltaTime;
        Vector3 objectPosition = objectWhoMove.transform.position;
        Vector3 newPosition = Vector3.MoveTowards(objectWhoMove.transform.position, new Vector3(objectPosition.x, targetTransform.position.y, objectPosition.z), step);
        objectWhoMove.transform.position = newPosition;
        if (newPosition.y == targetTransform.position.y)
        {
            targetTransform.gameObject.SetActive(false);
            return 2;
        }
           


        return 1;
    }
    public int destroy(GameObject target, GameObject otherInformation)
    {
        if (otherInformation.activeSelf)
            return 3;
        target.SetActive(false) ;
        return 2;
    }
    public int lever(Transform objectWhoMove,Transform LeverTransform, Transform targetTransform, Transform DoorTransform, Transform targetTransform2)
    {

        if (Mathf.Abs(LeverTransform.position.x - objectWhoMove.position.x) > 4 || Mathf.Abs(LeverTransform.position.y - objectWhoMove.position.y) > 4)
            return 3;
        float step = 2.0f * Time.deltaTime;
        Quaternion newRotation = Quaternion.Lerp(LeverTransform.rotation, targetTransform.rotation,step);
        LeverTransform.rotation = newRotation;
        if (newRotation == targetTransform.rotation)
            return door(DoorTransform, targetTransform2);


        return 1;
    }
    public int door(Transform DoorTransform, Transform targetTransform)
    {

        float step = 3.0f * Time.deltaTime;
        Vector3 objectPosition = DoorTransform.position;
        Vector3 newPosition = Vector3.MoveTowards(DoorTransform.position, new Vector3(objectPosition.x, targetTransform.position.y, objectPosition.z), step);
        DoorTransform.position = newPosition;
        if (newPosition.y == targetTransform.position.y)
        {
            targetTransform.gameObject.SetActive(false);
            return 2;
        }
        return 1;
        //Version portes qui s'ouvre en rotation
        /*float step = 2.0f * Time.deltaTime;
        Quaternion newRotation = Quaternion.Lerp(DoorTransform.rotation, targetTransform.rotation, step);
        DoorTransform.rotation = newRotation;
        if (newRotation == targetTransform.rotation)
            return 2;


        return 1;*/
    }
}
