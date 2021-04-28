using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class soundButtonScript : MonoBehaviour
{
    public Sprite on, off;
    public AudioListener audio;
    
    // Start is called before the first frame update
    bool toggleListening = true;
    void Start()
    {
        toggleListening = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Listening();
        if (toggleListening)
        {
            audio.enabled = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Listen(true);
            this.GetComponent<Image>().sprite = on;
        }
        else
        {
            audio.enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Listen(false);
            this.GetComponent<Image>().sprite = off;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Press()
    {
        toggleListening = !toggleListening;
        if (toggleListening)
        {
            audio.enabled = true;
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Listen(true);
            this.GetComponent<Image>().sprite = on;
        }
        else
        {
            audio.enabled = false;
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Listen(false);
            this.GetComponent<Image>().sprite = off;
        }
    }
}
