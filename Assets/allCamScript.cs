using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class allCamScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        if (!GameObject.FindGameObjectWithTag("Player").GetComponent<mainScript>().Listening())
        {
            this.GetComponent<AudioListener>().enabled = false;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
           
    }
}
