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

    string[] messages;

    bool gameOver, isClick;

    int waitingTime;

    int countGame;

    float period = 0.0f;

    void Start()
    {
        messages = new string[]{ "Camel", "Dwarf", "Camel", "Dwarf", "Camel", "Dwarf"};

        prepareGame();

        gameOver = false;
        isClick = false;
      
        switch(diffLevel)
        {
            case 1:
                waitingTime = 3;
                break;
            case 2:
                waitingTime = 2;
                break;
            case 3:
                waitingTime = 1;
                break;
        }
        countGame = 0;

        //timer sıfırla
    }

    void prepareGame()
    {
        updateText();
        cdObject = Instantiate(cdObject);
        cdObject.transform.parent = gameObject.transform;
    }

    void updateText()
    {
        int index = Random.Range(0, messages.Length);
        text.text = messages[index];
        Debug.Log("+");
    }

    void checkGame()
    {
        if (text.text == cdObject.situation)
        {
            if (isClick)      //true
            {
                Debug.Log("TRUE");
                countGame++;
                updateText();
                cdObject.changePos();
            }
            else
            {
                Debug.Log("GAME OVER");
                gameOver = true;
                Finish();
            }
        }
        else if (text.text != cdObject.situation)
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
                countGame++;
                updateText();
                cdObject.changePos();
            }
        }
        isClick = false;
    }

    void Update()
    {
        if(!gameOver)
        {
            if (period > (waitingTime))
            {
                updateText();

                period -= waitingTime;
            }
            period += UnityEngine.Time.deltaTime;

            if (countGame == (diffLevel * 3))
            {
                Finish();
            }
        }
    }

    public void checkPos()
    {
        isClick = true;
        checkGame();
    }

    IEnumerator changeText()
    {
        while(gameOver)
        {
            updateText();
            yield return new WaitForSeconds(waitingTime);
        }
    }

    void Finish()
    {
        Destroy(text);
        GameObject.Destroy(gameObject.transform.GetChild(0).gameObject);
        gameOver = true;
    }
}
