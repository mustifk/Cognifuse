using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class camelEngine : MonoBehaviour
{
    public int Demo = 0;
    //timebar
    public GameObject TBC;
    timebarScript timebar;

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

    int maxSpinGame;

    void Start()
    {
        //timebar
        GameObject temp = Instantiate(TBC);
        timebar = temp.GetComponent<TBCscript>().timebar();

        //  messages = new string[]{ "Camel", "Dwarf", "Camel", "Dwarf", "Camel", "Dwarf", "Camel", "Dwarf"};

        if (Demo == 0)
        {
            diffLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Difficulty();
        }
        else
        {
            diffLevel = Demo;
        }

        switch(diffLevel)
        {
            case 1:
                maxSpinGame = 8;
                break;
            case 2:
                maxSpinGame = 12;
                break;
            case 3:
                maxSpinGame = 16;
                break;
        }

        setMessage();

       // updateText();
       
        prepareGame();

        gameOver = false;
        isClick = false;
      
        switch(diffLevel)
        {
            case 1:
                waitingTime = 2f;
                timebar.SetMax(10);
                break;
            case 2:
                waitingTime = 1.5f;
                timebar.SetMax(12);
                break;
            case 3:
                waitingTime = 1f;
                timebar.SetMax(15);
                break;
        }
        countGame = 1;

        miniTime = mini.GetComponent<Tbcmini>().timebar();
        miniTime.SetMax(waitingTime);

        miniTime.Begin();

        setTimer();
        timebar.Begin();
    }

    void setMessage()
    {
        messages = new string[60];
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
                countGame++;
               // StartCoroutine(wait());
                updateText();
                setTimer();
                cdObject.changePos();
            }
            else
            {
                gameOver = true;
                Finish(false);
            }
        }
        else if (text.text == cdObject.situation)
        {
            if (isClick)
            {
                gameOver = true;
                Finish(false);
            }
            else
            {
                countGame++;
                updateText();
                setTimer();
            }
        }
        isClick = false;
    }

    void Update()
    {
        if (timebar.GetTime() == 0 && !gameOver)
        {
            timebar.Stop();
        }
        currentPos = Time.time;
        if (!gameOver)
        {
            if (countGame > maxSpinGame)
            {
                Finish(true);
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
        yield return new WaitForSeconds((6 - diffLevel) / 10);
        updateText();
        setTimer();
    }

    void Finish(bool win)
    {
        timebar.Stop();
        Destroy(text);
        int childs = transform.childCount;
        for(int i=childs-1;i>=0;i--) 
            GameObject.Destroy(gameObject.transform.GetChild(i).gameObject);
        Destroy(mini.gameObject);
        gameOver = true;
        StartCoroutine(End(win));
    }

    IEnumerator End(bool win)
    {
        yield return new WaitForSeconds(0.8f);
        if(Demo == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame(1, win);
            //GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame((timebar.GetTime() / timebar.GetMax()), win);
        }
    }
}
