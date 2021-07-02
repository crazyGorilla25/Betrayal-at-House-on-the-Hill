using System;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] Character[] allCharacters = new Character[12];
    [SerializeField] int myCharacterId;
    Character myCharacter;
    Map mapInstance;

    public Character GetCharacter()
    {
        foreach (Character character in allCharacters)
        {
            if (character.GetId() == myCharacterId)
            {
                return character;
            }
        }

        return null;
    }
    public int GetCharacterId()
    {
        return myCharacterId;
    }
}