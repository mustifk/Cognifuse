using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ChangeText : MonoBehaviour
{
    public int Demo = 0;
    public Animator anim;

    //timebar
    public GameObject TBC;
    timebarScript timebar;
    public AudioSource correct,incorrect;
    [SerializeField] private TextMeshProUGUI myText;
    [SerializeField] private TextMeshProUGUI myText2;
    [SerializeField] private Button Button;
    [SerializeField] private Button Button2;
    [SerializeField] private Button checkButton;
    [SerializeField] private Button uncheckButton;
    private int difficulty = new int();

    string[] colorNames = { "Red", "Blue", "Black", "Green", "cyan", "Magenta", "White", "Yellow" };
    Color[] colors;
    private int current, current2, current3;
    private float level;

    
    bool gameover = false;
    bool answer;
    void Start()
    {
        

        //timebar
        GameObject temp = Instantiate(TBC);
        timebar = temp.GetComponent<TBCscript>().timebar();

        if (Demo == 0)
        {
            difficulty = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Difficulty();
        }
        else
        {
            difficulty = Demo;
        }


        switch (difficulty)
        {
            case 2:
                level = 6;
                timebar.SetMax(8);
                break;
            case 3:
                timebar.SetMax(10);
                level = 8;
                break;
            default:
                timebar.SetMax(6);
                level = 4;
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



        if (GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Language() == 0)
        {
            colorNames[0] = "Kirmizi";
            colorNames[1] = "Mavi";
            colorNames[2] = "Siyah";
            colorNames[3] = "Yesil";
            colorNames[4] = "Turkuaz";
            colorNames[5] = "Pembe";
            colorNames[6] = "Beyaz";
            colorNames[7] = "Sari";
        }

        timebar.Begin();
        StartCoroutine(Begin());
    }

    private void Update()
    {
        if (timebar.GetTime() == 0 && !gameover)
        {
            endGame(false);
        }
    }

    IEnumerator Begin()
    {
        current = Random.Range(0, 8);
        current2 = Random.Range(0, 8);
        while (current == current2) // birincinin texti ile ikincinin renginin farklı olması için.
        {
            current2 = Random.Range(0, 8);
        }
        checkButton.gameObject.SetActive(true);
        Button.gameObject.SetActive(true);
        Button2.gameObject.SetActive(true);
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
            endGame(true);
        }

    }


    public void Press(bool trueorfalse)
    {
        if (trueorfalse && answer || !trueorfalse && !answer && !gameover)
        {
            correct.Play();
            StartCoroutine(Begin());
            level--;
        }
        else
        {
            incorrect.Play();
            endGame(false);
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
    }

    public void endGame(bool win)
    {
        StopAllCoroutines();
        gameover = true;
        timebar.Stop();
        myText.gameObject.SetActive(false);
        myText2.gameObject.SetActive(false);
        checkButton.gameObject.SetActive(false);
        uncheckButton.gameObject.SetActive(false);
        Button.gameObject.SetActive(false);
        Button2.gameObject.SetActive(false);
        if (win)
        {
            StartCoroutine(EndOfMinigame(true));
        }
        else
        {
            StartCoroutine(EndOfMinigame(false));
        }
    }
    IEnumerator EndOfMinigame(bool result)
    {
        if (result==true)
        {
            anim.SetBool("true1", true);
        }
        else
        {
            anim.SetBool("false1", true);
        }
        yield return new WaitForSeconds(0.8f);
        if (Demo == 0)
        {
                GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame((timebar.GetTime() / timebar.GetMax()), result);
        }
    }
}
