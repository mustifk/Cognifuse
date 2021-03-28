using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blockScript : MonoBehaviour
{
    int state;
    Color objectColor;
    // Start is called before the first frame update
    void Start()
    {
        objectColor = this.gameObject.GetComponent<SpriteRenderer>().color;
        state = 2;
    }

    // Update is called once per frame
    void Update()
    {
        objectColor = this.gameObject.GetComponent<SpriteRenderer>().color;
        switch (state)
        {
            case 0:
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(objectColor,new Color(objectColor.r,objectColor.g,objectColor.b,0),1);  
                break;
            case 1:
                this.gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(objectColor, Color.red, 1);
                break;
        }

    }

    private void OnMouseDown()
    {
        state--;
    }
    public int CurrentState()
    {
        return state;
    }

    public void ChangeState(int NewState)
    {
        state = NewState;
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
