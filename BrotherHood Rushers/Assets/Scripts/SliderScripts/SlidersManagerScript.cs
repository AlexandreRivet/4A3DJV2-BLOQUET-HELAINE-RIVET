using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SlidersManagerScript : MonoBehaviour {

    [SerializeField]
    GameManagerScript _gameManager;

    [SerializeField]
    CharacterManager _characterManager;

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

    /*[SerializeField]
    List<Button>[] _markersArray = new List<Button>[3];*/

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
        //TODO: faire un tableau de liste d'actions
        _gameManager.addActionPlayers(index, new Action(index, new List<float>{position}, -1, null, "Move"));
        /*switch (index)
        {
            case 0:
                _gameManager.addActionPlayer1(new Action(0, new Vector3(position, 0, 0), null, -1, null, "Move"));
                break;
            case 1:
                _gameManager.addActionPlayer2(new Action(1, new Vector3(position, 0, 0), null, -1, null, "Move"));
                break;
            case 2:
                _gameManager.addActionPlayer3(new Action(2, new Vector3(position, 0, 0), null, -1, null, "Move"));
                break;
        }*/
        // Création du marker
        createMarker(index, position);

        // Ajouter d'autres traitements ?
    }

    public void createMarker(int index, float position)
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
        //_markersArray[index].Add(but);
       
        // Mise à jour du prochain ID
        lastIndex[index]++;
    }
}
