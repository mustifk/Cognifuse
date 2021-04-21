﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDobject : MonoBehaviour
{
    [SerializeField]
    Sprite camel;

    [SerializeField]
    Sprite dwarf;

    int bit;

    public string situation;

    void Start()
    {
       // GetComponent<SpriteRenderer>().sprite = camel;
        bit = 0;
        situation = "Camel";
    }

    public void changePos()
    {
        if(bit == 0)
        {
            GetComponent<SpriteRenderer>().sprite = dwarf;
            bit = 1;
            situation = "Dwarf";
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = camel;
            bit = 0;
            situation = "Camel";
        }
    }
}
