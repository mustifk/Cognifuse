using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EngineScript : MonoBehaviour
{
    public int Demo = 0;
    //timebar
    public GameObject TBC;
    timebarScript timebar;

    bool isGameover = false;

    int blinkCount,objCount,difficulty = new int();
    public GameObject dot;
    Vector2[] spawnPositions = new Vector2[9];
    GameObject[] dots;
    int[] queue;
    int next = 0;
    bool wrong = false;
    private int temp;

    // Start is called before the first frame update
    void Start()
    {
        //timebar
        GameObject temp = Instantiate(TBC);
        timebar = temp.GetComponent<TBCscript>().timebar();

        if (Demo == 0)
        {
            difficulty = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Difficulty();
        }
        else
        {
            difficulty = Demo;
        }

        switch (difficulty)
        {
            case 1:
                blinkCount = Random.Range(3, 5);
                timebar.SetMax(3);
                objCount = 3;
                break;
            case 2:
                timebar.SetMax(4);
                blinkCount = Random.Range(5, 7);
                objCount = 6;
                break;
            case 3:
                timebar.SetMax(6);
                blinkCount = Random.Range(7, 10);
                objCount = 7;
                break;
            default:
                break;
        }
        
        spawnPositions[0] = new Vector2(0, 2.7f); 
        spawnPositions[1] = new Vector2(2.598076f, -1.2f); 
        spawnPositions[2] = new Vector2(-2.598076f, -1.2f);
        spawnPositions[3] = new Vector2(2.598076f, 1.2f);
        spawnPositions[4] = new Vector2(0, -2.7f);
        spawnPositions[5] = new Vector2(-2.598076f, 1.2f);
        spawnPositions[6] = new Vector2(0, 0);
        queue = new int[blinkCount];
        dots = new GameObject[objCount];
        Spawner();
    }

    private void Update()
    {
        if (timebar.GetTime() == 0 && !isGameover)
        {
            StartCoroutine(EndOfMinigame(false));
            isGameover = true;
        }
    }

    public void Press(GameObject child)
    {
        if (next < blinkCount && wrong != true)
        {
            if (next < blinkCount && child != dots[queue[next]])
            {
                wrong = true;
                StartCoroutine(EndOfMinigame(false));
                next = objCount;
                PlayerAct(false);
            }
            if (next == blinkCount-1 && wrong == false)
            {
                StartCoroutine(EndOfMinigame(true));
                PlayerAct(false);
            }
            next++;
        }
    }

    IEnumerator EndOfMinigame(bool result)
    {
        Debug.Log(result);
        isGameover = true;
        timebar.Stop();
        yield return new WaitForSeconds(1);
        if (Demo == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame(10, result);
        }
    }

    void Blinker()
    {
        StartCoroutine(BlinkerFunction());
    }
    
    void Spawner()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        yield return new WaitForSeconds(0);
        for (int i = 0; i < objCount; i++)
        {
            dots[i] = Instantiate(dot, spawnPositions[i], Quaternion.identity,gameObject.transform) as GameObject;
            yield return new WaitForSeconds(0.1f - objCount * 0.01f);
        }
        StartCoroutine(Begin());
    }
    
    IEnumerator BlinkerFunction()
    {
        for (int i = 0; i < blinkCount; i++)
        {
            temp = Random.Range(0,objCount);
            queue[i] = temp;
            dots[temp].GetComponent<buttonscript>().Blink();
            yield return new WaitForSeconds(1.3f - objCount * 0.08f);
        }
        PlayerAct();
    }
    
    IEnumerator Begin()
    {
        yield return new WaitForSeconds(1f);
        Blinker();
    }

    void PlayerAct(bool active = true)
    {
        for (int i = 0; i < objCount; i++)
        {
            dots[i].GetComponent<buttonscript>().SetActive(active);
        }
        if (active)
        {
            timebar.Begin();
        }
    }
}