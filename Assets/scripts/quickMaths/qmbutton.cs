using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class qmbutton : MonoBehaviour
{
    public TextMeshProUGUI text;
    void Start()
    {
        
    }


    void Update()
    {

    }

    public void Answer()
    {
        this.transform.parent.GetComponent<qmscript>().choice(text.text);
    }
}
