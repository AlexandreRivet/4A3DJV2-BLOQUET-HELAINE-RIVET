using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class OnClickMarker : MonoBehaviour {

    [SerializeField]
    SlidersManagerScript _sliderManager;
    

    Marker _marker = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void setSliderManager(SlidersManagerScript sliderManager)
    {
        _sliderManager = sliderManager;
    }
    public void setMarker(Marker marker)
    {
        _marker = marker;
    }
    public Marker getMarker()
    {
        return _marker;
    }
    public void OnClick()
    {
        if (_marker == null)
            return;
        List<Action> actionList = _marker.getActionList();
        _sliderManager.setActiveMarkerPanel(true);
        _sliderManager.setActiveMarkerActionAll(false);
        for (int i = 0; i < actionList.Count; i++)
        {
            _sliderManager.setActiveMarkerAction(i,true);
            _sliderManager.setTextMarkerLabelAction(i,actionList[i].get_typeAction());
        }
    }
}
