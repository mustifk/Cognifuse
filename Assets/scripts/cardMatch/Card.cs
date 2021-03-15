using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField]
    private gameEngine controller;

    //private GameObject controller;

    private SpriteRenderer rend;

    [SerializeField]
    private Sprite faceSprite, backSprite;

    private bool coroutineAllowed, facedUp;
    public bool matched = false;

    private int id;
    public int getId
    {
        get { return id; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //controller = GameObject.FindGameObjectWithTag("GameController");
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = backSprite;
        coroutineAllowed = true;
        facedUp = false;
    }

    public void ChangeSprite(int id, Sprite image)
    {
        this.id = id;
        faceSprite = image;
    }

    private void OnMouseDown()
    {
        if (coroutineAllowed && !matched && controller.selectedTwoCard)
        {
            Flip();
            controller.CardRevealed(this);
        }
    }

    public void Flip()
    {
        StartCoroutine(RotateCard());
    }

    private IEnumerator RotateCard()
    {
        coroutineAllowed = false;

        if (!facedUp)
        {
            for (float i = 0f; i <= 180f; i += 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    rend.sprite = faceSprite;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }

        else if (facedUp)
        {
            for (float i = 180f; i >= 0f; i -= 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    rend.sprite = backSprite;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }

        coroutineAllowed = true;

        facedUp = !facedUp;
    }
}
