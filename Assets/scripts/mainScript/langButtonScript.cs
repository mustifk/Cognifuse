using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class langButtonScript : MonoBehaviour
{
    public Sprite tr, en;

    public int totalLanguage = 2;
    // Start is called before the first frame update
    static int language = 0;
    void Start()
    {
        language = GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Language();
        if (language == 0)
        {
            this.GetComponent<Image>().sprite = tr;
        }
        else
        {
            this.GetComponent<Image>().sprite = en;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Press()
    {
        language = (language + 1) % totalLanguage;
        if (language == 0)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().ChangeLanguage(language);
        }
        else
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().ChangeLanguage(language);
        }
    }
}
