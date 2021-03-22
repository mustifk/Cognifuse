using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EngineScript : MonoBehaviour
{
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
        difficulty = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Difficulty();
        switch (difficulty)
        {
            case 1:
                blinkCount = Random.Range(3, 5);
                objCount = 3;
                break;
            case 2:
                blinkCount = Random.Range(5, 7);
                objCount = 6;
                break;
            case 3:
                blinkCount = Random.Range(7, 10);
                objCount = 7;
                break;
            default:
                break;
        }

        spawnPositions[0] = new Vector2(0, 3); 
        spawnPositions[1] = new Vector2(2.598076f, -1.5f); 
        spawnPositions[2] = new Vector2(-2.598076f, -1.5f);
        spawnPositions[3] = new Vector2(2.598076f, 1.5f);
        spawnPositions[4] = new Vector2(0, -3);
        spawnPositions[5] = new Vector2(-2.598076f, 1.5f);
        queue = new int[blinkCount];
        dots = new GameObject[objCount];
        Spawner();
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
        yield return new WaitForSeconds(1);
        GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame(10, result);
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
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < objCount; i++)
        {
            dots[i] = Instantiate(dot, spawnPositions[i], Quaternion.identity,gameObject.transform) as GameObject;
            yield return new WaitForSeconds(0.3f - objCount * 0.03f);
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
    }
}