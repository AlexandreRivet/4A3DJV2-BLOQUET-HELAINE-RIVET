using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {

    //Manager qui contient la liste des objets de ma scènes
    [SerializeField]
    private GameObject[] _charactersArray;
    [SerializeField]
    private GameObject[] _charactersArrayPosition;
    [SerializeField]
    private GameObject[] _objectsLevel;
 
    public GameObject[] getCharactersArray()
    {
        return _charactersArray;
    }
    public GameObject[] getCharactersPositionsArray()
    {
        return _charactersArrayPosition;
    }
    public GameObject getCharactersByIndex( int index)
    {
        return _charactersArray[index];
    }
    public GameObject getCharactersPositionByIndex(int index)
    {
        return _charactersArrayPosition[index];
    }
    public Material getCharactersMaterialByIndex(int index)
    {
        return _charactersArray[index].renderer.material;
    }
    public void setMainColorByIndex(int index,  Color color)
    {
        _charactersArray[index].renderer.material.color = color;
    }
    public GameObject getObjectLevelById(int id)
    {
        return _objectsLevel[id];
    }
    public GameObject[] getObjectLevelArrayById(int[] id)
    {
        GameObject[] objectLevel = new GameObject[id.Length];
        for (int i = 0; i < id.Length; i++)
            objectLevel[i] = _objectsLevel[id[i]];
        return objectLevel;
    }
}
