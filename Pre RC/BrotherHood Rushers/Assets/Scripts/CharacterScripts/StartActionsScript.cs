using UnityEngine;
using System.Collections;

public class StartActionsScript : MonoBehaviour {
    private PileActions _pileActions = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if ( this._pileActions == null)
            return;
        for(int i = 0; i < _pileActions.getLength(); i++  )
        {
           switch(_pileActions.getAction(i).get_actionState())
           {
               case 0:
                   break;
               case 1:
                   break;
               case 2:
                   break;
           }
            
        }
	}

    public void setPileActions(PileActions pileActions)
    {
        this._pileActions = pileActions;
    }
    public PileActions getPileActions()
    {
        return this._pileActions;
    }
}
