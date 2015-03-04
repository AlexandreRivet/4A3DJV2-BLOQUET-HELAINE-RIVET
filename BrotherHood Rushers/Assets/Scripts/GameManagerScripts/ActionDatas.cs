using UnityEngine;
using System.Collections;

public class ActionDatas : MonoBehaviour {
    [SerializeField]
    private GameObject _parentObject;
    [SerializeField]
    private Transform _parentTransform;

    //Utiliser si besoin d'info sur des gameObjects pour les action actives sur cet objet
    [SerializeField]
    private GameObject[] _datasObjects;

    //Utiliser si besoin d'info supplémentaire. Ce tableau correspond a des string qui permette de récupérer des valeurs du tableau _datasValues
    // On s'en sert donner des informations comme la distance minimale entre le joueur et l'objet pour faire l'action
    [SerializeField]
    private string[] _datasLabels;
    [SerializeField]
    private float[] _datasValues;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    //Getters
    public GameObject getParentObject()
    {
        return _parentObject;
    }
    public Transform getParentTranform()
    {
        return _parentTransform;
    }
    public GameObject[] getDatasObject()
    {
        return _datasObjects;
    }
    public GameObject getDatasObjectById(int id)
    {
        return _datasObjects[id];
    }
    public string[] getDatasLabels()
    {
        return _datasLabels;
    }
    public float[] getDatasValuesByLabel()
    {
        return _datasValues;
    }
    public float getDatasValuesByLabel(string label)
    {
        for (int i = 0; i < _datasLabels.Length; i++ )
        {
            if (_datasLabels[i].Equals(label))
                return _datasValues[i];
        }
            return -1.0f;
    }
}
