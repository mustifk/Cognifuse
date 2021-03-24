using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class images : MonoBehaviour
{
    private int id;
    public int getId
    {
        get { return id; }
    }

    public void isDestroy(bool x)
    {
        if (x)
        {
            StartCoroutine(destroy());

        }

    }
    public void ChangeSprite(int id, Sprite image)
    {
        this.id = id;
        GetComponent<SpriteRenderer>().sprite = image;

    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(1.5f);

        Destroy(this.gameObject);

    }
}
