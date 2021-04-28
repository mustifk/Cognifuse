using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public AudioSource flip;
    [SerializeField]
    private gameEngine controller;

    private SpriteRenderer render;

    [SerializeField]
    private Sprite faceSprite, backSprite;

    private bool coroutineAllowed, facedUp;
    public bool matched = false;

    private int id;
    public int getId
    {
        get { return id; }
    }

    void Start()
    {
        render = GetComponent<SpriteRenderer>();
        render.sprite = backSprite;
        facedUp = false;
        coroutineAllowed = true;
    }

    public void ChangeSprite(int id, Sprite image)
    {
        this.id = id;
        faceSprite = image;
    }

    private void OnMouseDown()
    {
        if (coroutineAllowed && !matched && controller.selectedTwoCard & controller.bTime)
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
        flip.Play();
        coroutineAllowed = false;

        if (!facedUp)
        {
            for (float i = 0f; i <= 180f; i += 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    render.sprite = faceSprite;
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
                    render.sprite = backSprite;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }

        coroutineAllowed = true;

        facedUp = !facedUp;
    }
}
