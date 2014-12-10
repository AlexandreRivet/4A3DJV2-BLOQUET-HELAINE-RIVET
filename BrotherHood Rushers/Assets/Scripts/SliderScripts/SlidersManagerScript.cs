using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlidersManagerScript : MonoBehaviour {

    [SerializeField]
    Slider[] _sliders;

    [SerializeField]
    RectTransform[] _slidersRect;

    [SerializeField]
    Transform _sliderParent;

    [SerializeField]
    float _xLevelStart = 0.0f;

    [SerializeField]
    float _xLevelEnd = 0.0f;

    [SerializeField]
    float _scaleFactor = 0.1f;

    [SerializeField]
    float _offsetYSliders = 15.0f;

    [SerializeField]
    Button _markerModel;

    float[] lastIndex = new float[] { 0, 0, 0 };

	// Use this for initialization
	void Start () 
    {
        // Sliders initialization
        if (_slidersRect.Length != 3)
            return;

        for (int i = 0; i < _slidersRect.Length; i++)
        {
            float x = _xLevelStart * (1 / _scaleFactor);
            float y = _slidersRect[i].anchoredPosition3D.y;
            float width = (1 / _scaleFactor) * (_xLevelEnd - _xLevelStart);
            float height = _slidersRect[i].rect.height;

            _slidersRect[i].sizeDelta = new Vector2(width, height);
            _slidersRect[i].anchoredPosition = new Vector2(x, y);

            // Debug.Log("(" + x + "," + y + ")"); -> Controle de la nouvelle position
        }	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void sendPositionInScene(int index)
    {
        // Calcul de la position dans la scène
        float value = _sliders[index].value;
        float position = value * (_xLevelEnd - _xLevelStart) + _xLevelStart;

        // Création du marker
        createMarker(index, position);

        // Ajouter d'autres traitements ?
    }

    void createMarker(int index, float position)
    {
        // Création du button
        Button but = (Button)Instantiate(_markerModel);
        but.name = "Marker_" + index + "_" + lastIndex[index];
        but.transform.localScale = new Vector3(_scaleFactor, _scaleFactor, _scaleFactor);
        but.transform.SetParent(_sliderParent);

        // Placement du button
        RectTransform rect_transform = but.GetComponent<RectTransform>();
        rect_transform.pivot = new Vector2(0.5f, 0);
        rect_transform.anchoredPosition3D = new Vector3(0, 0, 0);
        rect_transform.anchoredPosition = new Vector2(position * (1 / _scaleFactor), -(index * _offsetYSliders));
        //TODO: petit problème de décalage dans les button trouver pourquoi mm si ça doit être lié au slider

        // Mise à jour du prochain ID
        lastIndex[index]++;
    }
}
