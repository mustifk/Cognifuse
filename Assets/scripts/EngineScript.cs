using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EngineScript : MonoBehaviour
{
    public int objCount = 4;
    public GameObject dot;
    GameObject[] dots;
    Stack<int> n = new Stack<int>();
    int[] queue;
    int temp = 0,next = 0;
    bool wrong = false;
    // Start is called before the first frame update
    void Start()
    {
        queue = new int[objCount];
        dots = new GameObject[objCount];
        int temporary,check;
        
        Spawner();

        for (int i = 0; i < objCount; i++)
        {
            check = 0;
            while (check == 0)
            {
                temporary = Random.Range(0, objCount);
                if (n.Contains(temporary) != true)
                {
                    n.Push(temporary);
                    check++;
                }
            }
        }
        StartCoroutine(Begin());
    }

    public void Press(GameObject child)
    {
        if (next < objCount || wrong != true)
        {
            if (next < objCount && child != dots[queue[next]])
            {
                wrong = true;
                Debug.Log("You lose!");
                next = objCount;
            }
            if (next == objCount - 1 && wrong == false)
            {
                Debug.Log("WON THE GAME!!!");
            }
            next++;
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

    IEnumerator Spawn() // ÇARPIŞMALAARI ENGELLEYEMEDİN - TUTARLI SPAWN YAPMAYI DENE - BLİNK EYLEMİNDE SORUN VAR 
    {
        for (int i = 0; i < objCount; i++)
        {
            dots[i] = Instantiate(dot) as GameObject;
            dots[i].transform.position = new Vector2(Random.Range(2, 5.2f) * RandSign() , Random.Range(1, 2.3f) * RandSign());
            while (dots[i].GetComponent<buttonscript>().isTouching())
            {
                Destroy(dots[i]);
                dots[i] = Instantiate(dot) as GameObject;
                dots[i].transform.position = new Vector2(Random.Range(2, 5.2f) * RandSign(), Random.Range(1, 2.3f) * RandSign());
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator BlinkerFunction()
    {
        for (int i = 0; i < objCount; i++)
        {
            temp = n.Pop();
            queue[i] = temp;
            dots[temp].GetComponent<buttonscript>().Blink();
            yield return new WaitForSeconds(1.3f);
        }
    }

    IEnumerator Begin()
    {
        yield return new WaitForSeconds(3f);
        Blinker();
    }

    int RandSign()
    {
        return Random.Range(0, 1f) > 0.5f ? -1 : 1;
    }
}