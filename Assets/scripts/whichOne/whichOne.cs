using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class whichOne : MonoBehaviour
{
    public int Demo = 0;
    //timebar
    public GameObject TBC;
    timebarScript timebar;

    bool isGameover = false;
    [SerializeField] private Sprite[] images;
    private int difficulty;
    private int cardNumbers;
    [SerializeField] private MainCard originalCard;
    private float offsetX,offsetY;
    private int row = 4, col;
    private Vector3 startPos;
    public bool isAgain = false,find = false;
    private List<int> randomList = new List<int>();
    void Awake()
    {
        images = Resources.LoadAll<Sprite>("Sprites/whichOne");
        images = shuffleImages(images);
    }

    private void Start()
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
        
        originalCard.GetComponent<SpriteRenderer>().enabled = true;
        originalCard.transform.position = new Vector2(-2f, 2f);
        startPos = originalCard.transform.position;

        switch (difficulty)
        {
            case 1:
                col = 0;
                cardNumbers = 3;
                timebar.SetMax(3);
                break;
            case 2:
                startPos.x = -3f;
                col = 6;
                cardNumbers = 4;
                timebar.SetMax(3);
                break;

            case 3:
                col = 6;
                startPos.x = -6f;
                timebar.SetMax(3);
                cardNumbers = 6;
                break;

            default:
                break;
        }
       
        StartCoroutine(listCard());
    }
    private void Update()
    {
        if (timebar.GetTime() == 0 && !isGameover)
        {
            isGameover = true;
            timebar.Stop();
            StartCoroutine(GameOver(false));
        }
    }

    IEnumerator GameOver(bool win)
    {
        yield return new WaitForSeconds(1);
        if (Demo == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame((timebar.GetTime() / timebar.GetMax()), win);
        }
     
    }

    private IEnumerator listCard()
    {
        if (isAgain)
        {
            shuffleList();
        }
        for (int j = 0,i=0; j < cardNumbers; j++)
        {

            MainCard card;

            card = Instantiate(originalCard) as MainCard;
            card.transform.parent = this.gameObject.transform;
            card.isDestroy(isAgain);
            if (!isAgain)
            {
                card.ChangeSprite(j, images[j]);
            }
            else
            {
                card.ChangeSprite(randomList[j], images[randomList[j]]);
            }
            
            if (j < cardNumbers / 2)
            {
                float posX = (col * j) + startPos.x;
                float posY = startPos.y - 0.5f ;
                card.transform.position = new Vector2(posX, posY);

            }
            else
            {
                float posX = (col * i) + startPos.x;
                float posY = startPos.y - row;
                card.transform.position = new Vector2(posX, posY);
                i++; 
            }
            if (difficulty == 1)
            {
                if (j == 0)
                {
                    float posX = 4;
                    float posY = 0;
                    card.transform.position = new Vector2(posX, posY);
                }
                else if (j == 1)
                {
                    float posX = 0;
                    float posY = 0;
                    card.transform.position = new Vector2(posX, posY);
                }
                else
                {
                    float posX = -4;
                    float posY = 0;
                    card.transform.position = new Vector2(posX, posY);
                    i++;
                }
            }
        }
        yield return new WaitForSeconds(4f);
        if (!isAgain)
        {
            isAgain = true;
            StartCoroutine(listCard());
            timebar.Begin();
        }
    }
    private void shuffleList()
    {
        int index;
        for(int i = 0; i < cardNumbers; i++)
        {
            while (!find)
            {
                index = Random.Range(0, cardNumbers);
                if (!randomList.Contains(index))
                {
                    randomList.Add(index);
                    find = true;
                }
            }
            find = false;
        }

        index = Random.Range(0, cardNumbers);
        randomList[index] = cardNumbers;
    }

   public void check(int id)
    {
        timebar.Stop();
        isAgain = false;
        StopAllCoroutines();
        if (id == cardNumbers)
        {
            StartCoroutine(GameOver(true));
        }
        else
        {
            StartCoroutine(GameOver(false));
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
   
}
