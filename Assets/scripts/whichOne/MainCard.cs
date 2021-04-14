using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MainCard : MonoBehaviour
{
    private int id;
    private whichOne controller;

    private void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<whichOne>();
    }
    private void OnMouseDown()
    {
        if (controller.isAgain)
        {
            controller.check(id);
        }
       
    }
    public void isDestroy(bool x)
    {
        if (!x)
        {
            StartCoroutine(destroy());
        }
    }
    public int getId
    {
        get { return id; }
    }

    public void ChangeSprite(int id, Sprite image)
    {
        this.id = id;
        GetComponent<SpriteRenderer>().sprite = image;

    }
    IEnumerator destroy()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
