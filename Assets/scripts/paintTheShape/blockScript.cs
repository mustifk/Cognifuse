using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockScript : MonoBehaviour
{
    public Sprite blockTransparent;
    int state;
    Color objectColor;
    // Start is called before the first frame update
    void Start()
    {
        objectColor = this.gameObject.GetComponent<SpriteRenderer>().color;
        state = 1;
    }

    // Update is called once per frame
    void Update()
    {
        objectColor = this.gameObject.GetComponent<SpriteRenderer>().color;
        switch (state)
        {
            case 0:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = blockTransparent;  
                break;
            //case 1:
                //this.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(objectColor, Color.red, 1);
                //break;
        }

    }

    private void OnMouseDown()
    {
        state--;
        this.transform.parent.GetComponent<paintTheShapeScript>().Press();
        if (state == 0)
        {
            ColliderOff();
        }
    }
    public int CurrentState()
    {
        return state;
    }

    public void ChangeState(int NewState)
    {
        state = NewState;
        switch (state)
        {
            case 0:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = blockTransparent;
                //this.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(objectColor, new Color(objectColor.r, objectColor.g, objectColor.b, 0), 1);
                break;
           /* case 1:
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(objectColor, Color.red, 1);
                break;*/
        }
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void ColliderOff()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
