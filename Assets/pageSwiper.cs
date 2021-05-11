using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; 

public class pageSwiper : MonoBehaviour,IDragHandler,IEndDragHandler
{
    private Vector3 panelLoc;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
    float swipeAmount = 20f;
    int position = 2;
    // Start is called before the first frame update
    void Start()
    {
        panelLoc = transform.position;
        if ((float)Screen.width / (float)Screen.height < 1.79f)
        {
            swipeAmount = 18.86f;
        }
    }

    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLoc - new Vector3(difference / 50, 0, 0); 
    }

    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLoc = panelLoc;
            if (percentage > 0 && position > 0)
            {
                position--;
                newLoc += new Vector3(-swipeAmount, 0, 0);
            }
            else if (percentage < 0 && position < 4)
            {
                position++;
                newLoc += new Vector3(swipeAmount, 0, 0);
            }
            else
            {
                StartCoroutine(SmoothMove(transform.position, panelLoc, easing));
            }
            //transform.position = newLoc;
            StartCoroutine(SmoothMove(transform.position, newLoc, easing));
            panelLoc = newLoc;
        }
        else
        {
            //transform.position = panelLoc;
            StartCoroutine(SmoothMove(transform.position, panelLoc, easing));
        }
    }

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float sec)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / sec;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}
