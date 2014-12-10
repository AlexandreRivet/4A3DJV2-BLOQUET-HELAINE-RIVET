using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderScript : MonoBehaviour {

    [SerializeField]
    Slider _slider;

    [SerializeField]
    GameObject _handleSlideArea;

    float _oldValue = 0.5f;

	// Use this for initialization
	void Start () 
    {
        _handleSlideArea.SetActive(false);
        _slider.value = _oldValue;
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void onDrag()
    {
        _slider.value = _oldValue;
    }

    public void onClick()
    {
        StopAllCoroutines();
        _oldValue = _slider.value;
        _handleSlideArea.SetActive(true);
        StartCoroutine(test());
        
    }

    IEnumerator test()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);
            _handleSlideArea.SetActive(false);
        }
    }


}
