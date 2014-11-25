using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {


    public GameObject[] _charactersArray;
    public GameObject[] getCharactersArray()
    {
        return _charactersArray;
    }
    public GameObject getCharactersByIndex( int index)
    {
        return _charactersArray[index];
    }
    
    public Material getCharactersMaterialByIndex(int index)
    {
        return _charactersArray[index].renderer.material;
    }
    public void setMainColorByIndex(int index,  Color color)
    {
        _charactersArray[index].renderer.material.color = color;
    }

}
