using UnityEngine;
using System.Collections;

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
        _sliderManager.destroyMarker(gameObject);
    }
}
