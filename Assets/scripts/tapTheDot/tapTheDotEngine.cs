using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tapTheDotEngine : MonoBehaviour
{
    //timebar
    public GameObject TBC;
    timebarScript timebar;

    int blinkCount, difficulty = new int(); //blink free - diff max 3 
    public GameObject dot,dot2;
    private float lifespan;
    private bool gameOver = false;
    GameObject[] dots;
    // Start is called before the first frame update
    void Start()
    {

        //timebar
        GameObject temp = Instantiate(TBC);
        timebar = temp.GetComponent<TBCscript>().timebar();

        difficulty = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Difficulty();
        
        switch (difficulty)
        {
            case 1:
                blinkCount = Random.Range(4,7);
                break;
            case 2:
                blinkCount = Random.Range(7,10);
                break;
            case 3:
                blinkCount = Random.Range(10,14);
                break;
        }
        lifespan = 1.2f - (blinkCount * 0.035f);
        timebar.SetMax(2.3f + (lifespan * blinkCount));

        dots = new GameObject[3];
        StartCoroutine(Begin());
        timebar.Begin();
    }

    // Update is called once per frame
    void Update()
    {
        if (timebar.GetTime() == 0 && !gameOver)
        {
            timebar.Stop();
        }
    }

    public void Press(bool wrong)
    {
        if (wrong) // puan ver
        {
            StartCoroutine(EndOfMinigame(false));
            gameOver = true;
        }
        for (int i = 0; i < dots.Length; i++)
        {
            Destroy(dots[i]);
        }
    }

    IEnumerator Begin()
    {
        int temp;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < blinkCount && !gameOver; i++)
        {
            temp = Random.Range(0, difficulty);
            switch (temp)
            {
                case 1:
                    Twice();
                    break;
                case 2:
                    Thrice();
                    break;
                default:
                    Once();
                    break;
            }
            yield return new WaitForSeconds(lifespan + 0.1f);
        }
        if (!gameOver)
        {
            StartCoroutine(EndOfMinigame(true));
            gameOver = true;
        }
    }

    void Once()
    {
        dots[0] = Instantiate(dot, new Vector2(Random.Range(-7f,7.0001f), Random.Range(-3.5f, 3.50001f)), Quaternion.identity, gameObject.transform) as GameObject;
        dots[0].GetComponent<tapDotScript>().Exist(lifespan);
        if (Random.Range(0,3) > 1)
        {
            dots[0].GetComponent<SpriteRenderer>().material.color = new Color(0f,52f,0f);
            dots[0].GetComponent<tapDotScript>().Wrong();
        }
    }

    void Twice()
    {
        //2 obje - yy - dy
        Vector2 temp = new Vector2(Random.Range(-7f, 7.0001f), Random.Range(-3.5f, 3.50001f));
        for (int i = 0; i < 2; i++)
        {
            dots[i] = Instantiate(dot, temp, Quaternion.identity, gameObject.transform) as GameObject;
            dots[i].GetComponent<tapDotScript>().Exist(lifespan);
            if (i == 0)
            {
                dots[i].GetComponent<SpriteRenderer>().material.color = new Color(0f, 52f, 0f);
                dots[i].GetComponent<tapDotScript>().Wrong();
            }
            else
            {
                if (Random.Range(0, 4) > 2)
                {
                    dots[i].GetComponent<SpriteRenderer>().material.color = new Color(0f, 52f, 0f);
                    dots[i].GetComponent<tapDotScript>().Wrong();
                }
            }
            temp = NewPosition(temp);
        }
    }

    void Thrice()
    {
        //3 obje - yyy - yyd 
        Vector2 temp = new Vector2(Random.Range(-7f, 7.0001f), Random.Range(-3.5f, 3.50001f));
        for (int i = 0; i < 3; i++)
        {
            dots[i] = Instantiate(dot, temp, Quaternion.identity, gameObject.transform) as GameObject;
            dots[i].GetComponent<tapDotScript>().Exist(lifespan);
            
            if (i > 1)
            {
                if (Random.Range(0, 6) > 4)
                {
                    dots[i].GetComponent<SpriteRenderer>().material.color = new Color(0f, 52f, 0f);
                    dots[i].GetComponent<tapDotScript>().Wrong();
                }
            }
            else
            {
                if (Random.Range(0,4) == 1)
                {
                    dots[i].GetComponent<SpriteRenderer>().material.color = new Color(0f, 52f, 0f);
                    dots[i].GetComponent<tapDotScript>().Wrong();
                }
                else
                {
                    Destroy(dots[i]);
                    dots[i] = Instantiate(dot2, temp, Quaternion.identity, gameObject.transform) as GameObject;
                    dots[i].GetComponent<dontTapDot>().Exist(lifespan);
                }
            }
            if (i == 0)
            {
                temp = NewPosition(temp);
            }
            else
            {
                temp = NewPosition(temp, dots[i].transform.position);
            }
        }
    }
    Vector2 NewPosition(Vector2 temp)
    {
        Vector2 position = new Vector2(Random.Range(-7f, 7.0001f), Random.Range(-3.5f, 3.50001f));
        float x = temp.x, y = temp.y;
        while (Mathf.Abs(position.x - x) < 1.7 && Mathf.Abs(position.y - y) < 1.7)
        {
            position = new Vector2(Random.Range(-7f, 7.0001f), Random.Range(-3.5f, 3.50001f));
        }
        return position;
    }
    Vector2 NewPosition(Vector2 temp, Vector2 temp2)
    {
        Vector2 position = new Vector2(Random.Range(-7f, 7.0001f), Random.Range(-3.5f, 3.50001f));
        float x = temp.x, y = temp.y, x2 = temp2.x, y2 = temp2.y;
        while (Mathf.Abs(position.x - x) < 2 && Mathf.Abs(position.y - y) < 2 && Mathf.Abs(position.x - x2) < 2 && Mathf.Abs(position.y - y2) < 2)
        {
            position = new Vector2(Random.Range(-7f, 7.0001f), Random.Range(-3.5f, 3.50001f));
        }
        return position;
    }

    IEnumerator EndOfMinigame(bool result)
    {
        timebar.Stop();
        yield return new WaitForSeconds(1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame(10, result);
    }

}
