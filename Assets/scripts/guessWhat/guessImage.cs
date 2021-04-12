using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guessImage : MonoBehaviour
{
    private int id;
    public void ChangeSprite(int id, Sprite image)
    {
        this.id = id;
        GetComponent<SpriteRenderer>().sprite = image;

    }
}
