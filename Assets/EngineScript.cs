using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineScript : MonoBehaviour
{
    public GameObject[] dot = new GameObject[10];
    public Stack<int> n = new Stack<int>();
    int[] queue = new int[4];
    int queueNumber = 0;
    bool wrong = false;
    int next = 0;
    // Start is called before the first frame update
    void Start()
    {
        int temp,check; 
        for (int i = 0; i < 4; i++)
        {
            check = 0;
            while (check == 0)
            {
                temp = Random.Range(0, 4);
                if (n.Contains(temp) != true)
                {
                    n.Push(temp);
                    check++;
                }
            }
        }
        Blinker();
        
    }

    public void Press(GameObject child)
    {
        if (child != dot[queue[next]])
        {
            wrong = true;
        }
        next++;

        if (next == 4 && wrong == false)
        {
            Debug.Log("WON THE GAME!!!");
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
            int temp;
            temp = n.Pop();
            queue[queueNumber++] = temp;
            dot[temp].GetComponent<buttonscript>().Blink();
            yield return new WaitForSeconds(1.3f);
        }
    }

}
