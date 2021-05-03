using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class transScript : MonoBehaviour
{
    public TextMeshProUGUI text,title,tapToStart,mainMenu;
    public float transitionDuration = 8f;
    public int sceneIndex = 0;
    private string[][] instructions;

    [SerializeField]
    Canvas canvas;

    [SerializeField]
    GameObject brain,deathBrain;

    //[SerializeField]
    GameObject[] brains;

    Animator animator;

    bool changed2 = false, changed1 = false;
    float startTime, speed = 1.0f;

    static int number_of_brains;

    // Start is called before the first frame update
    void Start()
    {
        switch (GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Language())
        {
            case 1:
                tapToStart.text = "Tap to Continue";
                mainMenu.text = "Main Menu";
                break;
            default:
                tapToStart.text = "Devam Etmek Icın Dokunun";
                mainMenu.text = "Ana Menu";
                break;
        }
        instructions = new string[40][];
        for (int i = 0; i < 40; i++)
        {
            instructions[i] = new string[2];
        }
        instructions[0][0] = "This Is Title";
        instructions[0][1] = "Here we explain the minigame!";
        instructions[1][0] = "Guess The Item";
        instructions[1][1] = "Choose the correct item that is under the magnifying glass!";
        instructions[2][0] = "Correct Side";
        instructions[2][1] = "Place the item to the correct size but be careful, sides are changing!";
        instructions[3][0] = "Tap The Dot";
        instructions[3][1] = "Only press the blinking button if it is circle and dark green!";
        instructions[4][0] = "Quick Maths";
        instructions[4][1] = "Choose the answer of the given basic math questions,be quick!";
        instructions[5][0] = "Have You Seen This";
        instructions[5][1] = "Follow the objects in the screen and then remember, have you seen this?";
        instructions[6][0] = "Camel Dwarf";
        instructions[6][1] = "Press the screen to do a camel or a dwarf, be cautious about the current status!";
        instructions[7][0] = "Shape Match";
        instructions[7][1] = "Find the correct shapes among all the shapes on screen and select them!";
        instructions[8][0] = "Double Dribble";
        instructions[8][1] = "Catch the circles with the same color and avoid vice versa!";
        instructions[9][0] = "Color Confusion";
        instructions[9][1] = "Result is correct if the text in the left part is the color of the right part!";
        instructions[10][0] = "Match The Badges";
        instructions[10][1] = "Try to find the other twin of all the badges on the screen!";
        instructions[11][0] = "Word Quest";
        instructions[11][1] = "Try to find the hidden words written on the left before the time is up!";
        instructions[12][0] = "Which One";
        instructions[12][1] = "Watch carefully to catch the one which wasn't on the previous screen!";
        instructions[13][0] = "Number Rush";
        instructions[13][1] = "Try to collect all the numbers in the order, avoid the enemies!";
        instructions[14][0] = "Paint The Shape";
        instructions[14][1] = "Try to figure out the final shape when the shapes on left and right become one!";
        instructions[15][0] = "Simon's Orders";
        instructions[15][1] = "Wait for the buttons to blink, then push them in the correct order!";
        instructions[16][0] = "Gorseli Bul";
        instructions[16][1] = "Buyutec içerisindeki resmin orjinal hali hangisi bul.";
        instructions[17][0] = "Sagli Sollu";
        instructions[17][1] = "Ortadaki objeyi dogru tarafa yonlendir, dikkat et, taraflar degisiyor!";
        instructions[18][0] = "Butona Dokun";
        instructions[18][1] = "Ekrandaki butonlardan yalnızca koyu yesil ve yuvarlak olanına bas!";
        instructions[19][0] = "Cabuk Matematik";
        instructions[19][1] = "Verilen basit matematik sorularini cevapla, cabuk ol!";
        instructions[20][0] = "Bunu Gordun Mu";
        instructions[20][1] = "Ekrana gelen resimleri aklinda tut, sondaki resmi gormus muydun?";
        instructions[21][0] = "Deve Cuce";
        instructions[21][1] = "Deve veya cuce olmak icin ekrana dokun, o anki durumuna dikkat et!";
        instructions[22][0] = "Sekil Eslestir";
        instructions[22][1] = "Ekrandaki sekillerden yalnızca dogru olanları sec!";
        instructions[23][0] = "Ikili Bela";
        instructions[23][1] = "Saga ve sola dokunarak oyuncuları yonlendir, yalnızca dogru renkleri topla!";
        instructions[24][0] = "Renkli Kargasa";
        instructions[24][1] = "Cevap yalnızca soldaki yazı sağdaki renkle eslesiyorsa dogru, dikkatli ol!";
        instructions[25][0] = "Rozet Eslestirme";
        instructions[25][1] = "Ekrandaki rozetleri cevirerek eslerini bulmaya calis!";
        instructions[26][0] = "Kelime Avi";
        instructions[26][1] = "Sure bitmeden ekrandaki harf obeginden soldaki kelimeleri bulmaya calis!";
        instructions[27][0] = "Hangisi Gitti";
        instructions[27][1] = "İlk ekranda olup da sonraki ekranda olmayan gorseli bulmaya calis!";
        instructions[28][0] = "Sayi Maratonu";
        instructions[28][1] = "Dusmanlara yakalanmadan ekrandaki sayilari sirasiyla toplamaya calis!";
        instructions[29][0] = "Sekil Boyama";
        instructions[29][1] = "Kenarlardaki sekillerin ust uste binmis halini ortadaki sekile uygula,unutma beyaz baskin kutucuk!";
        instructions[30][0] = "Yakup Diyor Ki";
        instructions[30][1] = "Butonlarin yanmasini bekle ve ardindan ayni sirayla butonlara bas!";
        createBrains();
        startTime = Time.time;

        if (GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().GetHP() == number_of_brains && number_of_brains == 2)
            changed2 = true;
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().GetHP() == number_of_brains && number_of_brains == 1)
        {
            changed2 = true;
            changed1 = true;
        }
        number_of_brains = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().GetHP();
        changeColor();
  
        CurrentSceneSelector();
        StartCoroutine(NextScene());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReturnToMainMenu()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().tag = "OldScript";
        SceneManager.LoadScene("Main");
    }

    void changeColor()
    {
        if (number_of_brains == 2 && !changed2)
        {
            animator = brains[2].transform.GetComponent<Animator>();
            animator.SetBool("loseTrigger", true);
        }
        if (number_of_brains == 1 && !changed1)
        {
            animator = brains[1].transform.GetComponent<Animator>();
            animator.SetBool("loseTrigger", true);

            float t = (Time.time - startTime) * speed;

            brains[2].GetComponent<changeColor>().colorChange();
        }
        else
        {
            if (changed1 && changed2)
            {
                brains[1].GetComponent<changeColor>().colorChange();
                brains[2].GetComponent<changeColor>().colorChange();
            }
            else if (changed1)
                brains[1].GetComponent<changeColor>().colorChange();
            else if (changed2)
                brains[2].GetComponent<changeColor>().colorChange();
        }
    }

    void createBrains()
    {
        brains = new GameObject[3];
        if (GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().GetHP() == 3)
        {

            brains[0] = Instantiate(brain, new Vector3(-4, 3.5f), Quaternion.identity, canvas.transform);
            brains[1] = Instantiate(brain, new Vector3(0, 3.5f), Quaternion.identity, canvas.transform);
            brains[2] = Instantiate(brain, new Vector3(4, 3.5f), Quaternion.identity, canvas.transform);
        }
        else if (GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().GetHP() == 2)
        {

            brains[0] = Instantiate(brain, new Vector3(-4, 3.5f), Quaternion.identity, canvas.transform);
            brains[1] = Instantiate(brain, new Vector3(0, 3.5f), Quaternion.identity, canvas.transform);
            brains[2] = Instantiate(deathBrain, new Vector3(4, 3.5f), Quaternion.identity, canvas.transform);

        }
        else if (GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().GetHP() == 1)
        {

            brains[0] = Instantiate(brain, new Vector3(-4, 3.5f), Quaternion.identity, canvas.transform);
            brains[1] = Instantiate(deathBrain, new Vector3(0, 3.5f), Quaternion.identity, canvas.transform);
            brains[2] = Instantiate(deathBrain, new Vector3(4, 3.5f), Quaternion.identity, canvas.transform);

        }
    }

    IEnumerator NextScene()
    {
        
        yield return new WaitForSeconds(transitionDuration);
        GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().NextScene();
    }

    public void NextSceneImmediate()
    {
        StopAllCoroutines();
        GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().NextScene();

    }

    void CurrentSceneSelector()
    {
        switch (GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Language())
        {
            case 1:
                title.text = instructions[GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().CurrentSceneIndex()][0];
                text.text = instructions[GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().CurrentSceneIndex()][1];
                break;
            default:
                title.text = instructions[GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().CurrentSceneIndex()+15][0];
                text.text = instructions[GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().CurrentSceneIndex()+15][1];
                break;
        }
    }

}
