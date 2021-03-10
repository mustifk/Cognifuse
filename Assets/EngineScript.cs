using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EngineScript : MonoBehaviour
{
    public GameObject[] dots = new GameObject[4];
    Stack<int> n = new Stack<int>();
    int[] queue = new int[4];
    int temp = 0,next = 0;
    bool wrong = false;
    // Start is called before the first frame update
    void Start()
    {
        int temporary,check; 
        for (int i = 0; i < 4; i++)
        {
            check = 0;
            while (check == 0)
            {
                temporary = Random.Range(0, 4);
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
        if (next < 4 || wrong != true)
        {
            if (next < 4 && child != dots[queue[next]])
            {
                wrong = true;
                Debug.Log("You lose!");
                next = 4;
            }
            if (next == 3 && wrong == false)
            {
                Debug.Log("WON THE GAME!!!");
            }
            next++;
        }
    }

    void Blinker()
    {
        StartCoroutine(ExampleCoroutine());
    }

    IEnumerator ExampleCoroutine()
    {
        for (int i = 0; i < 4; i++)
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
}
