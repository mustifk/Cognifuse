using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class button : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public AudioSource slider;
    wordController wordController;

    Image color;

    RectTransform scale;

    string character;
    bool selected_button = false;

    public bool destroy_button = false;
    float reduction_scale = 0.1f;

    void Start()
    {
        wordController = GameObject.FindGameObjectWithTag("GameController").GetComponent<wordController>();

        color = GetComponent<Image>();

        scale = GetComponent<RectTransform>();

        character = gameObject.name;

    }

    void Update()
    {
        if(wordController.selected == false)
        {
            selected_button = false;
            color.color = Color.white;
        }

        if(destroy_button == true)
        {
            scale.localScale -= new Vector3(reduction_scale, reduction_scale, reduction_scale);

            if (scale.localScale.x <= 0)
                Destroy(gameObject);
        }
    }

    public void makeGreen()
    {
        if(wordController.selected)
        {
            slider.Play();
            color.color = Color.green;
            if(!selected_button)
            {
                wordController.makePath(gameObject);
                selected_button = true;             //each character given 1 time
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
        wordController.selected = false;

        wordController.makeWord();

        wordController.text.text = null;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        wordController.selected = true;
    }
}
