using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class qmscript : MonoBehaviour
{
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
                y = Random.Range(1, 3);
                i = 20;
                forans3 = Random.Range(1, 20);
                forans2 = Random.Range(1, 20);
                if (forans2 == forans3) { forans2 = Random.Range(1, forans3); }
                break;
            case 2:
                x = 4;
                y = Random.Range(1, 4);
                forans3 = Random.Range(1, 30);
                forans2 = Random.Range(1, 30);
                if (forans2 == forans3) { forans2 = Random.Range(1, forans3); }
                if (y == 3) { i = 10; }
                else { i = 30; }

                break;
            case 3:
                x = 5;
                y = Random.Range(1, 5);
                forans3 = Random.Range(1, 40);
                forans2 = Random.Range(1, 40);
                if (forans2 == forans3) { forans2 = Random.Range(1, forans3); }
                if (y == 3 || y == 4) { i = 10; }
                else { i = 40; }

                break;
            default:
                break;
        }
        firstnum = Random.Range(1, i);
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

        }
        if (y == 2)
        {
            ans = firstnum - secnum;
            text.text = firstnum.ToString() + " - " + secnum.ToString();
            Debug.Log(firstnum + " - " + secnum + " =" + ans);
        }
        if (y == 3)
        {
            ans = firstnum * secnum;
            text.text = firstnum.ToString() + " * " + secnum.ToString();
            Debug.Log(firstnum + " * " + secnum + " =" + ans);
        }
        if (y == 4)
        {
            ans = firstnum / secnum;
            text.text = firstnum.ToString() + " / " + secnum.ToString();
            Debug.Log(firstnum + " / " + secnum + " =" + ans);
        }
        forans = Random.Range(1, 4);
        if (forans == 1) { text1.text = ans.ToString(); text2.text = forans2.ToString(); text3.text = forans3.ToString(); }
        if (forans == 2) { text1.text = forans2.ToString(); text2.text = ans.ToString(); text3.text = forans3.ToString(); }
        if (forans == 3) { text1.text = forans2.ToString(); text2.text = forans3.ToString(); text3.text = ans.ToString(); }


    }
    public void choice(string str)
    {
        if (str==ans.ToString())
        {
            Debug.Log("win");
        }
        else
        {
            Debug.Log("lose");
        }
    }

    void quest()
    {


    }
}
