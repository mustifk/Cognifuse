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
        messages = new string[]{ "Camel", "Dwarf", "Camel", "Dwarf", "Camel", "Dwarf", "Camel", "Dwarf"};

        prepareGame();

        gameOver = false;
        isClick = false;
      
        switch(diffLevel)
        {
            case 1:
                waitingTime = 3f;
                break;
            case 2:
                waitingTime = 2.5f;
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

    void prepareGame()
    {
        updateText();
        cdObject = Instantiate(cdObject);
        cdObject.transform.parent = gameObject.transform;
    }

    void updateText()
    {
        //int index = Random.Range(0, messages.Length);
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
            if (countGame == (diffLevel * 3))
            {
                Finish();
            }
            else if ((currentPos - startPos) > waitingTime)
            {
                checkGame();
                //updateText();
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
        }
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
