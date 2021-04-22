using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class camelEngine : MonoBehaviour
{
    [SerializeField]
    int diffLevel;

    [SerializeField]
    CDobject cdObject;

    [SerializeField]
    TextMeshProUGUI text;

    [SerializeField]
    Tbcmini mini;

    minibarScript miniTime;

    string[] messages;

    bool gameOver, isClick;

    float waitingTime;

    int countGame;

    float startPos, currentPos;

    int index = 0;

    void Start()
    {
      //  messages = new string[]{ "Camel", "Dwarf", "Camel", "Dwarf", "Camel", "Dwarf", "Camel", "Dwarf"};

        setMessage();

       // updateText();
       
        prepareGame();

        gameOver = false;
        isClick = false;
      
        switch(diffLevel)
        {
            case 1:
                waitingTime = 2.5f;
                break;
            case 2:
                waitingTime = 2f;
                break;
            case 3:
                waitingTime = 1.5f;
                break;
        }
        countGame = 0;

        miniTime = mini.GetComponent<Tbcmini>().timebar();
        miniTime.SetMax(waitingTime);

        miniTime.Begin();

        setTimer();
    }

    void setMessage()
    {
        messages = new string[40];
        if(Random.Range(0,2) == 0)
            messages[0] = "Camel";
        else
            messages[0] = "Dwarf";

        for(int i=1;i<messages.Length-3;i++)
        {
            messages[i] = "Camel";
            messages[++i] = "Dwarf";
            int ind = Random.Range(0, 9);
            if(ind % 2 == 0)
                messages[++i] = "Camel";
            else
                messages[++i] = "Dwarf";
        }
        index = Random.Range(0, messages.Length/2);
    }

    void prepareGame()
    {
        updateText();
        cdObject = Instantiate(cdObject);
        cdObject.transform.parent = gameObject.transform;
        if (text.text == "Camel")
            cdObject.setStartSituation(0);
        else
            cdObject.setStartSituation(1);
    }

    void updateText()
    {
        //int index = Random.Range(0, 10);
        text.text = messages[index++];
    }

    void checkGame()
    {
        if (text.text != cdObject.situation)
        {
            if (isClick)      
            {
                Debug.Log("TRUE");
                countGame++;
               // StartCoroutine(wait());
                updateText();
                setTimer();
                cdObject.changePos();
            }
            else
            {
                Debug.Log("GAME OVER");
                gameOver = true;
                Finish();
            }
        }
        else if (text.text == cdObject.situation)
        {
            if (isClick)
            {
                Debug.Log("GAME OVER");
                gameOver = true;
                Finish();
            }
            else
            {
                Debug.Log("TRUE");
              //  StartCoroutine(wait());
                updateText();
                setTimer();
            }
        }
        isClick = false;
    }

    void Update()
    {
        currentPos = Time.time;
        if (!gameOver)
        {
            if (countGame > (diffLevel * 4) - 1)
            {
                Finish();
            }
            else if ((currentPos - startPos) > waitingTime)
            {
                checkGame();
            }
        }
    }

    void setTimer()
    {
        startPos = Time.time;
        miniTime.SetMax(waitingTime);
        miniTime.Begin();
    }

    public void checkPos()
    {
        if(!gameOver)
        {
            isClick = true;
            checkGame();
            StartCoroutine(wait());
        }
    }

    IEnumerator wait()
    {
        miniTime.Stop();
        text.text = "";
        yield return new WaitForSeconds(0.5f);
        updateText();
        setTimer();
    }

    void Finish()
    {
        Destroy(text);
        int childs = transform.childCount;
        for(int i=childs-1;i>=0;i--) 
            GameObject.Destroy(gameObject.transform.GetChild(i).gameObject);
        Destroy(mini.gameObject);
        gameOver = true;
    }
}
