using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class haveYou : MonoBehaviour
{
    [SerializeField] private Sprite[] images;
    [SerializeField] private int difficulty;
    [SerializeField] private images originalCard;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Button button;
    [SerializeField] private Button button2;
    images card;

    bool isTrue = false, isAgain = true;
    private int cardNumbers;
    void Awake()
    {
        images = Resources.LoadAll<Sprite>("Sprites/whichOne");
        images = shuffleImages(images);
    }

    void Start()
    {
        setActives(false);
        originalCard.transform.localScale = new Vector2(1f, 1f);
        originalCard.transform.position = new Vector2(0f, 0f);
        switch (difficulty)
        {
            case 1:
                cardNumbers = 4;
                break;
            case 2:
                cardNumbers = 5;
                break;
            case 3:
                cardNumbers = 6;
                break;
            default:
                break;
        }
        StartCoroutine(listCards());
    }
    private IEnumerator listCards()
    {

        for (int i = 0; i < cardNumbers; i++)
        {
            StartCoroutine(showCards(i));
            yield return new WaitForSeconds(2f);

        }

        isAgain = false;

        if (Random.Range(0, 2) == 1)
        {
            True();
        }
        else
        {
            False();
        }

        yield return new WaitForSeconds(1f);
    }

    private IEnumerator showCards(int i)
    {
        yield return new WaitForSeconds(0f);

        card = Instantiate(originalCard) as images;
        card.transform.parent = this.gameObject.transform;
        card.ChangeSprite(i, images[i]);
        card.isDestroy(isAgain);

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
    private void True()
    {
        isTrue = true;
        setActives(true);
        originalCard.transform.localScale = new Vector2(0.6f, 0.6f);
        int index = Random.Range(0, difficulty);
        StartCoroutine(showCards(index));
        Debug.Log("true");
    }
    private void False()
    {
        isTrue = false;
        setActives(true);
        originalCard.transform.localScale = new Vector2(0.6f, 0.6f);
        int index = Random.Range(difficulty, images.Length);
        StartCoroutine(showCards(index));
        Debug.Log("false");
    }
    public void Press(bool trueorfalse)
    {
        if (isTrue && trueorfalse || !isTrue && !trueorfalse)
        {
            Debug.Log("win");

        }
        else
        {
            Debug.Log("lose");
        }
    }
    private void setActives(bool trueorfalse)
    {
        button.gameObject.SetActive(trueorfalse);
        button2.gameObject.SetActive(trueorfalse);
        text.enabled = trueorfalse;
    }
}
