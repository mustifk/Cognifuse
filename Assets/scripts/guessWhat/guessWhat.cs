using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guessWhat : MonoBehaviour
{
    public Animator anim;
    public int Demo = 0;
    //timebar
    public GameObject TBC;
    timebarScript timebar;
    public AudioSource correct, incorrect,winSound;
    [SerializeField] private Sprite[] images;
    [SerializeField] private int difficulty;
    [SerializeField] private guessImage originalCard;
    [SerializeField] private guessImage card;
    [SerializeField] private GameObject glass;

    guessImage[] cardChoices;
    private List<int> randomList = new List<int>();
    private bool find = false;
    private int choices, tourCount = 0;
    float scaleX;
    float startposX, startPosY = -2f,posX,plus;
    bool isGameover = false;
    void Awake()
    {
        cardChoices = new guessImage[5];
        images = Resources.LoadAll<Sprite>("Sprites/haveYou");
        images = shuffleImages(images);
    }
    void Start()
    {
        //timebar
        GameObject temp = Instantiate(TBC);
        timebar = temp.GetComponent<TBCscript>().timebar();

        if (Demo == 0)
        {
            difficulty = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Difficulty();
        }

        switch (difficulty)
        {
            case 2:
                tourCount = 3;
                choices = 4;
                startposX = -9f;
                plus = 3.5f;
                scaleX = 1.5f;
                timebar.SetMax(6);
                break;
            case 3:
                tourCount = 4;
                choices = 5;
                startposX = -9f;
                plus = 3f;
                scaleX = 2f;
                timebar.SetMax(8);
                break;
            default:
                tourCount = 2;
                scaleX = 1f;
                choices = 3;
                startposX = -8f;
                plus = 4f;
                timebar.SetMax(9);
                break;
        }
        shuffleList();
        StartCoroutine(Game());
        timebar.Begin();
    }

    IEnumerator Game()
    {
        yield return new WaitForSeconds(0.1f);
        begin();

       
    }

    void begin()
    {
       
        showCards(0,false);
        for (int i = 0; i < choices; i++)
        {
            cardChoices[i] = showCards(randomList[i], true);
        }
        glass.SetActive(true);

    }
    guessImage showCards(int i, bool inst)
    {
        guessImage temporary;
        if (inst)
        {
            temporary = Instantiate(originalCard) as guessImage;
            temporary.transform.parent = this.gameObject.transform;
            temporary.ChangeSprite(i, images[i]);
            posX = startposX + plus;
            startposX = posX;
            temporary.transform.position = new Vector2(posX, startPosY);
            return temporary;
        }
        else
        {
            float numberX = Random.Range(-2f,2f);
            float numberY = Random.Range(1f, 3f);

            card.transform.position = new Vector2(numberX, numberY);
            card.ChangeImage(i, images[i]);
            card.gameObject.SetActive(true);
            card.transform.position.Scale(new Vector2(scaleX,scaleX));
            return card;
        }
    }

    private void Update()
    {
        if (timebar.GetTime() == 0 && !isGameover)
        {
            isGameover = true;
            timebar.Stop();
            Terminate();
            if (Demo == 0)
            {
                StartCoroutine(End(false));
            }
        }
    }
    private Sprite[] shuffleImages(Sprite[] images)
    {
        Sprite[] temp = images.Clone() as Sprite[];
        for (int i = 0; i < temp.Length; i++)
        {
            Sprite tmp = temp[i];
            int random = Random.Range(i, temp.Length);
            temp[i] = temp[random];
            temp[random] = tmp;
        }
        return temp;
    }

    private void shuffleList()
    {
        randomList.Clear();
        int index;
        find = false;
        for (int i = 0; i < choices; i++)
        {
            while (!find)
            {
                index = Random.Range(0, choices);
                if (!randomList.Contains(index))
                {
                    randomList.Add(index);
                    find = true;
                }
            }
            find = false;
        }
    }

    public void check(int id)
    {
        if(id == 0 && !isGameover)
        {
            correct.Play();
            if (tourCount > 0)
            {
                tourCount--;
                Set();
                StartCoroutine(Game());
            }
            else
            {
                Terminate();
                timebar.Stop();
                if (Demo == 0)
                {
                    StartCoroutine(End(true));
                }
                isGameover = true;
            }
        }
        else
        {
            incorrect.Play();
            timebar.Stop();
            Terminate();
            if (Demo == 0)
            {
                StartCoroutine(End(false));
            }
            isGameover = true;
        }
    }

    IEnumerator End(bool win)
    {
        if (win == true)
        {
            winSound.Play();
            anim.SetBool("true1", true);
        }
        else
        {
            anim.SetBool("false1", true);
        }
        yield return new WaitForSeconds(0.8f);
        if (Demo == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame((timebar.GetTime() / timebar.GetMax()), win);
        }
    }

    void Terminate()
    {
        for (int i = 0; i < choices; i++)
        {
            cardChoices[i].GetComponent<guessImage>().defuse();
        }
    }

    void Set()
    {
        images = shuffleImages(images);
        shuffleList();
        switch (difficulty)
        {
            case 2:
                startposX = -9f;
                plus = 3.5f;
                break;
            case 3:
                startposX = -9f;
                plus = 3f;
                break;
            default:
                startposX = -8f;
                plus = 4f;
                break;
        }
        for (int i = 0; i < choices; i++)
        {
            Destroy(cardChoices[i].gameObject);
        }
    }
}
