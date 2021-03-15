using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameEngine : MonoBehaviour
{
    //1 -> Easy
    //2 -> Normal
    //3 -> Hard
    public int diffLevel = 1;

    public int row;
    public int col;
    public const float offsetX = 4f;
    public const float offsetY = 5f;

    [SerializeField] private Card originalCard;
    [SerializeField] private Sprite[] images;   //hold our images

    public bool selectedTwoCard
    {
        get { return _secondRevealed == null; }
    }

    private void Start()
    {
        originalCard.GetComponent<SpriteRenderer>().enabled = true;
        Vector3 startPos = originalCard.transform.position;

        //int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        int[] numbers = new int[row * col];
        numbers = prepareArray(numbers);
        numbers = Shuffle(numbers);

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

    private int[] Shuffle(int[] numbers)
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

    private Card _firstRevealed;
    private Card _secondRevealed;

    public void CardRevealed(Card card)
    {
        if (_firstRevealed == null)     //ilk kart
        {
            _firstRevealed = card;
            // _firstRevealed.Flip();
        }
        else
        {
            _secondRevealed = card;
            //_secondRevealed.Flip();
            StartCoroutine(checkMatch());
        }
    }

    private IEnumerator checkMatch()
    {
        if (_firstRevealed.getId != _secondRevealed.getId)
        {
            yield return new WaitForSeconds(0.5f);
            _firstRevealed.Flip();
            _secondRevealed.Flip();
            //_firstRevealed.Unreveal();
            //_secondRevealed.Unreveal();
        }
        else
        {
            _firstRevealed.matched = true;
            _secondRevealed.matched = true;
        }

        _firstRevealed = null;
        _secondRevealed = null;
    }

}
