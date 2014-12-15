﻿using UnityEngine;
using System.Collections;


//Author: Bloquet Pierre
//Date :28/11/2014
//Last modification : 03/12/2014
//Descrition:
//Note: c'est juste du test, donc rien d'important

public class GameManagerScript : MonoBehaviour {


    [SerializeField]
    private int _idPlayerActif;
    private bool _menuInteractObjectActif = false;
    [SerializeField]
    private GameObject _gameObjectPlayerActif;
    private PileActions _pileActionPlayer1;
	private PileActions _pileActionPlayer2;
	private PileActions _pileActionPlayer3;
    private bool _startFakeSimulation = false;
    
    private float[] waitBeforeOtherAction = new float[]{0.0f,0.0f,0.0f};
    private bool[] firstTimeCallAction = new bool[] { true, true, true };
	// Use this for initialization
	void Start () {
		_pileActionPlayer1 = new PileActions();
        _pileActionPlayer2 = new PileActions();
        _pileActionPlayer3 = new PileActions();
	}
	
	// Update is called once per frame
	void Update () {
        if (!_startFakeSimulation)
            return;
      
        readPileAction(_pileActionPlayer1,0);
        readPileAction(_pileActionPlayer2,1);
        readPileAction(_pileActionPlayer3,2);
        
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
    public void addActionPlayer1(Action action)
    {
        _pileActionPlayer1.addActionPlayer(action);
    }
    public void addActionPlayer2(Action action)
    {
        _pileActionPlayer2.addActionPlayer(action);
    }
    public void addActionPlayer3(Action action)
    {
        _pileActionPlayer3.addActionPlayer(action);
    }
    public void switchFakeSimulation()
    {
        _startFakeSimulation = !_startFakeSimulation;
    }
    public void leaveApplication(string levelName)
    {
        Application.LoadLevel(levelName);
    }


    public void readPileAction(PileActions pileActions, int id)
    {
        Action currenAction;
    
        for (int i = 0; i < pileActions.getLength(); i++)
        {
           
            currenAction = pileActions.getAction(i);
           
            switch (currenAction.get_actionState())
            {
                case 0:
                    currenAction.set_actionState(1);
                    break;
                case 1:
                    playAction(currenAction, id);
                    return;
                case 2:
                    continue;
                case 3:
                    return;
            }

        }
    }
    public void playAction(Action action, int id)
    {
        string typeAction = action.get_typeAction();
 
        switch(typeAction)
        {
            case "Move":
                action.set_actionState(move(action.getCharacter(), action.getRangeMax().x));
                break;
            case "Grab":
                action.set_actionState(grab(action.getCharacter().transform, action.getTarget().transform));
                break;
            case "UnGrab":
                action.set_actionState(unGrab(action.getTarget().transform));
                break;
            case "Jump":
                action.set_actionState(jump(action.getCharacter().transform, action.getTarget().transform));
                break;
            case "Teleport":
                action.set_actionState(teleport(action.getCharacter().transform, action.getTarget().transform, action.getOtherGameObject()));
                break;
            case "Pull":
                action.set_actionState(pull(action.getOtherGameObjectById(0), action.getTarget().transform));
                break;
            case "Destroy":
                action.set_actionState(destroy(action.getTarget(), action.getOtherGameObjectById(0)));
                break;
            case "LeverOn":
                action.set_actionState(lever(action.getCharacter().transform,action.getOtherGameObjectById(0).transform, action.getOtherGameObjectById(1).transform, action.getOtherGameObjectById(2).transform, action.getOtherGameObjectById(3).transform));
                break;
            case "Wait":
                action.set_actionState(wait(action.get_informationById(0), id));
                break;
        }
    }
    public void setAllActionsOnCharacters()
    {

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

        float step = 2.0f * Time.deltaTime;
        Quaternion newRotation = Quaternion.Lerp(DoorTransform.rotation, targetTransform.rotation, step);
        DoorTransform.rotation = newRotation;
        if (newRotation == targetTransform.rotation)
            return 2;


        return 1;
    }
}
