using UnityEngine;
using System.Collections;

public class ActionDatas : MonoBehaviour {
    [SerializeField]
    private GameObject _parentObject;
    [SerializeField]
    private Transform _parentTransform;
    [SerializeField]
    private GameObject[] _datasObjects;
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
