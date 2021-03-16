using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameEngine : MonoBehaviour
{
    //1 -> Easy
    //2 -> Normal
    //3 -> Hard
    public int diffLevel;

    private int row = 2;
    private int col;
    public const float offsetX = 4f;
    public const float offsetY = 5f;

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
        originalCard.GetComponent<SpriteRenderer>().enabled = true;

        Vector3 startPos = originalCard.transform.position;

        switch(diffLevel)
        {
            case 1:
                col = 3;
                startPos.x = -5;
                break;
            case 2:
                col = 4;
                break;
            case 3:
                startPos.x = -8;
                col = 5;
                break;
        }

        //int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        int[] numbers = new int[row * col];
        numbers = prepareArray(numbers);
        numbers = shuffle(numbers);

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
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
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
        }
        //release selected two cards for new cards
        firstCard = null;
        secondCard = null;
    }

}
