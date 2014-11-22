using UnityEngine;
using System.Collections;

public class CharacterManager : MonoBehaviour {


    public GameObject[] _charactersArray;
    public Material[] _charactersColorArray;

    public GameObject[] getCharactersArray()
    {
        return _charactersArray;
    }
    public GameObject getCharactersByIndex( int index)
    {
        return _charactersArray[index];
    }
    public Material[] getCharactersMaterialArray()
    {
        return _charactersColorArray;
    }
    public Material getCharactersMaterialByIndex(int index)
    {
        return _charactersColorArray[index];
    }
    public void setMainColorByIndex(int index,  Color color)
    {
        _charactersColorArray[index].color = color;
    }

}
