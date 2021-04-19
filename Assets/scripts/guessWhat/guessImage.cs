using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guessImage : MonoBehaviour
{

    private guessWhat controller;

    private int id;
    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<guessWhat>();
    }
    
    public void ChangeImage(int id, Sprite image)
    {
        this.id = id;
        GetComponent<Image>().sprite = image;

    }
    public void ChangeSprite(int id, Sprite image)
    {
        this.id = id;
        GetComponent<SpriteRenderer>().sprite = image;

    }
    private void OnMouseDown()
    {
        controller.check(this.id);

    }
    
    public void defuse()
    {
        this.GetComponent<BoxCollider2D>().enabled = false;
    }
}
