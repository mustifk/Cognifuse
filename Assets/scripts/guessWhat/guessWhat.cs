using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guessWhat : MonoBehaviour
{
    [SerializeField] private Sprite[] images;
    [SerializeField] private int difficulty;
    [SerializeField] private guessImage originalCard;
    [SerializeField] private GameObject cover;

    guessImage card;
    void Awake()
    {
        images = Resources.LoadAll<Sprite>("Sprites/haveYou");
        images = shuffleImages(images);
    }
    void Start()
    {
        originalCard.transform.localScale = new Vector2(0.5f, 0.5f);
        originalCard.transform.position = new Vector2(0f, 2f);
        switch (difficulty)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            default:
                break;
        }
        StartCoroutine(begin());
    }
    private IEnumerator begin()
    {
        yield return new WaitForSeconds(0f);

        StartCoroutine(showCards(0));
        cover.gameObject.SetActive(true);

       
    }
    private IEnumerator showCards(int i)
    {
        yield return new WaitForSeconds(0f);

        card = Instantiate(originalCard) as guessImage;
        card.transform.parent = this.gameObject.transform;
        card.ChangeSprite(i, images[i]);

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
