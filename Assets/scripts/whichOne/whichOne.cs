using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class whichOne : MonoBehaviour
{
    [SerializeField] private Sprite[] images;
    [SerializeField] private int difficulty;
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

        originalCard.GetComponent<SpriteRenderer>().enabled = true;
        originalCard.transform.position = new Vector2(-2f, 2f);
        startPos = originalCard.transform.position;

        switch (difficulty)
        {
            case 1:
                col = 4;
                cardNumbers = 4;
                break;
            case 2:
                startPos.x = -6f;
                col = 6;
                cardNumbers = 6;
                break;

            case 3:
                col = 4;
                startPos.x = -6f;
                cardNumbers = 8;
                break;

            default:
                break;
        }
       
        StartCoroutine(listCard());
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
                Debug.Log(randomList[j]);
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
           
            
        }
        yield return new WaitForSeconds(4f);

        if (!isAgain)
        {
            isAgain = true;
            StartCoroutine(listCard());
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
        if (id == cardNumbers)
        {
            Debug.Log("win");
            StopAllCoroutines();
            isAgain = false;

        }
        else
        {
            Debug.Log("lose");
            StopAllCoroutines();
            isAgain = false;
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
