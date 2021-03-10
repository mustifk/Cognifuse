using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonscript : MonoBehaviour
{
    SpriteRenderer sprt;
    Color colortemp;
    bool blinking = false,touching = false;
    // Start is called before the first frame update
    void Start()
    {
        sprt = GetComponent<SpriteRenderer>();
        colortemp = sprt.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (blinking)
        {
            sprt.material.color = Color.white + colortemp;
        }
        else if (touching)
        {
            sprt.material.color = Color.white + colortemp + colortemp;
        }
        else
        {
            sprt.material.color = Color.Lerp(Color.white,colortemp,1f);
        }
        
    }
    
    void OnMouseDown()
    {
        Touch();
        this.GetComponentInParent<EngineScript>().Press(this.gameObject);
    }

    public void Blink()
    {
        blinking = true;
        //sprt.color = Color.Lerp(colortemp, Color.white, 0.6f);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.4f);
        //sprt.color = Color.Lerp(Color.white, colortemp, 1f);
        blinking = false;    
    }

    void Touch()
    {
        touching = true;
        //sprt.color = Color.Lerp(colortemp, Color.white, 0.6f);
        StartCoroutine(TouchWait());
    }

    IEnumerator TouchWait()
    {
        yield return new WaitForSeconds(0.15f);
        //sprt.color = Color.Lerp(Color.white, colortemp, 1f);
        touching = false;
    }
}
