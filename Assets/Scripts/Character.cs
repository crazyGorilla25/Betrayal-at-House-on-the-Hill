using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] int id = -1;
    [SerializeField] string characterName = "";
    [SerializeField] int[] mightArray = new int[8];
    [SerializeField] int[] speedArray = new int[8];
    [SerializeField] int[] knowledgeArray = new int[8];
    [SerializeField] int[] sanityArray = new int[8];
    [SerializeField] Date birthday = new Date();
    [SerializeField] int age = 0;

    public int GetId()
	{
        return id;
	}
}
