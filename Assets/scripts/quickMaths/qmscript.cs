using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class qmscript : MonoBehaviour
{
    static int questioncounter;
    public int difficulty;
    private int firstnum, secnum, ans;
    int temp, i, y, x, forans, forans2, forans3;
    public TextMeshProUGUI text, text1, text2, text3;
    public Button button1, button2, button3;
    void Start()
    {
        switch (difficulty)
        {
            case 1:
                questioncounter = 2;
                break;
            case 2:
                questioncounter = 3;
                break;
            case 3:
                questioncounter = 4;
                break;
            default:
                break;
        }
        quest();

    }

    public void choice(string str)
    {
        if (str == ans.ToString())
        {
            if (questioncounter == 0)
            {

                Debug.Log("win");
            }
            else {questioncounter--;
            quest(); }
            
        }
        else
            {
                Debug.Log("lose");
            }

    }

    void quest()
    {
        if (difficulty == 1)
        {
            y = Random.Range(1, 3);
            i = 20;
            x = 20;
        }
        if (difficulty == 2)
        {
            y = Random.Range(1, 4);
            if (y == 3) { i = 10; x = 10; }
            else { i = 30; }
        }
        if (difficulty == 3)
        {
            y = Random.Range(1, 5);
            if (y == 4) { i = 20; x = 40; }
            if (y == 3) { i = 10; x = 10; }
            else { x = 40; i = 40; }
        }

        firstnum = Random.Range(1, x);
        secnum = Random.Range(1, i);
        if (secnum > firstnum)
        {
            temp = secnum;
            secnum = firstnum;
            firstnum = temp;
        }
        if (y == 1)
        {
            ans = firstnum + secnum;
            text.text = firstnum.ToString() + " + " + secnum.ToString();
            Debug.Log(firstnum + " + " + secnum + " =" + ans);
            if (difficulty == 1)
            {
                forans3 = Random.Range(1, ans);
                forans2 = Random.Range(1, ans);
                if (forans2 == forans3) { forans2 = Random.Range(1, forans3); }
            }
            if (difficulty == 2)
            {
                forans3 = Random.Range(1, ans);
                forans2 = Random.Range(1, ans);
                if (forans2 == forans3) { forans2 = Random.Range(1, forans3); }
            }
            if (difficulty == 3)
            {
                forans3 = Random.Range(1, ans);
                forans2 = Random.Range(1, ans);
                if (forans2 == forans3) { forans2 = Random.Range(1, forans3); }
            }
        }
        if (y == 2)
        {
            ans = firstnum - secnum;
            text.text = firstnum.ToString() + " - " + secnum.ToString();
            Debug.Log(firstnum + " - " + secnum + " =" + ans);
            if (difficulty == 1)
            {
                forans3 = ans+3;
                forans2 = ans + 6;
                if (forans2 == forans3) { forans2 = Random.Range(1, forans3); }
            }
            if (difficulty == 2)
            {
                forans3 = ans + 3;
                forans2 = ans + 6;
                if (forans2 == forans3) { forans2 = Random.Range(1, forans3); }
            }
            if (difficulty == 3)
            {
                forans3 = ans + 3;
                forans2 = ans + 6;
                if (forans2 == forans3) { forans2 = Random.Range(1, forans3); }
            }
        }
        if (y == 3)
        {
            ans = firstnum * secnum;
            text.text = firstnum.ToString() + " * " + secnum.ToString();
            Debug.Log(firstnum + " * " + secnum + " =" + ans);
            if (difficulty == 1)
            {
                forans3 = Random.Range(1, ans);
                forans2 = Random.Range(1, ans);
                if (forans2 == forans3) { forans2 = Random.Range(1, forans3); }
            }
            if (difficulty == 2)
            {
                forans3 = Random.Range(1, ans);
                forans2 = Random.Range(1, ans);
                if (forans2 == forans3) { forans2 = Random.Range(1, forans3); }
            }
            if (difficulty == 3)
            {
                forans3 = Random.Range(1, ans);
                forans2 = Random.Range(1, ans);
                if (forans2 == forans3) { forans2 = Random.Range(1, forans3); }
            }
        }
        if (y == 4)
        {
            ans = firstnum / secnum;
            Debug.Log(firstnum + "  " + secnum);
            firstnum = ans * secnum;
            Debug.Log(firstnum + "  " + secnum);
            text.text = firstnum.ToString() + " / " + secnum.ToString();
            Debug.Log(firstnum + " / " + secnum + " =" + ans);
            if (difficulty == 1)
            {
                forans3 = ans + 3;
                forans2 = ans + 6;
                if (forans2 == forans3) { forans2 = Random.Range(1, forans3); }
            }
            if (difficulty == 2)
            {
                forans3 = ans + 3;
                forans2 = ans + 6;
                if (forans2 == forans3) { forans2 = Random.Range(1, forans3); }
            }
            if (difficulty == 3)
            {
                forans3 = ans + 3;
                forans2 = ans + 6;
                if (forans2 == forans3) { forans2 = Random.Range(1, forans3); }
            }
        }
        forans = Random.Range(1, 4);
        if (forans == 1) { text1.text = ans.ToString(); text2.text = forans2.ToString(); text3.text = forans3.ToString(); }
        if (forans == 2) { text1.text = forans2.ToString(); text2.text = ans.ToString(); text3.text = forans3.ToString(); }
        if (forans == 3) { text1.text = forans2.ToString(); text2.text = forans3.ToString(); text3.text = ans.ToString(); }
    }
}
