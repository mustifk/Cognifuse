using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //1 -> Easy
    //2 -> Normal
    //3 -> Hard
    public int diffLevel = 1;

    public int row;
    public int col;
    public const float offsetX = 4f;
    public const float offsetY = 5f;

    [SerializeField] private Main originalCard;
    [SerializeField] private Sprite[] images;   //hold our images

    private void Start()
    {
        Vector3 startPos = originalCard.transform.position;

        //int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        int[] numbers = new int[row * col];
        numbers = prepareArray(numbers);
        numbers = Shuffle(numbers);

        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                Main card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as Main;
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
        for(int i = 0, j=0;j<high;j++)
        {
            temp[i] = j;
            temp[++i] = j;
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

    private Main _firstRevealed;
    private Main _secondRevealed;

    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(Main card)
    {
        if (_firstRevealed == null)
        { //ilk kart
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(checkMatch());
        }
    }

    private IEnumerator checkMatch()
    {
        if(_firstRevealed.getId != _secondRevealed.getId)
        { 
            yield return new WaitForSeconds(0.5f);
            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;
    }

}
