using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class guessWhat : MonoBehaviour
{
    [SerializeField] private Sprite[] images;
    [SerializeField] private int difficulty;
    [SerializeField] private guessImage originalCard;
    [SerializeField] private guessImage card;
    [SerializeField] private GameObject glass;

    private List<int> randomList = new List<int>();
    private bool find = false;
    private int cardNumbers;
    float startposX, startPosY = -2f,posX,plus;
    void Awake()
    {
        images = Resources.LoadAll<Sprite>("Sprites/haveYou");
        images = shuffleImages(images);
    }
    void Start()
    {
        switch (difficulty)
        {
            case 1:
                cardNumbers = 3;
                startposX = -8f;
                plus = 4f;
                break;
            case 2:
                cardNumbers = 4;
                startposX = -9f;
                plus = 3.5f;

                break;
            case 3:
                cardNumbers = 5;
                startposX = -9f;
                plus = 3f;
                break;
            default:
                break;
        }
        shuffleList();
        StartCoroutine(begin());
    }
    private IEnumerator begin()
    {
        yield return new WaitForSeconds(0f);
       

        StartCoroutine(showCards(0,false));
        for (int i = 0; i < cardNumbers; i++)
        {

            StartCoroutine(showCards(randomList[i], true));
        }
        glass.SetActive(true);

    }
    private IEnumerator showCards(int i, bool inst)
    {
        yield return new WaitForSeconds(0f);
        if (inst)
        {
            card = Instantiate(originalCard) as guessImage;
            card.transform.parent = this.gameObject.transform;
            card.ChangeSprite(i, images[i]);
            posX = startposX + plus;
            startposX = posX;
            card.transform.position = new Vector2(posX, startPosY);


        }
        else
        {
            int numberX = Random.Range(-2,2);
            int numberY = Random.Range(1, 3);

            card.transform.position = new Vector2(numberX, numberY);
            card.ChangeImage(i, images[i]);
            card.gameObject.SetActive(true);


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
        int index;
        for (int i = 0; i < cardNumbers; i++)
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
    }
    public void check(int id)
    {
        if(id == 0)
        {
            Debug.Log("win");
        }
        else
        {
            Debug.Log("lose");

        }
    }
}
