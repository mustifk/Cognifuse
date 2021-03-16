using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ChangeText : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI myText;
    [SerializeField] private TextMeshProUGUI myText2;
    [SerializeField] private Button checkButton;
    [SerializeField] private Button uncheckButton;
    [SerializeField] private int difficulty;

    string[] colorNames = { "Red", "Blue", "Black", "Green", "cyan", "Magenta", "White", "Yellow" };
    Color[] colors;
    private int current, current2, current3;
    private float level;

    
    bool gameover = false;
    bool answer;
    void Start()
    {
        switch (difficulty)
        {
            case 1:
                level = 4;
            break;
            case 2:
                level = 6;
                break;
            case 3:
                level = 8;
                break;
            default:
                break;
        }
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

        while (current == current2) // birincinin texti ile ikincinin renginin farklı olması için.
        {
            current2 = Random.Range(0, 8);
        }

        StartCoroutine(Begin());
    }


    IEnumerator Begin()
    {
        checkButton.gameObject.SetActive(true);
        uncheckButton.gameObject.SetActive(true);
        checkButton.interactable = true;
        uncheckButton.interactable = true;

        yield return new WaitForSeconds(0);

       
        if (Random.Range(0, 2) == 1) // %50 ihtimalle doğru veya yanlış fonksiyonu çağırılıyor.
        {
            True();
        }
        else
        {
            False();
        }
        if (level == 0)
        {
            endGame();
        }

    }


    public void Press(bool trueorfalse)
    {
        if (trueorfalse && answer || !trueorfalse && !answer && !gameover)
        {
            StartCoroutine(Begin());
            level--;
        }
        else
        {
            Debug.Log("GameOver");
            endGame();

        }
    }

    void True()
    {
        answer = true; 
        myText.text = colorNames[current]; // birincinin textiyle ikincinin rengi aynı olursa doğru oluyor.
        myText2.color = colors[current];


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
        myText.text = colorNames[current]; // birincinin textiyle ikincinin rengi farklı olursa yanlış oluyor.
        myText2.color = colors[current2];

        //To different color and color names
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

    public void endGame()
    {
        StopAllCoroutines();
        gameover = true;
        myText.gameObject.SetActive(false);
        myText2.gameObject.SetActive(false);
        checkButton.gameObject.SetActive(false);
        uncheckButton.gameObject.SetActive(false);

        //checkButton.interactable = false;
        //uncheckButton.interactable = false;

    }
}
