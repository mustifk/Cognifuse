using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class camelEngine : MonoBehaviour
{
    public AudioSource gameOverSound;
    public AudioSource trueSound;

    public int Demo = 0;
    //timebar
    public GameObject TBC;
    timebarScript timebar;

    int diffLevel, lang = 0;

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

        lang = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Language();
        
        if (Demo == 0)
        {
            diffLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Difficulty();
        }
        else
        {
            lang = 1;
            diffLevel = Demo;
        }

        switch (diffLevel)
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

        prepareGame();

        gameOver = false;
        isClick = false;

        switch (diffLevel)
        {
            case 1:
                waitingTime = 2f;
                timebar.SetMax((waitingTime - 0.2f) * maxSpinGame);
                break;
            case 2:
                waitingTime = 1.6f;
                timebar.SetMax((waitingTime - 0.2f) * maxSpinGame);
                break;
            case 3:
                waitingTime = 1.2f;
                timebar.SetMax((waitingTime - 0.2f) * maxSpinGame);
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
        if ((Random.Range(0, 10) % 2) == 0)
        {
            if (lang == 0)
            {
                messages[0] = "Deve";
            }
            else
            {
                messages[0] = "Camel";
            }
        }
        else
        {
            if (lang == 0)
            {
                messages[0] = "Cuce";
            }
            else
            {
                messages[0] = "Dwarf";
            }
        }

        for (int i = 1; i < messages.Length - 3; i++)
        {
            if (lang == 0)
            {
                if(messages[i-1].Equals("Deve"))
                {
                    messages[i] = "Cuce";
                    messages[++i] = "Deve";
                }
                else
                {
                    messages[i] = "Deve";
                    messages[++i] = "Cuce";
                }

            }
            else
            {
                if (messages[i - 1].Equals("Camel"))
                {
                    messages[i] = "Dwarf";
                    messages[++i] = "Camel";
                }
                else
                {
                    messages[i] = "Camel";
                    messages[++i] = "Dwarf";
                }
            }

            if (Random.Range(0, 10) % 2 == 0)
            {
                if (lang == 0)
                {
                    messages[i] = "Deve";
                }
                else
                {
                    messages[i] = "Camel";
                }
            }
            else
            {
                if (lang == 0)
                {
                    messages[i] = "Cuce";
                }
                else
                {
                    messages[i] = "Dwarf";
                }
            }
        }
        index = Random.Range(0, messages.Length / 2);
    }

    void prepareGame()
    {
        updateText();
        cdObject = Instantiate(cdObject);
        cdObject.transform.parent = gameObject.transform;
        if (text.text == "Camel" || text.text == "Deve")
        {
            cdObject.setStartSituation(0, lang);
        }
        else
        {
            cdObject.setStartSituation(1, lang);
        }
    }

    void updateText()
    {
        text.text = messages[index++];
    }

    void checkGame()
    {
        if (text.text != cdObject.situation)
        {
            if (isClick)
            {
                countGame++;
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
            Finish(false);
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
        if (!gameOver)
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
        if (win)
        {
            trueSound.Play();
        }
        if (!win)
        {
            gameOverSound.Play();
        }
        timebar.Stop();
        Destroy(text);
        int childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
            GameObject.Destroy(gameObject.transform.GetChild(i).gameObject);
        Destroy(mini.gameObject);
        gameOver = true;
        StartCoroutine(End(win));
    }

    IEnumerator End(bool win)
    {
        yield return new WaitForSeconds(0.8f);
        if (Demo == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame((timebar.GetTime() / timebar.GetMax()), win);
            //GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame(1, win);
        }
    }
}
