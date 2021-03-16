using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeText : MonoBehaviour
{

    [SerializeField] private Text myText;
    [SerializeField] private Text myText2;
    [SerializeField] private Button checkButton;
    [SerializeField] private Button uncheckButton;

    //butonlar 2 adet
    string[] colorNames = { "Red", "Blue", "Black", "Green", "cyan", "Magenta", "White", "Yellow" };
    Color[] colors;
    private Color newColor;
    private string newString;
    private int current, current2, current3;

    [SerializeField] private int difficulty;
    private const float coefficient = 1.75f;
    //private int level = difficulty * coefficient;
    bool gameover = false;
    bool answer;
    void Start()
    {
//<<<<<<< Updated upstream

        colors = new Color[8];
        colors[0] = Color.red;
        colors[1] = Color.blue;
        colors[2] = Color.black;
        colors[3] = Color.green;
        colors[4] = Color.cyan;
        colors[5] = Color.magenta;
        colors[6] = Color.white;
        colors[7] = Color.yellow;
        current = Random.Range(0, 8);
        current2 = Random.Range(0, 8);

        while (current == current2)
        {
            current2 = Random.Range(0, 8);
        }
      
        StartCoroutine(Begin());
    }


    IEnumerator Begin()
    {
        //bool gameover = false;

        yield return new WaitForSeconds(0.3f);
     
        if (Random.Range(0, 2) == 1)
        {
            True();
        }
        else
        {
            False();
        }


    }

    
    public void Press(bool trueorfalse)
    {
        if (trueorfalse && answer || !trueorfalse && !answer && !gameover)
        {
            StartCoroutine(Begin());

        }
        else
        {
            Debug.Log("GameOver");
            gameover = true;
            checkButton.interactable = false;
            uncheckButton.interactable = false;

        }
    }

    void True()
    {
        answer = true;
        myText.text = colorNames[current];
        myText2.color = colors[current];
//=======
       // myText. = colorNames[2];
//>>>>>>> Stashed changes
       

        while (current3 == current)
        {
            current3 = Random.Range(0, 8);
        }
        myText.color = colors[current3];
        current3 = Random.Range(0, 8);
        while (current3 == current)
        {
            current3 = Random.Range(0, 8);
        }
        myText2.text = colorNames[current3];

        Debug.Log("true");
    }

    void False()
    {
        answer = false;
        myText.text = colorNames[current];
        myText2.color = colors[current2];
     
        while (current3 == current)
        {
            current3 = Random.Range(0, 8);
        }
        myText.color = colors[current3];
        current3 = Random.Range(0, 8);
        while (current3 == current)
        {
            current3 = Random.Range(0, 8);
        }
        myText2.text = colorNames[current3];


        Debug.Log("false");
    }

}
