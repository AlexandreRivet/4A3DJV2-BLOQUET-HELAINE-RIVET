using UnityEngine;
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
    public void startFakeSimulation()
    {
        _startFakeSimulation = true;
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
            case "Jump":
                action.set_actionState(jump(action.getCharacter(), action.getTarget()));
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
        
        float step = 2.0f * Time.deltaTime;
        Vector3 objectPosition = objectWhoMove.transform.position;
        Vector3 newPosition = Vector3.MoveTowards(objectWhoMove.transform.position, new Vector3(_targetMove, objectPosition.y, objectPosition.z), step);
        objectWhoMove.transform.position = newPosition;
        if (newPosition.x == _targetMove)
            return 2;

        return 1;
    }

    public int jump(GameObject objectWhoMove, GameObject _targetMove)
    {
        float step = 2.0f * Time.deltaTime;
        Vector3 objectPosition = objectWhoMove.transform.position;
        Vector3 newPosition = Vector3.MoveTowards(objectWhoMove.transform.position, new Vector3(_targetMove.transform.position.x, _targetMove.transform.position.y, objectPosition.z), step);
        objectWhoMove.transform.position = newPosition;
        if (newPosition.x == _targetMove.transform.position.x && newPosition.y == _targetMove.transform.position.y)
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
        targetTransform.SetParent(objectTransform);
        return 2;
    }
    
    public int unGrabe(Transform targetTransform)
    {
        targetTransform.SetParent(null);
        return 2;
    }

    public int teleport()
    {
        return 2;
    }

    public int pull()
    {
        return 2;
    }
}
