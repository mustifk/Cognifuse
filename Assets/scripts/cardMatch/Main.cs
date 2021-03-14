using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [SerializeField] private GameController controller;
    [SerializeField] private GameObject card_back;

    private int id;

    public int getId
    {
        get { return id; }
    }

    public void ChangeSprite(int id, Sprite image)
    {
        this.id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }

    public void OnMouseDown()
    {
        if(card_back.activeSelf && controller.canReveal)
        {
            card_back.SetActive(false);
            controller.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        card_back.SetActive(true);
    }
}
