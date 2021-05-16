using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CDobject : MonoBehaviour
{
    public AudioSource up, down;
    [SerializeField]
    Sprite camel;

    [SerializeField]
    Sprite dwarf;

    int bit;
    int lang;

    public string situation;

    void Start()
    {
        lang = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Language();
        // GetComponent<SpriteRenderer>().sprite = camel;
        //setStartSituation();
    }
    public void setStartSituation(int value)
    {
        if (value == 0)
        {
            bit = 0;
            if (lang == 0)
            {
                situation = "Deve";
            }
            else
            {
                situation = "Camel";
            }
            GetComponent<SpriteRenderer>().sprite = camel;
        }
        else
        {
            bit = 1;
            if (lang == 0)
            {
                situation = "Cuce";
            }
            else
            {
                situation = "Dwarf";
            }
            GetComponent<SpriteRenderer>().sprite = dwarf;
        }
    }

    public void changePos()
    {
        if (bit == 0)
        {
            down.Play();
            GetComponent<SpriteRenderer>().sprite = dwarf;
            bit = 1;
            if (lang == 0)
            {
                situation = "Cuce";
            }
            else
            {
                situation = "Dwarf";
            }
            transform.localScale = new Vector2(1.8f, 1.8f);
        }
        else
        {
            up.Play();
            GetComponent<SpriteRenderer>().sprite = camel;
            bit = 0;
            if (lang == 0)
            {
                situation = "Deve";
            }
            else
            {
                situation = "Camel";
            }
            transform.localScale = new Vector2(2f, 2f);
        }
    }
}
