using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameEngine : MonoBehaviour
{
    public int Demo = 0;

    //timebar
    public GameObject TBC;
    timebarScript timebar;
    //1 -> Easy
    //2 -> Normal
    //3 -> Hard
    int diffLevel = new int();
    private int row = 2;
    private int col;
    private float offsetX = 4f;
    private float offsetY = 4f;

    private int counter = 0;
    private int numberOfCards;

    [SerializeField] private Card originalCard;
    [SerializeField] private Sprite[] images;   //hold our images

    private Card firstCard;
    private Card secondCard;

    public bool selectedTwoCard
    {
        get { return secondCard == null; }
    }

    void Awake()
    {
        images = Resources.LoadAll<Sprite>("Sprites/cardMatch");
        images = shuffleImages(images);
    }

    private void Start()
    {
        //timebar
        GameObject temp = Instantiate(TBC);
        timebar = temp.GetComponent<TBCscript>().timebar();

        if (Demo == 0)
        {
            diffLevel = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Difficulty();
        }
        else
        {
            diffLevel = Demo;
        }
        
        originalCard.GetComponent<SpriteRenderer>().enabled = true;

        Vector3 startPos = originalCard.transform.position;

        switch(diffLevel)
        {
            case 1:
                col = 3;
                startPos.x = -4;
                timebar.SetMax(4);
                break;
            case 2:
                col = 4;
                timebar.SetMax(7);
                break;
            case 3:
                startPos.x = -7;
                offsetX = 3.5f;
                col = 5;
                timebar.SetMax(10);
                break;
        }

        //int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        int[] numbers = new int[row * col];
        numbers = prepareArray(numbers);
        numbers = shuffle(numbers);

        numberOfCards = (row * col) / 2;

        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                Card card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as Card;
                    card.transform.parent = gameObject.transform;
                }

                int index = j * col + i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]);

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                card.transform.localScale = new Vector3(0.6f,0.6f,1);
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private void Update()
    {
        if (timebar.GetTime() == 0)
        {
            GameOver(0);
        }
    }

    private int[] prepareArray(int[] numbers)
    {
        int high, maxNumber;
        high = row > col ? row : col;
        maxNumber = row * col;

        int[] temp = new int[maxNumber];
        for (int i = 0, j = 0; j < high; j++)
        {
            temp[i++] = j;
            temp[i++] = j;
        }
        return temp;
    }

    private int[] shuffle(int[] numbers)
    {
        int[] temp = numbers.Clone() as int[];
        for (int i = 0; i < temp.Length; i++)
        {
            int tmp = temp[i];
            int random = Random.Range(i, temp.Length);
            temp[i] = temp[random];
            temp[random] = tmp;
        }
        return temp;
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


    public void CardRevealed(Card card)
    {
        if (timebar.GetTime() == timebar.GetMax())
        {
            timebar.Begin();
        }
        if (firstCard == null)     //first card
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
            StartCoroutine(checkMatch());
        }
    }

    private IEnumerator checkMatch()
    {
        if (firstCard.getId != secondCard.getId)    //they are not same
        {
            yield return new WaitForSeconds(0.5f);
            firstCard.Flip();
            secondCard.Flip();
        }
        else                                        //their face are same
        {
            firstCard.matched = true;
            secondCard.matched = true;
            counter++;
            if(counter == numberOfCards)
            {
                GameOver(1);
            }
        }
        //release selected two cards for new cards
        firstCard = null;
        secondCard = null;
    }

    IEnumerator EndOfMinigame(bool result)
    {
        yield return new WaitForSeconds(1);
        if (Demo == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().EndOfMinigame(10, result);
        }
    }


    void GameOver(int x)
    {
        if (x == 1)
        {
            timebar.Stop();
            StartCoroutine(EndOfMinigame(true));
        }
        else
        {
            timebar.Stop();
            StartCoroutine(EndOfMinigame(false));
        }
    }
}
