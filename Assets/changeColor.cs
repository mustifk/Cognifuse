using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeColor : MonoBehaviour
{
    public void colorChange()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<Image>().color = new Color(0.3568628f, 0.3490196f, 0.3490196f, 1); ;
    }
}
