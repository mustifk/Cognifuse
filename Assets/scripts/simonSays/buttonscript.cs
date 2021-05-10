using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonscript : MonoBehaviour
{
    static int colorCount;
    public AudioSource doo, re, mi, fa, sol, la, si;
    AudioSource original;
    SpriteRenderer sprt;
    Color colortemp;
    bool blinking = false,clicked = false,colliding = false;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
        switch (colorCount)
        {
            default:
                original = doo;
                this.GetComponent<SpriteRenderer>().color = new Color(Color.red.r / 1.5f, Color.red.g / 1.5f, Color.red.b / 1.5f);
                break;
            case 1:
                original = re;
                this.GetComponent<SpriteRenderer>().color = new Color(Color.green.r / 1.5f, Color.green.g / 1.5f, Color.green.b / 1.5f);
                break;
            case 2:
                original = mi;
                this.GetComponent<SpriteRenderer>().color = new Color((Color.blue.r - 0.05f) / 1.4f, (Color.blue.g + 0.05f) / 1.4f, (Color.blue.b - 0.2f) / 1.4f);
                break;
            case 3:
                original = fa;
                this.GetComponent<SpriteRenderer>().color = new Color(Color.magenta.r / 1.5f, Color.magenta.g / 1.5f, Color.magenta.b / 1.5f);
                break;
            case 4:
                original = sol;
                this.GetComponent<SpriteRenderer>().color = new Color(Color.cyan.r / 1.5f, Color.cyan.g / 1.5f, Color.cyan.b / 1.5f);
                break;
            case 5:
                original = la;
                this.GetComponent<SpriteRenderer>().color = new Color(Color.yellow.r / 1.5f, Color.yellow.g / 1.5f, Color.yellow.b / 1.5f);
                break;
            case 6:
                original = si;
                this.GetComponent<SpriteRenderer>().color = new Color(Color.gray.r / 1.5f, Color.gray.g / 1.5f, Color.gray.b / 1.5f);
                break;
        }
        colorCount++;
        if (colorCount > 6)
        {
            colorCount = 0;
        }
        sprt = GetComponent<SpriteRenderer>();
        colortemp = sprt.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (blinking)
        {
            sprt.material.color = Color.white + colortemp + Color.white;
        }
        else if (clicked)
        {
            sprt.material.color = Color.white + colortemp + colortemp;
        }
        else
        {
            sprt.material.color = Color.Lerp(Color.white,colortemp,1f);
        }
        colliding = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        colliding = true;
    }

    void OnMouseDown()
    {
        Touch();
        this.GetComponentInParent<EngineScript>().Press(this.gameObject);
    }

    public void Blink()
    {
        original.Play();
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
        original.Play();
        clicked = true;
        //sprt.color = Color.Lerp(colortemp, Color.white, 0.6f);
        StartCoroutine(TouchWait());
    }

    IEnumerator TouchWait()
    {
        yield return new WaitForSeconds(0.15f);
        //sprt.color = Color.Lerp(Color.white, colortemp, 1f);
        clicked = false;
    }

    public bool isTouching()
    {
        return colliding;
    }

    public void SetActive(bool active = true)
    {
        gameObject.GetComponent<CircleCollider2D>().enabled = active;
    }
}
